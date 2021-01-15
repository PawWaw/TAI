import { atom, selector } from "recoil";
import { history } from "../..";

export const userState = atom({
  key: "userState",
  default: null,
});

export const loginSelector = selector({
  key: "login",
  set: ({ set }, formValues) => {
    set(userState, formValues);
    history.push("/dashboard");
  },
});

export const registerSelector = selector({
  key: "register",
  set: ({ set }, formValues) => {
    console.log(formValues)
  },
});
