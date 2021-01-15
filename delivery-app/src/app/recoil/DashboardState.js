import { atom, selector } from "recoil";

export const infoState = atom({
  key: "infoState",
  default: [],
});

export const ordersCountSelector = selector({
  key: "ordersCount",
  get: ({ get }) => {
    const info = get(infoState);
    const ordersCount = info.filter((i) => i.name === "Current orders")[0]
      ?.value;
    return !ordersCount ? 0 : ordersCount;
  },
});

export const infoSelector = selector({
  key: "info",
  get: ({ get }) => {
    const info = get(infoState);
    return info.filter((i) => i.name !== "Current orders");
  },
});

export const fetchInfoSelector = selector({
  key: "fetchInfoSelector",
  set: async ({ set }) => {
    try {
      set(infoState, [
        {
          name: "Current orders",
          value: 5,
        },
        {
          name: "Client rate",
          value: "3.39",
        },
        {
          name: "Total delivery",
          value: "407",
        },
        {
          name: "Max daily orders",
          value: "32",
        },
      ]);
    } catch(error) {
      
    }
  },
});
