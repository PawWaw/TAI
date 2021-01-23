import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { toast } from "react-toastify";
import agent from "../api/agent";

export const fetchInfo = createAsyncThunk(
  "userSlice/info",
  async (formValues) => {
    try {
      const info = await agent.Dashboard.statisticts();
      return info;
    } catch (error) {
      toast.error("Could not fetch statistics.");
      throw error;
    }
  }
);

export const dashboardSlice = createSlice({
  name: "dashboardSlice",
  initialState: {
    info: null,
    loading: false,
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
  },
});

export const selectOrdersCount = (state) => {
  if (state.dashboard.info != null) {
    return state.dashboard.info["Current Orders"];
  }
  return 0
};
export const selectStatistics = (state) => {
  if (state.dashboard.info != null) {
    return Object.keys(state.dashboard.info)
      .map((k) =>
        Object.create({
          name: k,
          value: state.dashboard.info[k],
        })
      )
      .slice(1);
  }
  return []
};

export default dashboardSlice.reducer;
