<script setup>
    import { ref, inject, onBeforeMount } from 'vue';
    import SavedPromptItem from '/src/components/SavedPromptItem.vue';

    const dialogRef = inject('dialogRef');

    const systemPromptsVisible = ref(false);

    const prompts = ref([]);
    // const systemPrompts = ref([]);

    const loadPrompts = async() => {

    };

    const confirm = () => { 
        if (dialogRef.value.options.callbacks.confirm)
           dialogRef.value.options.callbacks.confirm();
        
        dialogRef.value.close();
    }

    const cancel = () => { 
        dialogRef.value.close();
    }

    onBeforeMount(() => {
        
    });
</script>

<template>
    <div class="container">
        <div class="content flex-center">
            <div v-if="systemPromptsVisible" class="system-prompts-container fill" style="border: 2px solid red;">
            </div>

            <div v-else class="prompts-container fill">
                <SavedPromptItem class="promptItem"></SavedPromptItem>

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
    border-radius: 8px;
    padding: 20px;
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

.prompts-container {
}

.promptItem {
    height: 50px;
}
</style>
