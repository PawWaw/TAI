import React, { useEffect } from "react";
import { useDispatch } from "react-redux";
import styled from "styled-components";
import { fetchInfo } from "../../app/redux/dashboardSlice";
import OrderInfo from "./OrderInfo";
import Statistics from "./Statistics";

const Wrapper = styled.div`
  display: flex;
  flex: 1;
  flex-direction: column;
  align-items: center;
  justify-content: space-evenly;
`;

const Dashboard = () => {
  const dispatch = useDispatch()
  useEffect(() => {
    dispatch(fetchInfo())
  }, []);
  return (
    <Wrapper>
      <OrderInfo />
      <Statistics />
    </Wrapper>
  );
};

export default Dashboard;
