<template>
  <div class="card my-card">
    <div class="card-content">
      <div class="row">
        <div class="col">
          <span class="font-weight-bold">{{day}}</span>
          <p class="text-muted">{{date}} {{isFahrenheit}}</p>
        </div>
      </div>

      <div class="row">
        <MeterologicalConditionCard
          class="col"
          icon="fas fa-thermometer-half"
          v-bind:value="forecast.averagedTemperature"
        />
        <MeterologicalConditionCard
          class="col"
          icon="fas fa-tint"
          v-bind:value="forecast.averagedHumidity"
        />
        <MeterologicalConditionCard
          class="col"
          icon="fas fa-wind"
          v-bind:value="forecast.averagedWind"
        />
      </div>
    </div>
  </div>
</template>

<script>
import MeterologicalConditionCard from "./MeterologicalConditionCard";

export default {
  name: "ForecastCard",
  components: {
    MeterologicalConditionCard
  },
  props: {
    forecast: Object,
    isFahrenheit: Boolean
  },
  computed: {
    date: function() {
      const options = { year: "numeric", month: "short", day: "numeric" };
      let date = new Date(this.forecast.date).toLocaleDateString("en", options);
      return date;
    },
    day: function() {
      return new Date(this.forecast.date).toLocaleDateString("en", {
        weekday: "long"
      });
    }
  }
};
</script>

<style scoped>
.my-card {
  border: 3px solid #343d4b;
  color: #c2ccd4;
  background-color: #222831;
}
.weather-icon {
  font-size: 3em;
}
</style>