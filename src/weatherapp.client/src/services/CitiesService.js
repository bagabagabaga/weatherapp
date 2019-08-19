import axios from 'axios'

export default {
    getCities(query) {
        if (query.trim()) {
            return axios.get('/cities?name=' + query)
                .then(response => {
                    return response.data
                })
        }

        return Promise.resolve([]);
    }
}