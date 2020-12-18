import React, { useState } from "react";
import styled from "styled-components";
import HorizontalLogo from "../../app/common/HorizontalLogo";
import CashBalance from "./CashBalance";
import { Link } from "react-router-dom";
import Button from "../../app/common/Button";
import { history } from "../..";
import { SvgIcon } from "../../app/common/SvgIcon";

const Wrapper = styled.div`
  display: flex;
  flex-direction: column;
`;

const Bar = styled.nav`
  display: flex;
  flex-direction: row;
  justify-content: space-between;
  align-items: center;
`;

const Menu = styled.div`
  display: flex;
  flex-direction: column;
  margin-top: 2em;
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
    <Wrapper>
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
          <MenuItem>Home</MenuItem>
          <MenuItem>History</MenuItem>
          <MenuItem>Settings</MenuItem>
          <Button secondary>Logout</Button>
        </Menu>
      )}
    </Wrapper>
  );
};

export default Navbar;
