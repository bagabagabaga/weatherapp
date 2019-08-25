<template>
  <div class="card my-card">
    <div class="card-content">
      <div class="row">
        <div class="col">
          <span class="font-weight-bold">{{day}}</span>
          <p class="text-muted">{{date}}</p>
        </div>
      </div>

      <div class="row">
        <MeterologicalConditionCard
          class="col"
          icon="fas fa-thermometer-half"
          v-bind:value="temperature"
          v-bind:sign="sign"
        />
        <MeterologicalConditionCard
          class="col"
          icon="fas fa-tint"
          v-bind:value="forecast.averagedHumidity"
          sign="%"
        />
        <MeterologicalConditionCard
          class="col"
          icon="fas fa-wind"
          v-bind:value="forecast.averagedWind"
          sign="㎧"
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
    },
    temperature: function() {
      if (this.isFahrenheit) {
        return ((this.forecast.averagedTemperature - 273.15) * 9) / 5 + 32;
      } else {
        return this.forecast.averagedTemperature - 273.15;
      }
    },
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
.my-card {
  border: 3px solid #343d4b;
  color: #c2ccd4;
  background-color: #222831;
}
.weather-icon {
  font-size: 3em;
}
</style>