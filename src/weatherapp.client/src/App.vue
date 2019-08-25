<template>
  <div class="container-fluid" id="app">
    <div class="row p-2">
      <div class="col-xs-12 col-md-12 col-sm-12">
        <SearchBar
          v-on:weatherReceived="handleWeather"
          v-on:errorFetchingWeather="handleErrorFetchingWeather"
        />
        <div class="row p-4">
          <div class="col">
            <SwitchButton v-on:temperatureSignChanged="changeTemperatureSign" />
          </div>
          <div class="col">
            <button class="btn btn-outline-light" v-on:click="toggleHistoryTab">
              <i v-if="!isHistoryTabOpened" class="fas fa-sort-down p-1"></i>
              <i v-if="isHistoryTabOpened" class="fas fa-sort-up p-1"></i>
              <span>History</span>
            </button>
          </div>
        </div>
        <div v-if="weatherForecasts" class="row">
          <div class="col">
            <h2>{{city.name}}</h2>
          </div>
        </div>
        <div class="row px-4">
          <ForecastCard
            class="col-xs-12 col-md-4 col-sm-6"
            v-for="(forecast, index) in weatherForecasts"
            v-bind:key="index"
            v-bind:forecast="forecast"
            v-bind:isFahrenheit="isFahrenheit"
          />
        </div>
        <div v-if="showError" class="row p-4">
          <h3>Error happened while fetching data. Please try later.</h3>
        </div>
      </div>
    </div>
    <div class="container-fluid">
      <Charts ref="myChart" />
    </div>
    <div class="container">
      <div v-if="isHistoryTabOpened" class="col-xs-12 col-md-12 col-sm-12">
        <HistoryTab v-bind:history="history" v-bind:isFahrenheit="isFahrenheit" />
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
import Charts from "./components/Charts";

export default {
  name: "app",
  components: {
    HistoryTab,
    SearchBar,
    ForecastCard,
    SwitchButton,
    Charts
  },
  data() {
    return {
      weatherForecasts: [],
      city: {},
      items: [{ message: "Foo", id: 1 }, { message: "Bar", id: 2 }],
      isHistoryTabOpened: false,
      isFahrenheit: true,
      showError: false,
      history: []
    };
  },
  methods: {
    setWeather(data) {
      this.weatherForecasts = data.forecasts;
      this.city = data.city;
    },
    addHistory() {
      const history = {
        humidity: this.weatherForecasts[0].averagedHumidity,
        temperature: this.weatherForecasts[0].averagedTemperature,
        date: this.weatherForecasts[0].date,
        cityName: this.city.name
      };

      WeatherService.addHistory(history).then(response => this.updateHistory());
    },
    handleWeather(data) {
      this.showError = false;
      this.setWeather(data);
      this.setGraph(data.forecasts);
      this.addHistory();
    },
    setGraph(data) {
      const temperatures = data.map(x => x.averagedTemperature);
      const humidities = data.map(x => x.averagedHumidity);
      this.$refs.myChart.updateGraph(
        temperatures,
        humidities,
        data.map(x => x.date),
        this.isFahrenheit
      );
    },
    handleErrorFetchingWeather(error) {
      this.weatherForecasts = [];
      this.showError = true;
    },
    toggleHistoryTab() {
      this.isHistoryTabOpened = !this.isHistoryTabOpened;
    },
    updateHistory() {
      WeatherService.getHistory().then(response => (this.history = response));
    },
    changeTemperatureSign() {
      this.isFahrenheit = !this.isFahrenheit;
      this.setGraph(this.weatherForecasts);
    }
  },
  created() {
    this.updateHistory();
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
  background-color: #343d4b;
}
html,
body {
  height: 100%;
  background-color: #343d4b !important;
}
</style>
