import { ref } from 'vue';
import { useDialog } from 'primevue/usedialog';

export default function useDialogService() {

    const dynamicDialog = useDialog();
    const dialogRef = ref(null);

    const showDynamicDialog = ({ content, data, callbacks }) => {
        dialogRef.value = dynamicDialog.open(content, {
            props: {
                style: {
                    
                },
                breakpoints: {

                },
                closable: false,
                closeOnEscape: false,
                showHeader: false,
                modal: true,
                unstyled: true
            },
            data: data,
            callbacks: callbacks
        });
    };

    return { showDynamicDialog };
}


