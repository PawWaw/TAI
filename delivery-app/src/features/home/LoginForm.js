import React, { useRef, useState } from "react";
import { toast } from "react-toastify";
import { useRecoilCallback, useRecoilValue, useSetRecoilState } from "recoil";
import { history } from "../..";
import agent from "../../app/api/agent";
import Button from "../../app/common/Button";
import { Field, Form, Input, Label } from "../../app/common/Form";
import { loadingState, loginCallback, loginSelector, userState } from "../../app/recoil/UserState";

const LoginForm = (e) => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
//  const login = useSetRecoilState(loginSelector);
  const setUser = useSetRecoilState(userState)


  const handleLogin = () => {
    const values = { username, password };
    loginCallback(values)
  //  login(values);
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
      <Button
        type="submit"
        fluid
        style={{ marginTop: "20px" }}
        onClick={handleLogin}
        loading={loading ? 1 : 0}
      >
        Login
      </Button>
    </Form>
  );
};

export default LoginForm;
