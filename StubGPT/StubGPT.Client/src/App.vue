<script setup>
    import { ref, computed, onMounted, onUnmounted } from 'vue';
    import useEndpointService from './composables/services/useEndpointService.js';
    import useDialogService from './composables/services/useDialogService.js';
    import { useToast } from 'primevue/usetoast';
    import SavedPromptsDialog from './components/dialogs/SavedPromptsDialog.vue';
    import LoginDialog from './components/dialogs/LoginDialog.vue';
    import { getCookie, setCookie, escapeHtml } from './composables/utils/webUtils.js';
    import { marked } from 'marked';
    import 'highlight.js/styles/github.css';

    const endpointService = useEndpointService();
    const dialogService = useDialogService();
    const toast = useToast();

    const isConnected = ref(true);
    const conversationContainer = ref();
    const scrollPanelHeight = ref('auto');

    const conversation = ref([]);
    const systemMessage = ref('You are a helpful assistant');
    const systemMessageFocused = ref(false);

    const message = ref('');
    const messageInputDisabled = ref(false);


    // Methods
    const ping = async () => {
        let pingResponse = await endpointService.getData('/api/v1/system/ping');
        isConnected.value = pingResponse?.status == 200;
    };

    const showPromptShortcutsDialog = () => {
        dialogService.showDynamicDialog({ 
            content: SavedPromptsDialog, 
            data: {},
            callbacks: {
                promptSelected: (prompt) => {
                    if (prompt.promptType == 0)
                        message.value = prompt ? prompt.text : message.value 
                    else
                        message.value = prompt ? prompt.text : message.value 
                }
            }
        });
    };

    const showLoginDialog = () => {
        dialogService.showDynamicDialog({ 
            content: LoginDialog, 
            data: {},
            callbacks: {
                loginSuccess: async (sessionToken) => {
                    setCookie('SessionToken', sessionToken, 30);
                    toast.add({ severity: 'success', summary: null, detail: 'Login Success', life: 3000 });
                    await initialize();
                },
                loginFailed: () => toast.add({ severity: 'error', summary: null, detail: 'Login Failed', life: 3000 })
            }
        });
    };

    const isAuthenticated = async () => {
        let isAuthenticated = false;

        const sessionToken = getCookie('SessionToken');
        if (sessionToken) {
            const response = await endpointService.getData('/api/v1/user/isAuthenticated');
            if (response && response.status == 200)
                isAuthenticated = true;
        }

        return isAuthenticated;
    };

    const initialize = async () => {
        const getLastSystemPromptResponse = await endpointService.getData('/api/v1/message/getLastSystemPrompt');
        if (getLastSystemPromptResponse && getLastSystemPromptResponse.status == 200)
            systemMessage.value = getLastSystemPromptResponse.data.prompt;
    };

    const updateLayout = () => {
        const conversationContainerHeight = conversationContainer.value.clientHeight;
        scrollPanelHeight.value = `${conversationContainerHeight}px`;
    };

    const messageInput_Enter = async (event) => {
        if (!event.shiftKey) {
            event.preventDefault();

            messageInputDisabled.value = true;

            if (conversation.value.length == 0)
                conversation.value.push({ role: "system", content: systemMessage.value });

            conversation.value.push({ role: "user", content: message.value });

            message.value = '';

            const response = await endpointService.postData(`/api/v1/message/sendMessage`, { "conversation": conversation.value, "message": message.value });

            if (response && response.status == 200)
                conversation.value.push({ role: "assistant", content: response.data.response });

            messageInputDisabled.value = false;
        }
    }

    const promptShortcutsButton_Click = () => {
        showPromptShortcutsDialog();
    };

    const conversationMessageStyle = computed(() => (message) => {
        switch (message.role) {
            case 'system':
                return 'conversation-system-message message';

            case 'user':
                return 'conversation-user-message message';

            case 'assistant':
                return 'conversation-assistant-message message';
        }
    });

    onMounted(async () => {
        window.addEventListener('resize', updateLayout);

        updateLayout();

        await ping();

        if (await isAuthenticated())
            await initialize();
        else
            showLoginDialog();
    });

    onUnmounted(() => {
        window.removeEventListener('resize', updateLayout);
    });
