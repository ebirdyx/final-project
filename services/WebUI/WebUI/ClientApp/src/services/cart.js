import axios from 'axios';

const getCartUrl = async () => {
  // const response = await axios.get(`${window.location.origin}/config`)
  // return response.data.json()["cartServiceUrl"];
  return "https://ecommerce.az.orcuslab.com/api/cart/api/v1/cart";
}

const CartService = {
  get: async (username) => {
    const cartUrl = await getCartUrl();
    const resp = await axios.get(`${cartUrl}/${username}`);
    return resp.data;
  },
  post: async (data) => {
    const cartUrl = await getCartUrl();
    const resp = await axios.post(cartUrl, data);
    return resp.data;
  },
  checkout: async (data) => {
    const cartUrl = await getCartUrl();
    await axios.post(`${cartUrl}/CheckoutCart`, data);
  }
};

export default CartService;
