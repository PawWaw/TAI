import React, { useState } from "react";
import Button from "../../app/common/Button";
import { Form, IconInput } from "../../app/common/Form";
import styled from "styled-components";
import { useDispatch, useSelector } from "react-redux";
import { register, selectUserState } from "../../app/redux/userSlice";

const Header = styled.h1`
  font-size: 1.3em;
  color: black;
  margin: 20px 0px;
  align-self: center;
`;

const Register = () => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [email, setEmail] = useState("");
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [city, setCity] = useState("");
  const [address, setAddress] = useState("");

  const dispatch = useDispatch();
  const { loading } = useSelector(selectUserState);

  const handleRegister = () => {
    const values = {
      username,
      password,
      email,
      firstName,
      lastName,
      city,
      address,
    };
    dispatch(register(values));
  };

  return (
    <>
      <Header>Provide valid data</Header>
      <Form>
        <IconInput
          icon="../assets/svg/user.svg"
          placeholder="Username"
          value={username}
          onChange={(e) => setUsername(e.target.value)}
        />
        <IconInput
          icon="../assets/svg/lock.svg"
          placeholder="Password"
          type="password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
        <IconInput
          icon="../assets/svg/envelope.svg"
          placeholder="Email"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />
        <IconInput
          icon="../assets/svg/credential.svg"
          placeholder="First Name"
          value={firstName}
          onChange={(e) => setFirstName(e.target.value)}
        />
        <IconInput
          icon="../assets/svg/namefill.svg"
          placeholder="Last Name"
          value={lastName}
          onChange={(e) => setLastName(e.target.value)}
        />
        <IconInput
          icon="../assets/svg/location.svg"
          placeholder="City"
          value={city}
          onChange={(e) => setCity(e.target.value)}
        />
        <IconInput
          icon="../assets/svg/pin.svg"
          placeholder="Address"
          value={address}
          onChange={(e) => setAddress(e.target.value)}
        />
        <Button onClick={handleRegister} loading={loading ? 1 : 0}>
          Sign Up
        </Button>
      </Form>
    </>
  );
};

export default Register;
