import React from "react";
import Separator from "../../app/common/Separator";
import VerticalLogo from "../../app/common/VerticalLogo";
import styled from "styled-components";
import Button from "../../app/common/Button";
import { useRecoilState } from "recoil";
import { modalState } from "../../app/recoil/ModalState";
import Register from "../register/Register";
import LoginForm from "./LoginForm";

const Container = styled.div`
  display: flex;
  flex: 1;
  justify-content: center;
  align-items: center;
`;

const Wrapper = styled.div`
  display: flex;
  flex-direction: column;
`;

const HomeLogin = () => {
  const [, setModal] = useRecoilState(modalState);

  const openRegisterForm = () => {
    setModal({
      opened: true,
      body: <Register />,
    });
  };

  return (
    <Container>
      <Wrapper>
        <VerticalLogo />
        <LoginForm/>
        <Separator margin="10px 0px" />
        <Button fluid secondary onClick={openRegisterForm}>
          Register
        </Button>
      </Wrapper>
    </Container>
  );
};

export default HomeLogin;
