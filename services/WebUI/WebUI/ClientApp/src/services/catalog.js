import axios from 'axios';

const catalogUrl = process.env.REACT_APP_CATALOG_SERVICE_URL;

const CatalogService = {
  getAll: async () => {
    const resp = await axios.get(catalogUrl);
    return resp.data;
  },
};

export default CatalogService;