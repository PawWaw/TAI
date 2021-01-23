import React, { useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { history } from "../..";
import Button from "../../app/common/Button";
import { Field, Form, Input, Label } from "../../app/common/Form";
import { login, selectUserState } from "../../app/redux/userSlice";

const LoginForm = () => {
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const dispatch = useDispatch();
  const { loading } = useSelector(selectUserState);

  const handleLogin = () => {
    const values = { username, password };
    dispatch(login(values))
      .then((r) => {
        if(r.payload !== undefined) {
          history.push("/dashboard")
        }
      })
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
