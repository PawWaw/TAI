import React, { useState } from "react";
import styled from "styled-components";
import HorizontalLogo from "../../app/common/HorizontalLogo";
import CashBalance from "./CashBalance";
import Button from "../../app/common/Button";
import { history } from "../..";
import { SvgIcon } from "../../app/common/SvgIcon";

const Bar = styled.nav`
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
  height: 7vh;
`;

const Menu = styled.div`
  position: absolute;
  display: flex;
  width: 100%;
  height: 96vh;
  z-index: 10;
  margin-top: 7vh;
  padding-top: 4vh;
  flex: 1;
  flex-direction: column;
  background-color: #222831;
`;

const MenuItem = styled.h1`
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

  const handleRoute = (path) => {
    setOpen(!open);
    history.push(path);
  };
  return (
    <>
      <Bar>
        {open ? (
          <HorizontalLogo onClick={() => handleRoute("/dashboard")} />
        ) : (
          <CashBalance />
        )}
        <SvgIcon
          onClick={toggleMenu}
          src={`assets/svg/${open ? "whiteClose" : "menu"}.svg`}
          alt="menu"
          height="20px"
        />
      </Bar>
      {open && (
        <Menu>
          <MenuItem key="dashboard" onClick={() => handleRoute("/dashboard")}>
            Home
          </MenuItem>
          <MenuItem key="orders" onClick={() => handleRoute("/orders")}>
            Orders
          </MenuItem>
          <MenuItem key="settings" onClick={() => handleRoute("/settings")}>
            Settings
          </MenuItem>
          <Button secondary onClick={() => handleRoute("/")}>
            Logout
          </Button>
        </Menu>
      )}
    </>
  );
};

export default Navbar;
