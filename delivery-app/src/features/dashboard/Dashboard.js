import React, { useEffect, useState } from "react";
import { useDispatch } from "react-redux";
import { useRecoilValue } from "recoil";
import styled from "styled-components";
import { modalState } from "../../app/recoil/ModalState";
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
  const [loaded, setLoaded] = useState(false)
  const modal = useRecoilValue(modalState)
  const dispatch = useDispatch()
  useEffect(() => {
    dispatch(fetchInfo()).then(() => setLoaded(true))
  }, [modal]);
  if(!loaded) {
    return <h1>Loading...</h1>
  }
  return (
    <Wrapper>
      <OrderInfo />
      <Statistics />
    </Wrapper>
  );
};

export default Dashboard;
