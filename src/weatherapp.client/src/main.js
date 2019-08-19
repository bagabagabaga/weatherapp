import Vue from 'vue'
import App from './App.vue'
import "bulma/css/bulma.css"
import "@fortawesome/fontawesome-free/css/all.css"
import axios from 'axios'

axios.defaults.baseURL = 'https://localhost:8001/api'

Vue.config.productionTip = false

new Vue({
  render: h => h(App),
}).$mount('#app')
