import axios from 'axios';

const catalogUrl = 'http://localhost:5184/api/v1/catalog';

const CatalogService = {
  getAll: async () => {
    const resp = await axios.get(catalogUrl);
    return resp.data;
  },

  // getById: async (id) => {
  //   const resp = await axios.get(`${categoryUrl}/categories/${id}`);
  //   return resp.data;
  // },
  //
  // create: async (category) => {
  //   const resp = await axios.post(`${categoryUrl}/categories`, category);
  //   return resp.data;
  // },
  //
  // delete: async (id) => {
  //   const resp = await axios.delete(`${categoryUrl}/categories/${id}`);
  //   return resp.data;
  // },
  //
  // edit: async (id, category) => {
  //   const resp = await axios.put(`${categoryUrl}/categories/${id}`, category);
  //   return resp.data;
  // },
};

export default CatalogService;