import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { toast } from "react-toastify";
import agent from "../api/agent";

export const fetchOrders = createAsyncThunk(
  "ordersManager/fetchOrders",
  async (current) => {
    try {
      const orders = await agent.Orders.list(current);
      return orders;
    } catch (error) {
      toast.error("Could not fetch orders.");
      throw error;
    }
  }
);

export const fetchOrderById = createAsyncThunk(
  "ordersManager/fetchOrderById",
  async (id) => {
    try {
      const order = await agent.Orders.details(id);
      return order;
    } catch (error) {
      toast.error(`Could not fetch order ${id}.`);
      throw error;
    }
  }
);

export const deliverOrderById = createAsyncThunk(
  "ordersManager/deliverOrderById",
  async (id) => {
    try {
      await agent.Orders.delivered(id);
      return "delivered"
    } catch (error) {
      toast.error(`Could not deliver order ${id}.`);
      throw error;
    }
  }
);

export const ordersManagerSlice = createSlice({
  name: "ordersManager",
  initialState: {
    orders: null,
    detailedOrder : null,
    loading: false,
  },
  extraReducers: {
    [fetchOrders.pending]: (state, action) => {
      state.loading = true;
    },
    [fetchOrders.fulfilled]: (state, action) => {
      state.orders = action.payload;
      state.loading = false;
    },
    [fetchOrders.rejected]: (state, action) => {
      state.loading = false;
    },
    [fetchOrderById.pending]: (state, action) => {
      state.loading = true;
    },
    [fetchOrderById.fulfilled]: (state, action) => {
      state.detailedOrder = action.payload;
      state.loading = false;
    },
    [fetchOrderById.rejected]: (state, action) => {
      state.loading = false;
    },
    [deliverOrderById.pending]: (state, action) => {
      state.loading = true;
    },
    [deliverOrderById.fulfilled]: (state, action) => {
      state.loading = false;
    },
    [deliverOrderById.rejected]: (state, action) => {
      state.loading = false;
    },
  },
});

export const selectOrdersManagerState = (state) => state.ordersManager;

export default ordersManagerSlice.reducer;
