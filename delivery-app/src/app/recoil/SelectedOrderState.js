import { atom, selector } from "recoil";
import { history } from "../..";

export const selectedOrderState = atom({
  key: "selectedOrderState",
  default: null,
});

export const selectOrderSelector = selector({
  key: "selectOrderSelector",
  set: async ({ set }, id) => {
    try {
      console.log("wybieram order");
      set(selectedOrderState, {
        id: id,
        status: "In progress",
        startDate: "2020-05-11T13:20",
        endDate: "2020-05-11T14:20",
        restaurant: "ul. Kwiatowa 15/7 Katowice",
        client: "ul. Sezamkowa 10/7 Katowice",
      });

    } catch (error) {}
  },
});

export const delieveredOrderSelector = selector({
  key: "delieveredOrderSelector",
  set: async ({set}, id) => {
    try {
      console.log(`Dostarczam order: ${id}`)
      history.push("/dashboard")

    } catch(error) {

    }
  }
})