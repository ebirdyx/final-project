import {configureStore, createSlice, createAsyncThunk} from '@reduxjs/toolkit';
import CartService from "./services/cart";

export const updateCart = createAsyncThunk(
  'cart/updateCart', async (cart, thunkAPI) => {
    return await CartService.post(cart);
});

const cartSlice = createSlice({
  name: 'cart',
  initialState: {
    userName: '',
    items: [],
    totalPrice: 0,
  },
  reducers: {
    resetCart: (state, action) => {
      state.userName = "";
      state.items = [];
      state.totalPrice = 0;
    },
  },
  extraReducers: builder => {
    builder.addCase(updateCart.fulfilled, (state, action) => {
      state.userName = action.payload.userName;
      state.items = action.payload.items;
      state.totalPrice = action.payload.totalPrice;
    });
  },
});

export const {resetCart} = cartSlice.actions;

const reLoadStore = () => {
  if (localStorage.getItem('orcuslab') !== null) {
    return JSON.parse(localStorage.getItem('orcuslab')); // re-hydrate the store
  }
};

const localStorageMiddleware = ({ getState }) => {
  return next => action => {
    const result = next(action);
    localStorage.setItem('orcuslab', JSON.stringify(getState()));
    return result;
  };
};

const rootStore = configureStore({
  reducer: {
    cart: cartSlice.reducer,
  },
  devTools: process.env.NODE_ENV !== 'production',
  preloadedState: reLoadStore(),
  middleware: getDefaultMiddleware =>
    getDefaultMiddleware().concat(localStorageMiddleware),
});

export default rootStore;