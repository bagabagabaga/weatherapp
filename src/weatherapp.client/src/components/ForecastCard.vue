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
          <p>24th september 2019</p>
        </div>
      </div>

      <div class="row">
        <MeterologicalConditionCard />
        <MeterologicalConditionCard />
        <MeterologicalConditionCard />
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
    day: String
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