import React from "react";
import styled from "styled-components";
import StatisticItem from "./StatisticItem";

const Wrapper = styled.div`
  display: flex;
  flex-direction: column;
`;

const Statistics = () => {
  const stats = [
    {
      caption: "Client rate",
      value: "3.39",
    },
    {
      caption: "Total delivery",
      value: "402",
    },
    {
      caption: "Best time per km",
      value: "11m 22s",
    },
  ];
  return (
    <Wrapper>
      {stats.map((s) => (
        <StatisticItem key={s.caption} statistic={s} />
      ))}
    </Wrapper>
  );
};

export default Statistics;
