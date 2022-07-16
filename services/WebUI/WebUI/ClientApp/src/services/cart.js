import axios from 'axios';

const cartUrl = process.env.REACT_APP_CART_SERVICE_URL;

const CartService = {
  get: async (username) => {
    const resp = await axios.get(`${cartUrl}/${username}`);
    return resp.data;
  },
  post: async (data) => {
    const resp = await axios.post(cartUrl, data);
    return resp.data;
  },
  checkout: async (data) => {
    await axios.post(`${cartUrl}/CheckoutCart`, data);
  }
};

export default CartService;
