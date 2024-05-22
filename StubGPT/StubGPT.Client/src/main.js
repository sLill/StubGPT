import './assets/main.css'

import { createApp } from 'vue'
import App from './App.vue'
import primevuePlugins from './primevue-plugins';

//FontAwesome
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'
import { fas } from '@fortawesome/free-solid-svg-icons';
import { far } from '@fortawesome/free-regular-svg-icons';
import { fab } from '@fortawesome/free-brands-svg-icons';
import { library } from '@fortawesome/fontawesome-svg-core';
library.add(fas, far, fab);


const app = createApp(App);
app.use(primevuePlugins);
app.component('FontAwesomeIcon', FontAwesomeIcon);
app.mount('#app');
