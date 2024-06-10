import { ref } from 'vue';
import axios from 'axios';
import { getCookie } from '../../composables/utils/webUtils.js';

export default function useEndpointService() {
    const baseUrl = 'https://stubgpt.com:5111';
    // const baseUrl = 'https://localhost:5111';

    // async function getData(endpoint) {
    //     const response = ref(null);

    //     try 
    //     {
    //         const client = axios.create({ baseURL: 'https://stubgpt.com:5111' });
    //         // const client = axios.create({ baseURL: 'https://localhost:5111' });
    //         const sessionToken = getCookie('SessionToken');
    //         const headers = sessionToken ? { 'Authorization': `Bearer ${sessionToken}` } : { };
    //         response.value = await client.get(endpoint, { headers: headers });
    //     } 
    //     catch (err) {
    //         console.log(err);
    //     }

    //     return response.value;
    // }

    // async function postData(endpoint, data) {
    //     const response = ref(null);

    //     try 
    //     {
    //         const client = axios.create({ baseURL: 'https://stubgpt.com:5111', headers: { 'Content-Type': 'application/json' } });
    //         // const client = axios.create({ baseURL: 'https://localhost:5111', headers: { 'Content-Type': 'application/json' } });
    //         const sessionToken = getCookie('SessionToken');
    //         const headers = sessionToken ? { 'Authorization': `Bearer ${sessionToken}` } : { };
    //         response.value = await client.post(endpoint, data, { headers: headers });
    //     } 
    //     catch (err) {
    //         console.log(err);
    //     }

    //     return response.value;
    // }

    async function getData(endpoint) {
        const sessionToken = getCookie('SessionToken');

        const options = {
            method: 'GET',
            headers: sessionToken ? { 'Content-Type': 'application/json', 'Authorization': `Bearer ${sessionToken}` } : { 'Content-Type': 'application/json' },
          };

        const response = await fetch(baseUrl + endpoint, options);
        return response;
    }

    async function postData(endpoint, data) {
        const sessionToken = getCookie('SessionToken');

        const options = {
            method: 'POST',
            headers: sessionToken ? { 'Content-Type': 'application/json', 'Authorization': `Bearer ${sessionToken}` } : { 'Content-Type': 'application/json' },
            body: JSON.stringify(data)
          };

          const response = await fetch(baseUrl + endpoint, options);
          return response;
    }

    return { getData, postData };
}


