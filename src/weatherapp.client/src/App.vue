<template>
  <div class="container-fluid" id="app">
    <div class="row p-2">
      <div v-if="isHistoryTabOpened" class="col-xs-12 col-md-12 col-sm-12">
        <HistoryTab v-bind:history="history" />
      </div>
      <div class="col-xs-12 col-md-12 col-sm-12">
        <SearchBar v-on:weatherReceived="handleWeather" />
        <div class="row p-4">
          <div class="col">
            <SwitchButton />
          </div>
          <div class="col">
            <button class="btn btn-light" v-on:click="toggleHistoryTab">History</button>
          </div>
        </div>
        <div class="row px-4">
          <ForecastCard
            class="col-xs-12 col-md-4 col-sm-6"
            v-for="(forecast, index) in weatherData"
            v-bind:key="index"
            v-bind:forecast="forecast"
          />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import SearchBar from "./components/SearchBar";
import ForecastCard from "./components/ForecastCard";
import SwitchButton from "./components/SwitchButton";
import HistoryTab from "./components/HistoryTab";
import WeatherService from "./services/WeatherService";

export default {
  name: "app",
  components: {
    HistoryTab,
    SearchBar,
    ForecastCard,
    SwitchButton
  },
  data() {
    return {
      weatherData: [],
      items: [{ message: "Foo", id: 1 }, { message: "Bar", id: 2 }],
      isHistoryTabOpened: false,
      history: []
    };
  },
  methods: {
    setWeather(data) {
      this.weatherData = data;
    },
    addHistory() {
      console.log(
        {
          "humidity":this.weatherData[0].averagedHumidity,
          "temperature":this.weatherData[0].averagedTemperature,
          "date":this.weatherData[0].date,
          "cityName":"name"
      });
    },
    handleWeather(data) {
      this.setWeather(data);
      this.addHistory();
    },
    toggleHistoryTab() {
      this.isHistoryTabOpened = !this.isHistoryTabOpened;
    }
  },
  created() {
    WeatherService.getHistory().then(response => (this.history = response));
  }
};
</script>


<style src="vue-multiselect/dist/vue-multiselect.min.css"></style>
<style>
#app {
  font-family: "Avenir", Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #fff;
  padding: 5em;
  background-color: #0a3354;
}
html,
body {
  height: 100%;
  background-color: #0a3354 !important;
}
.sidebar {
  position: fixed;
  left: 0;
  padding: 0.5rem;
}
</style>
