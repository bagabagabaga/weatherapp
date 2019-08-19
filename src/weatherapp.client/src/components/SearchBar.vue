<template>
  <div class="column">
    <div class="column is-7">
      <label class="typo__label" for="ajax">Async multiselect</label>
      <multiselect
        v-model="selectedCities"
        id="ajax"
        label="name"
        track-by="code"
        placeholder="Type to search"
        open-direction="bottom"
        :options="cities"
        :multiple="true"
        :searchable="true"
        :loading="isLoading"
        :internal-search="false"
        :clear-on-select="false"
        :close-on-select="false"
        :options-limit="50"
        :max-height="400"
        :show-no-results="false"
        :hide-selected="true"
        @search-change="asyncFind"
      >
        <template slot="tag" slot-scope="{ option, remove }">
          <span class="custom__tag">
            <span>{{ option.name }}</span>
            <span class="custom__remove" @click="remove(option)">❌</span>
          </span>
        </template>
        <template slot="clear" slot-scope="props">
          <div
            class="multiselect__clear"
            v-if="selectedCities.length"
            @mousedown.prevent.stop="clearAll(props.search)"
          ></div>
        </template>
        <span slot="noResult">Oops! No cities found.</span>
      </multiselect>
    </div>
    <div class="column is-3">
      <input
        class="input is-medium"
        placeholder="Postal Code"
        v-model="postalCode"
        @keypress="isNumber($event)"
      />
    </div>
    <button v-on:click="getWeatherData" class="btn button is-large">
      <i class="fas fa-umbrella"></i> Get Weather Data
    </button>
  </div>
</template>

<script>
import Multiselect from "vue-multiselect";
import CitiesService from "../services/CitiesService";
import WeatherService from "../services/WeatherService";

export default {
  name: "SearchBar",
  components: {
    Multiselect
  },
  data() {
    return {
      selectedCities: [],
      cities: [],
      isLoading: false,
      postalCode: "",
      weatherData: {}
    };
  },
  methods: {
    asyncFind(query) {
      this.isLoading = true;
      CitiesService.getCities(query)
        .then(response => {
          this.cities = response;
          this.isLoading = false;
        })
        .catch(error => this.console.log(error))
        .finally(() => {
          this.loading = false;
        });
    },
    clearAll() {
      this.selectedCities = [];
    },
    isNumber: function(evt) {
      evt = evt ? evt : window.event;
      var charCode = evt.which ? evt.which : evt.keyCode;
      if (charCode < 48 || charCode > 57) {
        evt.preventDefault();
      } else {
        return true;
      }
    },
    getWeatherData() {
      WeatherService.getWeather(
        this.selectedCities[0].id,
        this.postalCode
      ).then(response => (this.weatherData = response));
    }
  }
};
</script>