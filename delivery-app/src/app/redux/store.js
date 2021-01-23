import { configureStore } from "@reduxjs/toolkit";
import userReducer from "./userSlice";
import dashboardReducer from "./dashboardSlice";
import ordersManagerReducer from "./ordersManagerSlice";

export default configureStore({
  reducer: {
    user: userReducer,
    dashboard: dashboardReducer,
    ordersManager: ordersManagerReducer
  },
});
