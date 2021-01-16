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
    } catch (error) {}
  },
});

export const registerSelector = selector({
  key: "register",
  set: async ({ set }, formValues) => {
    try {
      console.log(formValues);
    } catch (error) {}
  },
});

export const updateSettingsSelector = selector({
  key: "updateSettingsSelector",
  set: async ({ set }, formValues) => {
    console.log("update settings!")
    console.log(formValues)
    set(userState, formValues)
  },
});


export const updatePasswordSelector = selector({
  key: "updatePasswordSelector",
  set: async({set}, formValues) => {
    console.log("update password")
    console.log(formValues)
  }
})