import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";
import { toast } from "react-toastify";
import { history } from "../..";
import agent from "../api/agent";

export const getCurrentUser = createAsyncThunk(
  "userSlice/getCurrentUser",
  async () => {
    const user = await agent.User.current();
    return user;
  }
);

export const login = createAsyncThunk("userSlice/login", async (formValues) => {
  try {
    const user = await agent.User.login(formValues);
    return user;
  } catch (error) {
    toast.error("Invalid username or password.");
    throw error;
  }
});

export const register = createAsyncThunk(
  "userSlice/register",
  async (formValues) => {
    try {
      await agent.User.register(formValues);
      toast.info("Account created.");
    } catch (error) {
      toast.error("Could not create account.");
      throw error;
    }
  }
);

export const updateSettings = createAsyncThunk(
  "userSlice/updateSettings",
  async (formValues) => {
    try {
      await agent.User.updateSettings(formValues);
      toast.info("Account updated.");
      return formValues;
    } catch (error) {
      toast.error("Could not update account.");
      throw error;
    }
  }
);

export const updatePassword = createAsyncThunk(
  "userSlice/updatePassword",
  async (formValues) => {
    try {
      await agent.User.updatePassword(formValues);
      toast.info("Password updated.");
      return formValues;
    } catch (error) {
      toast.error("Could not update password.");
      throw error;
    }
  }
);

export const userSlice = createSlice({
  name: "userSlice",
  initialState: {
    user: null,
    loading: false,
  },
  reducers: {
    logout: (state) => {
      window.localStorage.removeItem("jwt");
      state.user = null;
      history.push("/")
    },
  },
  extraReducers: {
    [getCurrentUser.pending]: (state, action) => {
      state.loading = true;
    },
    [getCurrentUser.fulfilled]: (state, action) => {
      state.user = action.payload;
      state.loading = false;
    },
    [getCurrentUser.rejected]: (state, action) => {
      state.loading = false;
    },
    [login.pending]: (state, action) => {
      state.loading = true;
    },
    [login.fulfilled]: (state, action) => {
      state.user = action.payload;
      window.localStorage.setItem("jwt", action.payload?.token);
      state.loading = false;
      history.push("/dashboard");
    },
    [login.rejected]: (state, action) => {
      state.loading = false;
    },
    [register.pending]: (state, action) => {
      state.loading = true;
    },
    [register.fulfilled]: (state, action) => {
      state.loading = false;
    },
    [register.rejected]: (state, action) => {
      state.loading = false;
    },
    [updateSettings.pending]: (state, action) => {
      state.loading = true;
    },
    [updateSettings.fulfilled]: (state, action) => {
      state.user = { ...state.user, ...action.payload };
      state.loading = false;
    },
    [updateSettings.rejected]: (state, action) => {
      state.loading = false;
    },
    [updatePassword.pending]: (state, action) => {
      state.loading = true;
    },
    [updatePassword.fulfilled]: (state, action) => {
      state.loading = false;
    },
    [updatePassword.rejected]: (state, action) => {
      state.loading = false;
    },
  },
});

export const selectUserState = (state) => state.user;
export const { logout } = userSlice.actions;
export default userSlice.reducer;
