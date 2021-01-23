import React from "react";
import { useSelector } from "react-redux";
import { useRecoilValue } from "recoil";
import styled from "styled-components";
import { infoSelector } from "../../app/recoil/DashboardState";
import { selectStatistics } from "../../app/redux/dashboardSlice";
import StatisticItem from "./StatisticItem";

const Wrapper = styled.div`
  display: flex;
  flex-direction: column;
`;

const Statistics = () => {
 
  //const stats = []
  const stats = useSelector(selectStatistics);

  return (
    <Wrapper>
      {stats.map((s) => (
        <StatisticItem key={s.name} statistic={s} />
      ))}
    </Wrapper>
  );
};

export default Statistics;