</script>

<template>
    <div class="wrapper">
        <div style="position: absolute; top: 10px; right: 10px;">
            <FontAwesomeIcon :icon="isConnected ? ['fas', 'check'] : ['fas', 'x']" />
        </div>
        <div class="chat-container">
            <div ref="conversationContainer" class="conversation-container">
                <ScrollPanel :style="{ padding: '0 40px', height: scrollPanelHeight }">
                    <div v-for="conversationMessage in conversation" 
                            :key="conversationMessage" 
                            :class="conversationMessageStyle(conversationMessage)" 
                            v-html="conversationMessage.role == 'assistant' ? marked(conversationMessage.content) : escapeHtml(conversationMessage.content)">
                    </div>
                </ScrollPanel>
            </div>

            <div class="message-container">
                <div class="fill">
                    <FloatLabel v-if="conversation.length == 0">
                        <label for="systemMessage">System Message</label>
                        <TextArea id="systemMessage" class="system-message-input" placeholder="System Message"
                                  v-model="systemMessage" :autoResize="systemMessageFocused" @focus="systemMessageFocused = true"/>
                    </FloatLabel>

                    <div class="message-input-container">
                        <TextArea class="message-input" placeholder="Message" v-model="message" 
                            @keypress.enter="messageInput_Enter" :disabled="messageInputDisabled" autoResize autofocus />
                    
                    </div>
                </div>

                <Button class="saved-prompts-button flex-center" @click="promptShortcutsButton_Click">
                    <FontAwesomeIcon style="font-size: 1rem;" :icon="['fas', 'file-pen']" />
                </Button>
            </div>
        </div>
    </div>

    <DynamicDialog />
    <Toast />
</template>

<style scoped>
:deep(code) {
  background-color: var(--surface-50);
  display: inline-block;
  padding: 0.5rem;
  border-radius: 5px;
}

.wrapper {
    display: flex;
    align-items: center;
    justify-content: center;
    flex-wrap: wrap;

    width: 100%;
    height: 100%;
}

.chat-container {
    display: grid;
    grid-gap: 30px;
    grid-template: 1fr auto / 1fr;
    grid-template-areas:
        "conversation"
        "message";
    
    width: 85%;
    height: 90%;
}

.conversation-container {
    grid-area: conversation;
}

.message {
    padding: 8px;
    border-radius: 5px;
}

.conversation-user-message {
    background: var(--surface-50);
    width: 70%;

    position: relative;
    left: 30%;
    margin-top: 16px;
    padding: 0.5rem;
}

.conversation-system-message {
    color: var(--text-color-secondary);
}

.conversation-assistant-message {
    margin-top: 16px;
}

.message-container {
    grid-area: message;
    display: flex;

    width: 100%;
    justify-self: center;
}

@media screen and (min-width: 1280px) {
    .message-container {
        max-width: 1280px;
    }
}

.system-message-input {
    resize: none;
    align-content: center;
    color: var(--text-color);
    opacity: 0.5;
    transition: background-color 0.3s ease;

    width: 100%;
    height: auto;
}

.system-message-input:focus {
    opacity: 1;
}

.message-input-container {
    display: flex;
}

.message-input {
    resize: none;
    align-content: center;

    color: var(--text-color);
    width: 100%;
    height: auto;
}

.message-input:disabled {
    opacity: 0.6;
    background: var(--surface-0);   
}

.code-block {
    background-color: red;
}

:deep(.p-scrollpanel-content) {
    overflow-x: hidden; 
    overflow-y: auto; 
    padding: 0 18px 0 0;
}

.saved-prompts-button {
    align-self: end; 
    width: 52px; 
    height: 52px;
    margin: 0 0 1px 5px; 
}
</style>
