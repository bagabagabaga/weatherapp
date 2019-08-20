<template>
  <div id="app">
    <div class="col">
      <SearchBar />
    </div>
    <div id="change-temperature-scale">
      <SwitchButton />
    </div>

    <div class="row">
      <ForecastCard class="col md-5" day="Today" />
      <ForecastCard class="col md-3" day="Tomorrow" />
    </div>
    <div class="row">
      <ForecastCard class="col" day="Tuesday" />
      <ForecastCard class="col" day="Thursday" />
      <ForecastCard class="col" day="Wednesday" />
      <ForecastCard class="col" day="Monday" />
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
