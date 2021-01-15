import { atom, selector, selectorFamily } from "recoil";

export const ordersHistoryState = atom({
  key: "ordersHistoryState",
  default: [],
});

export const fetchOrdersHistorySelector = selector({
  key: "fetchOrdersHistorySelector",
  set: async ({ set }, param) => {
    try {
      console.log(param);
      set(ordersHistoryState, [
        {
          id: 1,
          endDate: "2020-05-11T14:20",
          restaurant: "ul. Kwiatowa 15/7 Katowice",
          client: "ul. Sezamkowa 10/7 Katowice",
        },
        {
          id: 2,
          endDate: "2020-05-11T14:28",
          restaurant: "ul. Kwiatowa 15/7 Katowice",
          client: "ul. Sezamkowa 10/7 Katowicea",
        },
      ]);
    } catch (error) {}
  },
});
