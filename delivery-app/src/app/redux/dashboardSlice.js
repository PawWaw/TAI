import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { toast } from "react-toastify";
import agent from "../api/agent";

export const fetchInfo = createAsyncThunk("userSlice/info", async () => {
  try {
    const info = await agent.Dashboard.statisticts();
    return info;
  } catch (error) {
    toast.error("Could not fetch statistics.");
    throw error;
  }
});

export const findOrder = createAsyncThunk(
  "ordersManager/findOrder",
  async () => {
    try {
      const order = await agent.Orders.find()
      // const order = {
      //   id: 32132,
      //   restaurant: "Mielęckiego 7, 40-013 Katowice",
      //   client: "Bankowa 12, 40-007 Katowice",
      // };
      return order;
    } catch (error) {
      console.log(error);
      toast.info("There are not available orders.");
      throw error;
    }
  }
);

export const takeOrder = createAsyncThunk(
  "ordersManager/takeOrder",
  async (id) => {
    try {
      await agent.Orders.take(id);
      return "taken"

    } catch (error) {
      console.log(error);
      toast.error("Could not take order.");
      throw error;
    }
  }
);

export const dashboardSlice = createSlice({
  name: "dashboardSlice",
  initialState: {
    info: null,
    loading: false,
    currentOrder: null,
  },
  reducers: {
    declineOrder: (state) => {
        state.currentOrder = null;
      },
  },
  extraReducers: {
    [fetchInfo.pending]: (state, action) => {
      state.loading = true;
    },
    [fetchInfo.fulfilled]: (state, action) => {
      state.info = action.payload;
      state.loading = false;
    },
    [fetchInfo.rejected]: (state, action) => {
      state.loading = false;
    },
    [findOrder.pending]: (state, action) => {
      state.loading = true;
    },
    [findOrder.fulfilled]: (state, action) => {
      state.currentOrder = action.payload;
      state.loading = false;
    },
    [findOrder.rejected]: (state, action) => {
      state.loading = false;
    },
    [takeOrder.pending]: (state, action) => {
      state.loading = true;
    },
    [takeOrder.fulfilled]: (state, action) => {
      state.loading = false;
    },
    [takeOrder.rejected]: (state, action) => {
      state.loading = false;
    },
  },
});

export const selectDashboardState = (state) => state.dashboard;

export const { declineOrder } = dashboardSlice.actions;

export const selectOrdersCount = (state) => {
  return state.dashboard?.info["Current Orders"];
};
export const selectStatistics = (state) => {
  return Object.keys(state.dashboard.info)
    .map((k) =>
      Object.create({
        name: k,
        value: state.dashboard.info[k],
      })
    )
    .slice(1);
};

export default dashboardSlice.reducer;
