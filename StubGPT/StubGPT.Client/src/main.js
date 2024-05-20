import './assets/main.css'

import { createApp } from 'vue'
import App from './App.vue'
import primevuePlugins from './primevue-plugins';

const app = createApp(App);
app.use(primevuePlugins);
app.mount('#app');
