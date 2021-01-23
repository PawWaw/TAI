import { toast } from "react-toastify";
import { atom, selector, useRecoilCallback } from "recoil";
import { history } from "../..";
import agent from "../api/agent";

export const userState = atom({
  key: "userState",
  default: null,
});

export const tokenSelector = selector({
  key: "tokenSelector",
  get: () => {
    return window.localStorage.getItem("jwt");
  },
});

export const currentUserSelector = selector({
  key: "currentUserSelector",
  set: async ({ set }) => {
    try {
      const user = await agent.User.current();
      console.log(user);
      set(userState, user);
    } catch (error) {
      toast.error("Could not get user from token.");
    }
  },
});

// export const loginSelector = selector({
//   key: "login",
//   set: async ({ set }, formValues) => {
//     try {
//       set(loadingState, true)
//       const user = await agent.User.login(formValues)
//       set(userState, user);
//       set(loadingState, false)
//       history.push("/dashboard");
//     } catch (error) {

//     }
//     set(loadingState, false)
//   },
// });
export const loginSelector = selector({
  key: "loginSelector",
  get: (formValues) => async () => {
    const user = await agent.User.login(formValues);
    return user;
  },
});

export const registerSelector = selector({
  key: "register",
  set: async ({ set }, formValues) => {
    try {
      console.log(formValues);
      toast.info("Account created.");
    } catch (error) {
      toast.error("Could not create account.");
    }
  },
});

export const updateSettingsSelector = selector({
  key: "updateSettingsSelector",
  set: async ({ set }, formValues) => {
    try {
      console.log("update settings!");
      console.log(formValues);
      set(userState, formValues);
      toast.info("Settings updated.");
    } catch (error) {
      toast.error("Could not update settings.");
    }
  },
});

export const updatePasswordSelector = selector({
  key: "updatePasswordSelector",
  set: async ({ set }, formValues) => {
    try {
      console.log("update password");
      console.log(formValues);
      toast.info("Password changed.");
    } catch (error) {
      toast.error("Could not update password.");
    }
  },
});
