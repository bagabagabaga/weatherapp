import Vue from "vue";
import App from "./App.vue";

import BootstrapVue from "bootstrap-vue";
import "@fortawesome/fontawesome-free/css/all.css";
import "bootstrap/dist/css/bootstrap.css";
import "bootstrap-vue/dist/bootstrap-vue.css";

import axios from "axios";


axios.defaults.baseURL = "https://localhost:8001/api";

Vue.config.productionTip = false;
Vue.use(BootstrapVue);

new Vue({
  render: h => h(App)
}).$mount("#app");
