<script setup>
    import { ref, inject, onBeforeMount } from 'vue';

    const dialogRef = inject('dialogRef');

    const showSystemPrompts = ref(false);

    const confirm = () => { 
        //if (dialogRef.value.options.callbacks.confirm)
        //    dialogRef.value.options.callbacks.confirm();
        
        dialogRef.value.close();
    }

    const cancel = () => { 
        //if (dialogRef.value.options.callbacks.cancel)
        //    dialogRef.value.options.callbacks.cancel();

        dialogRef.value.close();
    }

    onBeforeMount(() => {
        
    });
</script>

<template>
    <div class="container">
        <div class="content flex-center">
            <div v-if="showSystemPrompts" class="system-prompts-container fill" style="border: 2px solid red;">

            </div>

            <div v-else class="prompts-container fill">
                <div class="prompt flex-center">
                    <InputText></InputText>
                    <InputText></InputText>
                    <Button><FontAwesomeIcon :icon="['fas', 'arrow-turn-up']" /></Button>
                </div>

                <Button style="position: relative; left: 50%; margin-top: 5px;">
                    <FontAwesomeIcon :icon="['fas', 'plus']" />
                </Button>
            </div>
        </div>
        <div class="options flex-center">
            <Button class="flex-center" style="padding: 12px;" label="Save" @click="confirm"/>
            <Button class="flex-center" style="padding: 12px;" label="Cancel" @click="cancel"/>
        </div>
    </div>
</template>

<style scoped>
.container {
    display: grid;
    grid-gap: 10px;
    grid-template: 1fr auto / 1fr;
    grid-template-areas:
    "content"
    "options";

    width: 80vw;
    height: 50vh;

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
}

.options {
    grid-area: options;
    justify-content: right;
    gap: 12px;
}

.prompts-container{
    
}

.prompt {
    display: grid;
    grid-template: 1fr / 120px 1fr 50px;
    grid-gap: 4px;
}
</style>
