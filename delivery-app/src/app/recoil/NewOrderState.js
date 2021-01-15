import { atom, selector } from "recoil";
import OrderPopup from "../../features/order/OrderPopup";
import { infoState } from "./DashboardState";
import { modalState } from "./ModalState";

export const newOrderState = atom({
  key: "newOrderState",
  default: null,
});

export const findOrderSelector = selector({
  key: "findOrderSelector",
  set: async ({ set }) => {
      try {
        console.log("szukam orderuuu")

        const id = 32132
        const restaurant = "Mielęckiego 7, 40-013 Katowice"
        const client = "Bankowa 12, 40-007 Katowice"

        set(newOrderState, {
          id,
          restaurant,
          client
        })

        set(modalState, {
          opened: true,
          body: <OrderPopup/>
        })
        
      } catch(error) {

      }
  },
});

export const acceptOrderSelector = selector({
  key: "acceptOrderSelector",
  set: async ({set, get}) => {
    const info = get(infoState)

    const objIndex = info.findIndex(obj => obj.name === 'Current orders');
    const updatedObj = { ...info[objIndex], value: info[objIndex].value + 1};

    const updatedInfo = [
      ...info.slice(0, objIndex),
      updatedObj,
      ...info.slice(objIndex + 1),
    ];

    set(infoState, updatedInfo)

    set(modalState, {
      opened: false,
      body: null
    })
  }
})


export const declineOrderSelector = selector({
  key: "declineOrderSelector",
  set: async ({set}) => {
    set(modalState, {
      opened: false,
      body: null
    })
    set(newOrderState, null)
  }
})
