<script setup>
    import { ref, onMounted } from 'vue';

    const shortcut = ref('');
    const prompt = ref('');

    const props = defineProps({
        model: { type: Object }
    });

    onMounted(async () => {
        shortcut.value = props.model.shortcut;
        prompt.value = props.model.prompt;
    });
</script>

<template>
    <div class="wrapper">
        <div class="container-component fill">
            <div class="prompt">
                <div class="flex-center" style="font-size: 1.5rem;">#</div>
                <InputText v-model="shortcut"></InputText>
                <InputText v-model="prompt"></InputText>
                <Button>
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
    grid-template: auto / auto 140px 1fr 50px;
    grid-gap: 4px;
}
</style>
