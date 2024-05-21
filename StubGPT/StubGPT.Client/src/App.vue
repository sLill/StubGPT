<script setup>
    import { ref, computed, onMounted } from 'vue';
    import useEndpointService from './composables/services/useEndpointService';

    const endpointService = useEndpointService();

    const conversation = ref([]);

    const systemMessage = ref('You are a helpful assistant');
    const message = ref('');

    // Methods
    const messageInput_Enter = async (event) => {
        if (!event.shiftKey) {
            event.preventDefault();

            if (conversation.value.length == 0)
                conversation.value.push({ role: "system", content: systemMessage.value });

            endpointService.postData(`/api/v1/message/sendMessage`, { "conversation": conversation.value, "message": message.value, "rolePreamble": null })
            .then(response => {
                if (response) {
                    conversation.value.push({ role: "assistant", content: response.value });
                }
            });

            conversation.value.push({ role: "user", content: message.value });
        }
    }

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
                <p v-for="conversationMessage in conversation" :key="conversationMessage" :class="conversationMessageStyle(conversationMessage)">
                    {{ conversationMessage.content }}
                </p>
            </div>

            <div class="message-container">
                <FloatLabel v-if="conversation.length == 0">
                    <label for="systemMessage">System Message</label>
                    <TextArea id="systemMessage" class="system-message-input" placeholder="System Message" v-model="systemMessage" />
                </FloatLabel>
                <TextArea class="message-input" placeholder="Message" v-model="message" 
                    @keypress.enter="messageInput_Enter" autoResize />
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
</style>
