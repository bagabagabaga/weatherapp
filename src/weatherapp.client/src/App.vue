<template>
  <div id="app">
    <div class="col">
      <SearchBar v-on:weatherReceived="setWeather" />
    </div>
    <div id="change-temperature-scale">
      <SwitchButton />
    </div>

    <div class="row">
      <ForecastCard
        class="col-xs-12 col-md-4 col-sm-6"
        v-for="(forecast, index) in weatherData"
        v-bind:key="index"
        v-bind:forecast="forecast"
      />
    </div>
  </div>
</template>

<script>
import SearchBar from "./components/SearchBar";
import ForecastCard from "./components/ForecastCard";
import SwitchButton from "./components/SwitchButton";

export default {
  name: "app",
  components: {
    SearchBar,
    ForecastCard,
    SwitchButton
  },
  data() {
    return {
      weatherData: [],
      items: [{ message: "Foo", id: 1 }, { message: "Bar", id: 2 }]
    };
  },
  methods: {
    setWeather(data) {
      this.weatherData = data;
      console.log(data);
    }
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
  color: #2c3e50;
  padding: 5em;
  background-color: #0a3354;
}
html,
body {
  height: 100%;
  background-color: #0a3354;
}
#change-temperature-scale {
  margin: 2em auto;
}
</style>
