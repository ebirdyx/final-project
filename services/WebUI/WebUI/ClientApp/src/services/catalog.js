import axios from 'axios';

const getCatalogUrl = async () => {
  // console.log(`${window.location.origin}/config`);
  // const response = await axios.get(`${window.location.origin}/config`)
  // return response.data.json()["catalogServiceUrl"];
  return "https://ecommerce.az.orcuslab.com/api/catalog/api/v1/catalog";
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