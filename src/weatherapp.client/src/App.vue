<template>
  <div id="app">
    <div class="columns">
      <SearchBar />
    </div>
    <div id="change-temperature-scale">
      <SwitchButton />
    </div>

    <div class="columns">
      <ForecastCard class="column is-7" day="Today" />
      <ForecastCard class="column" day="Tomorrow" />
    </div>
    <div class="columns">
      <ForecastCard class="column is-3" day="Tuesday" />
      <ForecastCard class="column is-3" day="Thursday" />
      <ForecastCard class="column is-3" day="Wednesday" />
      <ForecastCard class="column is-3" day="Monday" />
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
