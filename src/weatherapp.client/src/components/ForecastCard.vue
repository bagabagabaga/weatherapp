<template>
  <div class="card my-card">
    <div class="card-content">
      <div class="row">
        <div class="col">
          <div class="weather-icon">
            <i class="fas fa-poo-storm"></i>
          </div>
        </div>
        <div class="col">
          <span>{{day}}</span>
          <p>{{date}}</p>
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
    forecast: {}
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
  color: #c2ccd4;
  background-color: #133a5b;
}
.weather-icon {
  font-size: 3em;
}
</style>