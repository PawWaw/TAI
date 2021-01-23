import { configureStore } from "@reduxjs/toolkit";
import userReducer from "./userSlice";
import dashboardReducer from "./dashboardSlice";

export default configureStore({
  reducer: {
    user: userReducer,
    dashboard: dashboardReducer,
  },
});
