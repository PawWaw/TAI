import React from "react";
import styled from "styled-components";
import { SvgIcon } from "../../app/common/SvgIcon";

const Wrapper = styled.div`
  display: flex;
  cursor: pointer;
  flex-direction: column;
  border-bottom: 1px solid white;
  margin-right: 20px;
  padding: 0 5px 5px 5px;
  :last-of-type {
    border: none;
  }
`;

const FirstLine = styled.div`
  display: flex;
  justify-content: space-between;
`;

const DateText = styled.h2`
  font-size: 1em;
  color: white;
`;

const IdText = styled.h2`
  font-size: 1em;
  color: #6b99c7;
`;

const DataWrapper = styled.div`
  display: flex;
  flex-direction: row;
  align-items: center;
`;

const DataText = styled.h3`
  font-size: 0.7em;
  color: #9a9a9a;
  margin-left: 5px;
`;

const OrderShortDetails = ({ id, startDate, restaurant, client }) => {
  return (
    <Wrapper>
      <FirstLine>
        <DateText>{startDate}</DateText>
        <IdText>#{id}</IdText>
      </FirstLine>
      <DataWrapper>
        <SvgIcon
          alt="restaurant"
          src="assets/svg/restaurant.svg"
          height="16px"
        />
        <DataText>{restaurant}</DataText>
      </DataWrapper>
      <DataWrapper>
        <SvgIcon alt="restaurant" src="assets/svg/home.svg" height="16px" />
        <DataText>{client}</DataText>
      </DataWrapper>
    </Wrapper>
  );
};

export default OrderShortDetails;
