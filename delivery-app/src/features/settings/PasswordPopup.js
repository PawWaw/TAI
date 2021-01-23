import React, { useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import Button from "../../app/common/Button";
import { Field, Form, Input, Label } from "../../app/common/Form";
import { ModalHeader } from "../../app/common/ModalHeader";
import { selectUserState, updatePassword } from "../../app/redux/userSlice";

const PasswordPopup = () => {
  const dispatch = useDispatch()
  const {loading} = useSelector(selectUserState)
  const [oldPassword, setOldPassword] = useState("");
  const [newPassword, setNewPassword] = useState("");
  const [confirmNewPassword, setConfirmNewPassword] = useState("");

  const handlePasswordChange = () => {
    const formValues = {
      oldPassword: {
        password: oldPassword,
      },
      newPassword: {
        password: newPassword,
      }
    };
    dispatch(updatePassword(formValues));
  };

  return (
    <>
      <ModalHeader>Password change</ModalHeader>
      <Form>
        <Field>
          <Label dark>Current password</Label>
          <Input
            type="password"
            value={oldPassword}
            onChange={(e) => setOldPassword(e.currentTarget.value)}
          />
        </Field>
        <Field>
          <Label dark>New password</Label>
          <Input
            type="password"
            value={newPassword}
            onChange={(e) => setNewPassword(e.currentTarget.value)}
          />
        </Field>
        <Field>
          <Label dark>Confirm new password</Label>
          <Input
            type="password"
            value={confirmNewPassword}
            onChange={(e) => setConfirmNewPassword(e.currentTarget.value)}
          />
        </Field>
        <Button
          margin="20px 0 0 0"
          loading = {loading ? 1 : 0}
          disabled={newPassword !== confirmNewPassword}
          onClick={handlePasswordChange}
        >
          Change
        </Button>
      </Form>
    </>
  );
};

export default PasswordPopup;
