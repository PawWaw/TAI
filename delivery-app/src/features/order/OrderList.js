import React from "react";
import styled from "styled-components";

const Wrapper = styled.div`
  display: flex;
  flex-direction: column;
`;
const Header = styled.h1`
  color: white;
  font-size: 1.5em;
`;

const MenuWrapper = styled.div`
  display: flex;
  flex-direction: row;
`;

const MenuItem = styled.h2`
    font-size: 1.2em;
    cursor: pointer;
    border-bottom: ${props => props.active ? "3px solid #F05454" : "none" };
    margin-right: 2em;
`

const OrderList = () => {
  return (
    <Wrapper>
      <Header>Orders</Header>
      <MenuWrapper>
          <MenuItem active>Current</MenuItem>
          <MenuItem>Past</MenuItem>

      </MenuWrapper>
    </Wrapper>
  );
};

export default OrderList;
