import React from "react";
import Button from "../../app/common/Button";
import { Field, Form, Input, Label } from "../../app/common/Form";
import { ModalHeader } from "../../app/common/ModalHeader";

const PasswordPopup = () => {
  return (
    <>
      <ModalHeader>Password change</ModalHeader>
      <Form>
        <Field>
          <Label dark>Current password</Label>
          <Input type="password" />
        </Field>
        <Field>
          <Label dark>New password</Label>
          <Input type="password"/>
        </Field>
        <Field>
          <Label dark>Confirm new password</Label>
          <Input type="password"/>
        </Field>
        <Button margin="20px 0 0 0">Change</Button>
      </Form>
    </>
  );
};

export default PasswordPopup;
