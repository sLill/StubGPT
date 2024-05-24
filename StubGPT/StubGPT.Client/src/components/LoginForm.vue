<script setup>
    import { ref, inject } from 'vue';
    import useEndpointService from '../composables/services/useEndpointService.js';

    const endpointService = useEndpointService();
    const dialogRef = inject('dialogRef');

    const username = ref();
    const password = ref();

    const tryLogin = async () => { 
        //if (dialogRef.value.options.callbacks.confirm)
        //    dialogRef.value.options.callbacks.confirm();
        const response = await endpointService.postData(`/api/v1/user/login`, { "username": username.value, "password": password.value });
        if (response && response.status == 200 && response.data) {
            dialogRef.value.options.callbacks.loginSuccess(response.data.sessionToken);
            dialogRef.value.close();
        }
        else 
            dialogRef.value.options.callbacks.loginFailed();
    }
</script>

<template>
    <div class="container">
        <div class="content flex-center">
            <InputText placeholder="Username" v-model="username"></InputText>
            <InputText placeholder="Password" v-model="password" @keypress.enter="tryLogin"></InputText>
        </div>
        <div class="options flex-center">
            <Button class="flex-center" style="padding: 8px; width: 100px;" label="Login" @click="tryLogin" @enter="tryLogin"/>
        </div>
    </div>
</template>

<style scoped>
.container {
    display: grid;
    grid-gap: 20px;
    grid-template: 1fr auto / 1fr;
    grid-template-areas:
    "content"
    "options";

    background: var(--surface-50);
    padding: 20px;
    border-radius: 8px;
}

@media screen and (min-width: 1280px) {
    .container {
        max-width: 960px;
    }
}

.content {
    grid-area: content;
    flex-direction: column;
    gap: 5px;
}

.options {
    grid-area: options;
    justify-content: center;
    gap: 12px;
}

.prompts-container {
    
}

.prompt {
    display: grid;
    grid-template: 1fr / 120px 1fr 50px;
    grid-gap: 4px;
}
</style>
