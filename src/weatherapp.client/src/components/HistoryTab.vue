<template>
  <div>
    <h2>HISTORY</h2>
    <ul>
      <li v-for="(item, index) in history" v-bind:key="index">
        <div class="row">
          <div class="col">
            <span>{{item.cityName}}</span>
            <p class="text-muted small">{{date(item.date)}}</p>
          </div>
        </div>
        <div class="row">
          <div class="col">
            <MeterologicalConditionCard
              icon="fas fa-thermometer-half"
              v-bind:value="item.temperature"
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
    history: Array
  },
  methods: {
    date: function(date) {
      const options = { year: "numeric", month: "short", day: "numeric" };
      let dateFormatted = new Date(date).toLocaleDateString("en", options);
      return dateFormatted;
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
