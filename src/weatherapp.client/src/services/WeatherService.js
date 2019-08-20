import axios from 'axios'

export default {
    getWeather(cityId, postalCode) {
        debugger;
        let endpoint;
        if (cityId && postalCode) {
            endpoint = "/weather/forecast?" + "cityId=" + cityId + "&zipCode=" + postalCode;
        }
        else if (cityId) {
            endpoint = "/weather/forecast?" + "cityId=" + cityId;
        }
        else if (postalCode) {
            endpoint = "/weather/forecast?" + "zipCode=" + postalCode
        }
        if (endpoint) {
            return axios.get(endpoint)
                .then(response => {
                    return response.data
                })
        }
    }
}