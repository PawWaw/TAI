import React from "react";
import { useSelector } from "react-redux";
import styled from "styled-components";
import { SvgIcon } from "../../app/common/SvgIcon";
import { selectDragonCoinBalance } from "../../app/redux/userSlice";

const HorizontalWrapper = styled.div`
  display: flex;
  flex-direction: row;
  align-items: center;
  cursor: pointer;
`;

const CashText = styled.h1`
  font-size: 1em;
  color: white;
  margin-top: -5px;
  margin-left: 5px;
`;

const CashBalance = () => {
  
  const dragonCoinBalance  = useSelector(selectDragonCoinBalance);
  
  return (
    <HorizontalWrapper>
      <SvgIcon src="assets/svg/coin.svg" alt="cash" height="20px" />
      <CashText>{dragonCoinBalance}</CashText>
    </HorizontalWrapper>
  );
};

export default CashBalance;
