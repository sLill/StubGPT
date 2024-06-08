<script setup>
    import { ref, onMounted, onBeforeMount } from 'vue';
    import useEndpointService from '/src/composables/services/useEndpointService.js';

    const endpointService = useEndpointService();

    const modelRef = ref(null);

    const props = defineProps({
        model: { type: Object }
    });

    const emit = defineEmits(['insertButton-pressed', 'focus', 'blur']);

    const insertButton_Pressed = () => {
        emit('insertButton-pressed', modelRef.value);
    };

    const input_blur = async () => {
        await endpointService.postData('/api/v1/user/updateSavedPrompt', { Prompt: modelRef.value });
        emit('blur', modelRef.value);
    };

    onBeforeMount(() => {
        modelRef.value = props.model;
    });

    onMounted(async () => {
    });
</script>

<template>
    <div class="wrapper">
        <div class="container-component fill">
            <div class="prompt">
                <div class="flex-center" style="font-size: 1.5rem;">#</div>
                <InputText v-model="modelRef.shortcut" @focus="emit('focus', modelRef.value)" @blur.capture="input_blur"></InputText>
                <InputText v-model="modelRef.text" @focus="emit('focus', modelRef.value)" @blur.capture="input_blur"></InputText>
                <Button @click="insertButton_Pressed">
                    <FontAwesomeIcon :icon="['fas', 'arrow-turn-up']" />
                </Button>
            </div>
        </div>
    </div>
</template>

<style scoped>
.container-component {  

}

.prompt {
    display: grid;
    grid-template: auto / auto 120px 1fr 50px;
    grid-gap: 4px;
}
</style>
