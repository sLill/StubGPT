<script setup>
    import { ref, computed, watch, onMounted, onUnmounted } from 'vue';
    import useEndpointService from './composables/services/useEndpointService.js';
    import useDialogService from './composables/services/useDialogService.js';
    import { useToast } from 'primevue/usetoast';
    import PromptShortcuts from './components/PromptShortcuts.vue';
    import LoginForm from './components/LoginForm.vue';
    import { getCookie, setCookie } from './composables/utils/webUtils.js';

    const endpointService = useEndpointService();
    const dialogService = useDialogService();
    const toast = useToast();

    const conversationContainer = ref();
    const scrollPanelHeight = ref('auto');

    const conversation = ref([]);
    const systemMessage = ref('You are a helpful assistant');
    const systemMessageFocused = ref(false);

    const message = ref('');
    const messageInputDisabled = ref(false);

    // Methods
    // const showSettingsDialog = () => {

    // };

    const showPromptShortcutsDialog = () => {
        dialogService.showDynamicDialog({ 
            content: PromptShortcuts, 
            data: {},
            callbacks: {
                selectedPrompt: (prompt) => message.value = prompt ? prompt : message.value,
                selectedSystemPrompt: (systemPrompt) => systemMessage.value = systemPrompt ? systemPrompt : systemMessage.value
            }
        });
    };

    const showLoginDialog = () => {
        dialogService.showDynamicDialog({ 
            content: LoginForm, 
            data: {},
            callbacks: {
                loginSuccess: async (sessionToken) => {
                    setCookie('SessionToken', sessionToken);
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

    const formattedMessage = computed(() => (inputText) => {
        return inputText.replace(/```([\s\S]*?)```/g, function(match, code) {
            return '<pre style="background: var(--surface-50); padding: 20px; border-radius: 5px;"><code>' + code.trim() + '</code></pre>';});
    });

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
        <div class="chat-container">
            <ScrollPanel ref="conversationContainer" class="conversation-container" :style="{ height: scrollPanelHeight }">
                <div v-for="conversationMessage in conversation" 
                        :key="conversationMessage" 
                        :class="conversationMessageStyle(conversationMessage)" 
                        v-html="formattedMessage(conversationMessage.content)">
                </div>
            </ScrollPanel>

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
    width: 80%;

    position: relative;
    left: 20%;
    margin-top: 16px;
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
    width: 54px; 
    height: 54px;
    margin: 0 0 1px 5px; 
}
</style>
