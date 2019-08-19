import axios from 'axios'

export default {
    getWeather(cityId, postalCode) {
        debugger;
        return axios.get('/weather/forecast?cityId=' + cityId + postalCode)
            .then(response => {
                return response.data
            })
    }
}