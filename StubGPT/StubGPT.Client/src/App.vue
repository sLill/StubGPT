<script setup>
    import { ref, computed } from 'vue';
    import useEndpointService from './composables/services/useEndpointService';

    const endpointService = useEndpointService();

    const conversation = ref([]);
    const systemMessage = ref('You are a helpful assistant');
    const systemMessageFocused = ref(false);

    const message = ref('');
    const messageInputDisabled = ref(false);

    // Methods
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
</script>

<template>
    <div class="wrapper">
        <div class="chat-container">
            <div class="conversation-container">
                <p v-for="conversationMessage in conversation" :key="conversationMessage" :class="conversationMessageStyle(conversationMessage)" v-html="formattedMessage(conversationMessage.content)"></p>
            </div>

            <div class="message-container">
                <FloatLabel v-if="conversation.length == 0">
                    <label for="systemMessage">System Message</label>
                    <TextArea id="systemMessage" class="system-message-input" placeholder="System Message"
                              v-model="systemMessage" :autoResize="systemMessageFocused" @focus="systemMessageFocused = true"/>
                </FloatLabel>
                <TextArea class="message-input" placeholder="Message" v-model="message" 
                    @keypress.enter="messageInput_Enter" :disabled="messageInputDisabled" autoResize autofocus />
            </div>
        </div>
    </div>
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
    grid-gap: 20px;
    grid-template: 1fr auto / 1fr;
    grid-template-areas:
        "conversation"
        "message";
    
    width: 80%;
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
}

.conversation-system-message {
    color: var(--text-color-secondary);
}

.conversation-assistant-message {

}

.message-container{
    grid-area: message;
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
</style>
