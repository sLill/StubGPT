// //////////////////////////////////
// https://primevue.org/
// //////////////////////////////////

import PrimeVue from 'primevue/config';

// CSS
import 'primevue/resources/themes/aura-dark-indigo/theme.css'
import 'primevue/resources/primevue.min.css';
import 'primeicons/primeicons.css';

// Imports
//import Avatar from 'primevue/avatar';
//import AvatarGroup from 'primevue/avatargroup';
import Badge from 'primevue/badge';
import BlockUI from 'primevue/blockui';
import Button from 'primevue/button';
//import Calendar from 'primevue/calendar';
//import Card from 'primevue/card';
//import Carousel from 'primevue/carousel';
//import CascadeSelect from 'primevue/cascadeselect';
//import Chart from 'primevue/chart';
import Checkbox from 'primevue/checkbox';
//import ColorPicker from 'primevue/colorpicker';
//import Column from 'primevue/column';
//import ColumnGroup from 'primevue/columngroup';
import ConfirmDialog from 'primevue/confirmdialog';
import ConfirmPopup from 'primevue/confirmpopup';
import ContextMenu from 'primevue/contextmenu';
//import DataTable from 'primevue/datatable';
//import DataView from 'primevue/dataview';
//import DataViewLayoutOptions from 'primevue/dataviewlayoutoptions';
import Dialog from 'primevue/dialog';
import Divider from 'primevue/divider';
//import Dock from 'primevue/dock';
import Dropdown from 'primevue/dropdown';
import DynamicDialog from 'primevue/dynamicdialog';
import FloatLabel from 'primevue/floatlabel';
//import IconField from 'primevue/iconfield';
import Image from 'primevue/image';
//import InlineMessage from 'primevue/inlinemessage';
import InputIcon from 'primevue/inputicon';
//import InputGroup from 'primevue/inputgroup';
//import InputGroupAddon from 'primevue/inputgroupaddon';
//import InputMask from 'primevue/inputmask';
//import InputNumber from 'primevue/inputnumber';
//import InputSwitch from 'primevue/inputswitch';
import InputText from 'primevue/inputtext';
//import Listbox from 'primevue/listbox';
//import Menu from 'primevue/menu';
//import Menubar from 'primevue/menubar';
//import MeterGroup from 'primevue/metergroup';
import Message from 'primevue/message';
//import OverlayPanel from 'primevue/overlaypanel';
//import Paginator from 'primevue/paginator';
//import Panel from 'primevue/panel';
//import PanelMenu from 'primevue/panelmenu';
//import PickList from 'primevue/picklist';
//import Password from 'primevue/password';
//import ProgressBar from 'primevue/progressbar';
//import ProgressSpinner from 'primevue/progressspinner';
import RadioButton from 'primevue/radiobutton';
//import Row from 'primevue/row';
//import ScrollPanel from 'primevue/scrollpanel';
//import SelectButton from 'primevue/selectbutton';
//import Sidebar from 'primevue/sidebar';
import Slider from 'primevue/slider';
//import SpeedDial from 'primevue/speeddial';
//import SplitButton from 'primevue/splitbutton';
//import Splitter from 'primevue/splitter';
//import SplitterPanel from 'primevue/splitterpanel';
//import Steps from 'primevue/steps';
//import TabMenu from 'primevue/tabmenu';
//import TabView from 'primevue/tabview';
//import TabPanel from 'primevue/tabpanel';
//import Tag from 'primevue/tag';
//import Toolbar from 'primevue/toolbar';
import Tooltip from 'primevue/tooltip';
import TextArea from 'primevue/textarea';
//import Timeline from 'primevue/timeline';
import ToggleButton from 'primevue/togglebutton';
//import Tree from 'primevue/tree';
//import TriStateCheckbox from 'primevue/tristatecheckbox';
import Toast from 'primevue/toast';

import ConfirmationService from 'primevue/confirmationservice';
import DialogService from 'primevue/dialogservice';
import ToastService from 'primevue/toastservice';

import BadgeDirective from 'primevue/badgedirective';

export default {
    install(app) {
        app.use(PrimeVue);
        app.use(ConfirmationService);
        app.use(DialogService);
        app.use(ToastService);

        app.directive('tooltip', Tooltip);
        app.directive('badge', BadgeDirective);

        //app.component('Avatar', Avatar);
        //app.component('AvatarGroup', AvatarGroup);
        app.component('Badge', Badge);
        app.component('BlockUI', BlockUI);
        app.component('Button', Button);
        //app.component('Calendar', Calendar);
        //app.component('Card', Card);
        //app.component('Carousel', Carousel);
        //app.component('CascadeSelect', CascadeSelect);
        //app.component('Chart', Chart);
        app.component('Checkbox', Checkbox);
        //app.component('ColorPicker', ColorPicker);
        //app.component('Column', Column);
        //app.component('ColumnGroup', ColumnGroup);
        app.component('ConfirmDialog', ConfirmDialog);
        app.component('ConfirmPopup', ConfirmPopup);
        app.component('ContextMenu', ContextMenu);
        //app.component('DataTable', DataTable);
        //app.component('DataView', DataView);
        //app.component('DataViewLayoutOptions', DataViewLayoutOptions);
        app.component('Dialog', Dialog);
        app.component('Divider', Divider);
        //app.component('Dock', Dock);
        app.component('Dropdown', Dropdown);
        app.component('DynamicDialog', DynamicDialog);
        app.component('FloatLabel', FloatLabel);
        //app.component('IconField', IconField);
        app.component('Image', Image);
        //app.component('InlineMessage', InlineMessage);
        app.component('InputIcon', InputIcon);
        //app.component('InputGroup', InputGroup);
        //app.component('InputGroupAddon', InputGroupAddon);
        //app.component('InputMask', InputMask);
        //app.component('InputNumber', InputNumber);
        //app.component('InputSwitch', InputSwitch);
        app.component('InputText', InputText);
        //app.component('Listbox', Listbox);
        //app.component('Menu', Menu);
        //app.component('Menubar', Menubar);
        //app.component('MeterGroup', MeterGroup);
        app.component('Message', Message);
        //app.component('OverlayPanel', OverlayPanel);
        //app.component('Paginator', Paginator);
        //app.component('Panel', Panel);
        //app.component('PanelMenu', PanelMenu);
        //app.component('PickList', PickList);
        //app.component('Password', Password);
        //app.component('ProgressBar', ProgressBar);
        //app.component('ProgressSpinner', ProgressSpinner);
        app.component('RadioButton', RadioButton);
        //app.component('Row', Row);
        //app.component('ScrollPanel', ScrollPanel);
        //app.component('SelectButton', SelectButton);
        //app.component('Sidebar', Sidebar);
        app.component('Slider', Slider);
        //app.component('SpeedDial', SpeedDial);
        //app.component('SplitButton', SplitButton);
        //app.component('Splitter', Splitter);
        //app.component('SplitterPanel', SplitterPanel);
        //app.component('Steps', Steps);
        //app.component('TabMenu', TabMenu);
        //app.component('TabView', TabView);
        //app.component('TabPanel', TabPanel);
        //app.component('Tag', Tag);
        //app.component('Toolbar', Toolbar);
        app.component('Tooltip', Tooltip);
        app.component('TextArea', TextArea);
        //app.component('Timeline', Timeline);
        app.component('ToggleButton', ToggleButton);
        //app.component('Tree', Tree);
        //app.component('TriStateCheckbox', TriStateCheckbox);
        app.component('Toast', Toast);
    },
};
