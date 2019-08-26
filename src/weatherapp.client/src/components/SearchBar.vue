<template>
  <div class="container">
    <div class="row m-3">
      <div class="col-xs-12 col-md-6 col-sm-6">
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
          class="is-danger"
          :loading="isLoading"
          :internal-search="false"
          :clear-on-select="true"
          :close-on-select="true"
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
      <div class="col-xs-6 col-md-6 col-sm-4 p-1">
        <input
          class="form-control"
          placeholder="Postal Code"
          v-model="postalCode"
          @keypress="isNumber($event)"
        />
      </div>
    </div>
    <div class="row">
      <div class="col">
        <button v-on:click="getWeatherData" class="btn btn-outline-light col col-md-2">
          <i class="fas fa-umbrella"></i> Get Weather Data
        </button>
      </div>
    </div>
    <b v-if="isInputInvalid" class="text-danger">Please find a valid city or type in a postal code.</b>
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
      isInputInvalid: false,
      postalCode: ""
    };
  },
  methods: {
    asyncFind(query) {
      this.isInputInvalid = false;
      this.isLoading = true;
      CitiesService.getCities(query)
        .then(response => {
          this.cities = response;
          this.isLoading = false;
        })
        .catch(error => console.log(error))
        .finally(() => {
          this.loading = false;
        });
    },
    clearAll() {
      this.selectedCities = [];
    },
    isNumber: function(evt) {
      this.isInputInvalid = false;
      evt = evt ? evt : window.event;
      var charCode = evt.which ? evt.which : evt.keyCode;
      if (charCode < 48 || charCode > 57) {
        evt.preventDefault();
      } else {
        return true;
      }
    },
    getWeatherData() {
      if (this.selectedCities.length > 0 || this.postalCode) {
        WeatherService.getWeather(
          this.selectedCities.length > 0 ? this.selectedCities[0].id : "",
          this.postalCode
        )
          .then(response => this.$emit("weatherReceived", response))
          .catch(error => this.$emit("errorFetchingWeather", error));
      } else {
        this.isInputInvalid = true;
      }
    }
  }
};
</script>

<style scoped>
#app {
  text-align: center;
  color: #2c3e50;
  padding: 5em;
  background-color: #0a3354;
}
html,
body {
  height: 100%;
  background-color: #0a3354 !important;
}
</style>