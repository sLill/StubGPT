import { ref } from 'vue';
import axios from 'axios';
import { getCookie } from '../../composables/utils/webUtils.js';

export default function useEndpointService() {
    async function getData(endpoint) {
        const response = ref(null);

        try 
        {
            const client = axios.create({ baseURL: 'https://stubgpt.com:5111' });
            //const client = axios.create({ baseURL: 'https://localhost:5111' });
            const sessionToken = getCookie('SessionToken');
            const headers = sessionToken ? { 'Authorization': `Bearer ${sessionToken}`, 'Access-Control-Allow-Origin': '*' } : { 'Access-Control-Allow-Origin': '*' };
            response.value = await client.get(endpoint, { headers: headers });
        } 
        catch (err) {
            console.log(err);
        }

        return response.value;
    }

    async function postData(endpoint, data) {
        const response = ref(null);

        try 
        {
            const client = axios.create({ baseURL: 'https://stubgpt.com:5111', headers: { 'Content-Type': 'application/json' } });
            //const client = axios.create({ baseURL: 'https://localhost:5111', headers: { 'Content-Type': 'application/json' } });
            const sessionToken = getCookie('SessionToken');
            const headers = sessionToken ? { 'Authorization': `Bearer ${sessionToken}`, 'Access-Control-Allow-Origin': '*' } : { 'Access-Control-Allow-Origin': '*' };
            response.value = await client.post(endpoint, data, { headers: headers });
        } 
        catch (err) {
            console.log(err);
        }

        return response.value;
    }

    return { getData, postData, error };
}


