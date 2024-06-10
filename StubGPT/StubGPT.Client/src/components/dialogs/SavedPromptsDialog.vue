<script setup>
    import { ref, inject, onBeforeMount } from 'vue';
    import useEndpointService from '/src/composables/services/useEndpointService.js';
    import SavedPromptItem from '/src/components/SavedPromptItem.vue';

    const endpointService = useEndpointService();

    const dialogRef = inject('dialogRef');

    const promptNavOptions = ref([
        { icon: 'user', value: 'User' },
        { icon: 'laptop', value: 'System' }
    ]);

    const promptNav = ref(promptNavOptions.value[0]);
    const savedPrompts = ref([]);
    const selectedPrompt = ref();

    // Methods
    const loadPrompts = async() => {
        const savedPromptsResponse = await endpointService.getData('/api/v1/user/getSavedPrompts');
        const savedPromptsData = await savedPromptsResponse.json();
        savedPrompts.value = savedPromptsData.prompts;
    };

    const cancel = () => { 
        dialogRef.value.close();
    };

    const savedPrompt_Selected = (prompt) => {
        dialogRef.value.options.callbacks.promptSelected(prompt);
        dialogRef.value.close();
    };

    const addPrompt_Clicked = async (promptType) => {
        await endpointService.postData('/api/v1/user/addSavedPrompt', { PromptType: promptType });
        await loadPrompts();
    };

    const deletePrompt_Clicked = async() => {
        if (selectedPrompt.value) {
            await endpointService.postData('/api/v1/user/removeSavedPrompt', { prompt: selectedPrompt.value });
            await loadPrompts();
        }
    };

    onBeforeMount(async () => {
        await loadPrompts();      
    });
</script>

<template>
    <div class="container">
        <div class="nav-container">
            <SelectButton class="nav-select fill flex-center" 
                        v-model="promptNav" 
                        :options="promptNavOptions" 
                        optionLabel="value" 
                        dataKey="value" 
                        aria-labelledby="custom"
                        :pt="{
                            button: 'nav-select-button fill'
                        }">
                <template #option="slotProps">
                    <FontAwesomeIcon :icon="['fas', slotProps.option.icon]" />
                </template>
            </SelectButton>
        </div>

        <div class="content-container flex-center">
            <div class="content fill">
                <div v-if="promptNav == promptNavOptions[0]" class="prompts-container fill">
                    <SavedPromptItem v-for="prompt in savedPrompts.filter(x => x.promptType == 0)" 
                        :key="prompt" 
                        :model="prompt" 
                        class="promptItem"
                        @focus="selectedPrompt = prompt"
                        @blur.capture="selectedPrompt = null"
                        @insertButton-pressed="savedPrompt_Selected">
                    </SavedPromptItem>

                    <div style="display: flex; justify-content: center; gap: 8px;">
                        <Button class="add-prompt" @click="addPrompt_Clicked('User')">
                            <FontAwesomeIcon :icon="['fas', 'plus']" />
                        </Button>
                        <Button v-if="selectedPrompt != null" class="delete-prompt" @click="deletePrompt_Clicked">
                            <FontAwesomeIcon :icon="['fas', 'trash']" />
                        </Button>
                    </div>
                </div>

                <div v-else class="prompts-container fill">
                    <SavedPromptItem v-for="prompt in savedPrompts.filter(x => x.promptType == 1)" 
                        :key="prompt" 
                        :model="prompt" 
                        class="promptItem"
                        @focus="selectedPrompt = prompt"
                        @blur.capture="selectedPrompt = null"
                        @insertButton-pressed="savedPrompt_Selected">
                    </SavedPromptItem>

                    <div style="display: flex; justify-content: center; gap: 8px;">
                        <Button class="add-prompt" @click="addPrompt_Clicked('System')">
                            <FontAwesomeIcon :icon="['fas', 'plus']" />
                        </Button>
                        <Button v-if="selectedPrompt != null" class="delete-prompt" @click="deletePrompt_Clicked">
                            <FontAwesomeIcon :icon="['fas', 'trash']" />
                        </Button>
                    </div>
                </div>
            </div>
            <div class="options flex-center">
                <Button class="flex-center" style="padding: 12px;" label="Cancel" @click="cancel"/>
            </div>

        </div>
    </div>
</template>

<style scoped>
.container {
    display: grid;
    grid-template: 1fr / 100px 1fr;
    grid-template-areas: "nav main";
    grid-gap: 6px;

    width: 80vw;
    height: 70vh;
}

@media screen and (min-width: 1280px) {
    .container {
        max-width: 1024px;
    }
}

.nav-container {
    grid-area: nav;

    display: flex;
    flex-direction: column;

    .nav-select {
        display: flex;
        flex-direction: column;
        gap: 2px;
    }
}

.content-container {
    grid-area: main;

    display: grid;
    grid-gap: 10px;
    grid-template: 1fr auto / 1fr;
    grid-template-areas:
    "content"
    "options";

    border-radius: 8px;
    padding: 20px;

    background: var(--surface-50);

    .content {
        grid-area: content;
        flex-direction: column;
    }
}

.options {
    grid-area: options;
    justify-content: right;
    gap: 12px;
}

.prompts-container {
    .promptItem {
        height: 50px;
    }

    .add-prompt {
        border: none;
    }

    .delete-prompt {
        background: var(--red-500);
        border: none;
    }
}
</style>
