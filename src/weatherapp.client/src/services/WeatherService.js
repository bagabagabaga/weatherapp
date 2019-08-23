import axios from "axios";

export default {
  getWeather(cityId, postalCode) {
    let endpoint;
    if (cityId && postalCode) {
      endpoint =
        "/weather/forecast?" + "cityId=" + cityId + "&zipCode=" + postalCode;
    } else if (cityId) {
      endpoint = "/weather/forecast?" + "cityId=" + cityId;
    } else if (postalCode) {
      endpoint = "/weather/forecast?" + "zipCode=" + postalCode;
    }
    if (endpoint) {
      return axios.get(endpoint).then(response => {
        return response.data;
      });
    }
  },
  getHistory() {
    return axios.get("/weather/history").then(response => {
      return response.data;
    });
  },
  async addHistory(data) {
    return axios.post("/weather/history", data);
  }
};
