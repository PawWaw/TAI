const { atom } = require("recoil");

export const modalState = atom({
  key: "modalState",
  default: {
    opened: false,
    body: null,
  },
});

