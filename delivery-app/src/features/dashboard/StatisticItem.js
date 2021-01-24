import React from "react";
import styled from "styled-components";

const Wrapper = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
`;

const Value = styled.h1`
  color: white;
  font-size: 2em;
  font-weight: bold;
`;

const Caption = styled.h2`
  margin-top: -0.7em;
  color: #e5e5e5;
  font-size: 1em;
`;

const StatisticItem = ({ statistic }) => {
  return (
    <Wrapper>
      <Value>{statistic.value.toFixed(2)}</Value>
      <Caption>{statistic.name}</Caption>
    </Wrapper>
  );
};

export default StatisticItem;
