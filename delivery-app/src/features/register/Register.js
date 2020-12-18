import React from "react";
import Button from "../../app/common/Button";
import { Form, IconInput } from "../../app/common/Form";
import styled from "styled-components";

const Header = styled.h1`
  font-size: 1.3em;
  color: black;
  margin: 20px 0px;
`;

const Register = () => {
  return (
    <>
      <Header>Fill up fields to register</Header>
      <Form>
        <IconInput icon="../assets/svg/user.svg" placeholder="Username" />
        <IconInput icon="../assets/svg/lock.svg" placeholder="Password" />
        <IconInput icon="../assets/svg/envelope.svg" placeholder="Email" />
        <IconInput
          icon="../assets/svg/credential.svg"
          placeholder="First Name"
        />
        <IconInput icon="../assets/svg/namefill.svg" placeholder="Last Name" />
        <IconInput icon="../assets/svg/location.svg" placeholder="City" />
        <IconInput icon="../assets/svg/pin.svg" placeholder="Address" />
        <Button>Sign Up</Button>
      </Form>
    </>
  );
};

export default Register;
