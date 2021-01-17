import { toast } from "react-toastify";
import { atom, selector } from "recoil";
import { history } from "../..";

export const userState = atom({
  key: "userState",
  default: null,
});

export const loginSelector = selector({
  key: "login",
  set: async ({ set }, formValues) => {
    try {
      set(userState, {
        email: "anthony@gmai.com",
        radius: 10,
        city: "Katowice",
        address: "Polna 20",
      });
      history.push("/dashboard");
    } catch (error) {
      toast.error("Invalid username or password.");
    }
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
