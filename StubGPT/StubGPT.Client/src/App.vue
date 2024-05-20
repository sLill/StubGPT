<script setup>
    import { ref, onMounted } from 'vue';

    const message = ref('');
</script>

<template>
    <div class="wrapper">
        <div class="chat-container">
            <InputText class="message" placeholder="Message" v-model="message" />
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
    grid-template: 1fr auto / 1fr;
    grid-template-areas:
        "conversation"
        "message";
    
    width: 80%;
    height: 90%;
}

.conversation {
    grid-area: conversation;
}

.message {
    grid-area: message;
}
</style>
