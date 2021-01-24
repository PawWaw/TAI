import React from "react";
import { useDispatch, useSelector } from "react-redux";
import { useRecoilState } from "recoil";
import styled from "styled-components";
import { history } from "../..";
import Button from "../../app/common/Button";
import { PageHeader } from "../../app/common/PageHeader";
import { modalState } from "../../app/recoil/ModalState";
import {
  findOrder,
  selectDashboardState,
  selectOrdersCount,
} from "../../app/redux/dashboardSlice";
import OrderPopup from "../order/OrderPopup";

const Wrapper = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: space-evenly;
`;

const Text = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
`;

const Order = styled.h1`
  margin-top: -0.6em;
  cursor: pointer;
  font-size: 1.5em;
  color: ${(props) => (props.id ? "#F05454" : "#6B99C7")};
`;

const OrderInfo = () => {
  const [, setModal] = useRecoilState(modalState);
  const ordersCount = useSelector(selectOrdersCount);
  const dispatch = useDispatch();
  const { loading } = useSelector(selectDashboardState);

  const handleOrderClick = () => {
    if (ordersCount > 0) {
      history.push(`/orders`);
    }
  };

  const handleFindOrder = () => {
    dispatch(findOrder()).then((r) => {
      if (r?.payload !== undefined) {
        setModal({
          opened: true,
          body: <OrderPopup />,
        });
      }
    });
  };

  return (
    <Wrapper>
      <Text>
        <PageHeader>Current orders:</PageHeader>
        <Order onClick={handleOrderClick}>
          {ordersCount > 0 ? `${ordersCount}` : "None"}
        </Order>
      </Text>
      <Button
        secondary
        onClick={handleFindOrder}
        loading={loading ? 1 : 0}
        fluid
      >
        Find order
      </Button>
    </Wrapper>
  );
};

export default OrderInfo;
