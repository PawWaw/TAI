import React, { useState } from "react";
import styled from "styled-components";
import HorizontalLogo from "../../app/common/HorizontalLogo";
import CashBalance from "./CashBalance";
import { Link } from "react-router-dom";
import Button from "../../app/common/Button";
import { history } from "../..";
import { SvgIcon } from "../../app/common/SvgIcon";


const Bar = styled.nav`
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
  height: 6vh;
  
`;

const Menu = styled.div`
  position: absolute;
  display: flex;
  width: 100%;
  height: 96vh;
  z-index: 10;
  margin-top: 6vh;
  padding-top: 4vh;
  flex: 1;
  flex-direction: column;
  background-color: #222831;
`;

const MenuItem = styled(Link)`
  color: white;
  font-size: 1.3em;
  margin-bottom: 1em;
  text-decoration: none;
  align-self: center;
  &:hover {
    color: #6b99c7;
  }
`;

const Navbar = () => {
  const [open, setOpen] = useState(false);
  const toggleMenu = () => {
    setOpen(!open);
  };
  const redirectHome = () => {
    setOpen(!open);
    history.push("/home");
  };
  return (
    <>
      <Bar>
        {open ? <HorizontalLogo onClick={redirectHome} /> : <CashBalance />}
        <SvgIcon
          onClick={toggleMenu}
          src={`assets/svg/${open ? "whiteClose" : "menu"}.svg`}
          alt="menu"
          height="20px"
        />
      </Bar>
      {open && (
        <Menu>
          <MenuItem to="/dashboard">Home</MenuItem>
          <MenuItem to="/orders">Orders</MenuItem>
          <MenuItem to="/settings">Settings</MenuItem>
          <Button secondary>Logout</Button>
        </Menu>
      )}
    </>
  );
};

export default Navbar;
