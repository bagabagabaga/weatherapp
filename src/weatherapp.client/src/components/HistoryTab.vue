<template>
  <div>
    <h2>HISTORY</h2>
    <ul>
      <li v-for="(item, index) in history" v-bind:key="index">
        <div class="row">
          <div class="col-md-6 align-self-center">
            <span class="align-middle">{{item.cityName}}</span>
            <p class="text-muted small">{{date(item.date)}}</p>
          </div>

          <div class="col">
            <MeterologicalConditionCard
              icon="fas fa-thermometer-half"
              v-bind:value="scaleTemperature(item.temperature, isFahrenheit)"
              v-bind:sign="sign"
            />
          </div>
          <div class="col">
            <MeterologicalConditionCard icon="fas fa-tint" v-bind:value="item.humidity" />
          </div>
        </div>
      </li>
    </ul>
  </div>
</template>

<script>
import MeterologicalConditionCard from "./MeterologicalConditionCard";

export default {
  name: "HistoryTab",
  components: {
    MeterologicalConditionCard
  },
  props: {
    history: Array,
    isFahrenheit: Boolean
  },
  methods: {
    date: function(date) {
      const options = { year: "numeric", month: "short", day: "numeric" };
      let dateFormatted = new Date(date).toLocaleDateString("en", options);
      return dateFormatted;
    },
    scaleTemperature: function(temperature, isFahrenheit) {
      if (isFahrenheit) {
        return ((temperature - 273.15) * 9) / 5 + 32;
      } else {
        return temperature - 273.15;
      }
    }
  },
  computed: {
    sign: function() {
      if (this.isFahrenheit) {
        return "°F";
      } else {
        return "°C";
      }
    }
  }
};
</script>

<style scoped>
.sidebar {
  padding: 0.5rem;
}
ul {
  overflow-y: auto;
  overflow-x: hidden;
  height: 300px;
  list-style-type: none;
  padding: 0;
  background-color: #ffffff;
}
li {
  border: 1px solid black;
  background-color: #222831;
  margin: 0.5em;
}
</style>
