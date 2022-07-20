import axios from 'axios';

const getCatalogUrl = async () => {
  // console.log(`${window.location.origin}/config`);
  // const response = await axios.get(`${window.location.origin}/config`)
  // return response.data.json()["catalogServiceUrl"];
  return "http://catalog:5000/api/v1/catalog";
}

const CatalogService = {
  getAll: async () => {
    const catalogUrl = await getCatalogUrl();
    console.log(catalogUrl);
    const resp = await axios.get(catalogUrl);
    return resp.data;
  },
};

export default CatalogService;