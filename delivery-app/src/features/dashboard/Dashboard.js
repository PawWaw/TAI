import React, { useEffect } from "react";
import { useSetRecoilState } from "recoil";
import styled from "styled-components";
import { fetchInfoSelector } from "../../app/recoil/DashboardState";
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
  const fetchInfo = useSetRecoilState(fetchInfoSelector);
  useEffect(() => {
    fetchInfo();
  }, []);
  return (
    <Wrapper>
      <OrderInfo />
      <Statistics />
    </Wrapper>
  );
};

export default Dashboard;
