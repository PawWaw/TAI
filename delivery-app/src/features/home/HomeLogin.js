import React from "react";
import { Button } from "../../app/common/Button";
import { Field, Form, Label, Input } from "../../app/common/Form";
import Separator from "../../app/common/Separator";
import VerticalLogo from "../../app/common/VerticalLogo";
import styled from "styled-components";

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
  return (
    <Container>
      <Wrapper>
        <VerticalLogo />
        <Form>
          <Field>
            <Label>Username</Label>
            <Input />
          </Field>
          <Field>
            <Label>Password</Label>
            <Input />
          </Field>
          <Button fluid style={{ marginTop: "20px" }}>
            Login
          </Button>
        </Form>
        <Separator margin="10px 0px" />
        <Button fluid secondary>
          Register
        </Button>
      </Wrapper>
    </Container>
  );
};

export default HomeLogin;
