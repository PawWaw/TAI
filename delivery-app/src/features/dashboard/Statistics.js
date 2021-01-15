import React from "react";
import { useRecoilValue } from "recoil";
import styled from "styled-components";
import { infoSelector } from "../../app/recoil/DashboardState";
import StatisticItem from "./StatisticItem";

const Wrapper = styled.div`
  display: flex;
  flex-direction: column;
`;

const Statistics = () => {
  const stats = useRecoilValue(infoSelector)
    
  return (
    <Wrapper>
      {stats.map((s) => (
        <StatisticItem key={s.name} statistic={s} />
      ))}
    </Wrapper>
  );
};

export default Statistics;
