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
      caption: "Max daily orders",
      value: "12",
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
