import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import styled from "styled-components";
import { PageHeader } from "../../app/common/PageHeader";
import { fetchOrders, selectOrdersManagerState } from "../../app/redux/ordersManagerSlice";
import OrderShortDetails from "./OrderShortDetails";

const Wrapper = styled.div`
  display: flex;
  flex-direction: column;
  flex: 1;
  overflow: hidden;
  /* @media screen and (max-width: 300px) {
    font-size: 15px;
  }; */
`;

const MenuWrapper = styled.div`
  display: flex;
  flex-direction: row;
`;

const MenuItem = styled.h2`
  font-size: 1.1em;
  cursor: pointer;
  border-bottom: ${(props) => (props.active ? "3px solid #F05454" : "none")};
  margin-right: 1em;
`;

const OrdersWrapper = styled.div`
  display: flex;
  flex-direction: column;
  flex: 1;
  margin-top: 10px;
  overflow-x: auto;
  ::-webkit-scrollbar {
    width: 5px;
  }

  ::-webkit-scrollbar-track {
    background: white;
  }

  ::-webkit-scrollbar-thumb {
    background: #f05454;
  }

  ::-webkit-scrollbar-thumb:hover {
    background: #f05454;
  }
`;

const OrderList = () => {
  const [activeTab, setActiveTab] = useState("current");
  const {orders} = useSelector(selectOrdersManagerState)
  const dispatch = useDispatch()

  useEffect(() => {
    dispatch(fetchOrders(activeTab === "current"))
  }, [activeTab])

  return (
    <Wrapper>
      <PageHeader>Orders</PageHeader>
      <MenuWrapper>
        <MenuItem
          active={activeTab === "current"}
          onClick={() => setActiveTab("current")}
        >
          Current
        </MenuItem>
        <MenuItem
          active={activeTab === "past"}
          onClick={() => setActiveTab("past")}
        >
          Past
        </MenuItem>
      </MenuWrapper>
      <OrdersWrapper>
        {orders?.map((order, i) => (
          <OrderShortDetails
            key={i}
            order={order}
          />
        ))}
      </OrdersWrapper>
    </Wrapper>
  );
};

export default OrderList;
