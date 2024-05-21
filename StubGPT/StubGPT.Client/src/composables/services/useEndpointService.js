import { ref } from 'vue';
import axios from 'axios';

export default function useEndpointService() {
    async function getData(endpoint) {
        const response = ref(null);

        try 
        {
            const client = axios.create({ baseURL: 'http://localhost:5110' });
            response.value = await client.get(endpoint);
        } 
        catch (err) {
            error.value = err.message;
            console.log(error);
        }

        return response.value;
    }

    async function postData(endpoint, data) {
        const response = ref(null);

        try 
        {
            const client = axios.create({ baseURL: 'http://localhost:5110', headers: { 'Content-Type': 'application/json' } });
            response.value = await client.post(endpoint, data);
        } 
        catch (err) {
            error.value = err.message;
            console.log(error);
        }

        return response.value;
    }

    return { getData, postData };
}


