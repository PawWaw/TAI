import React, { useRef, useState } from "react";
import { useSetRecoilState } from "recoil";
import { history } from "../..";
import Button from "../../app/common/Button";
import { Field, Form, Input, Label } from "../../app/common/Form";
import { loginSelector } from "../../app/recoil/UserState";

const LoginForm = (e) => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const login = useSetRecoilState(loginSelector);

  const handleLogin = () => {
    // console.log({
    //   username,
    //   password
    // })
    const values = { username, password };
    login(values);
  };

  return (
    <Form>
      <Field>
        <Label>Username</Label>
        <Input
          name="username"
          value={username}
          onChange={(e) => setUsername(e.target.value)}
        />
      </Field>
      <Field>
        <Label>Password</Label>
        <Input
          type="password"
          name="password"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
      </Field>
      <Button type="submit" fluid style={{ marginTop: "20px" }} onClick={handleLogin}>
        Login
      </Button>
    </Form>
  );
};

export default LoginForm;
