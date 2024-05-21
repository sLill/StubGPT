<script setup>
    import { ref, onMounted } from 'vue';
    import useEndpointService from './composables/services/useEndpointService';

    const endpointService = useEndpointService();

    const message = ref('');

    // Methods
    const messageInput_KeyPressed = async (event) => {
        if (event.key == 'Enter')
            var response = await endpointService.postData(`/api/v1/message/sendMessage`, {
  "conversation": [
    "string"
  ],
  "message": "sdfsdssssss",
  "rolePreamble": "string"
});
    }
</script>

<template>
    <div class="wrapper">
        <div class="chat-container">
            <div class="conversation-container">
                <p class="conversation-user-message">How do I do the thing</p>
            </div>

            <InputText class="message" placeholder="Message" v-model="message" @keypress.enter="messageInput_KeyPressed"/>
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

.conversation-user-message {

}

.message {
    grid-area: message;
}
</style>
