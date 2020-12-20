import React, { useState } from "react";
import { useRecoilState } from "recoil";
import styled from "styled-components";
import Button from "../../app/common/Button";
import { Field, Form, Input, Label } from "../../app/common/Form";
import { PageHeader } from "../../app/common/PageHeader";
import Separator from "../../app/common/Separator";
import { SvgIcon } from "../../app/common/SvgIcon";
import { modalState } from "../../app/recoil/ModalState";
import PasswordPopup from "./PasswordPopup";

const Wrapper = styled.div`
  display: flex;
  flex-direction: column;
`;

const RadiusWrapper = styled.div`
  display: flex;
  flex-direction: row;
  align-items: center;
  margin: -10px 0;
`;

const RadiusText = styled.h1`
  font-size: 2em;
  font-weight: 800;
  color: white;
  margin: 0 10px;
  margin-top: -0.25em;
`;

const Settings = () => {
  const [radius, setRadius] = useState(255);
  const [modal, setModal] = useRecoilState(modalState);
  return (
    <Wrapper>
      <PageHeader>Settings</PageHeader>
      <Form>
        <Field>
          <Label>Radius</Label>
          <RadiusWrapper>
            <SvgIcon
              alt="left"
              src="assets/svg/left.svg"
              onClick={() => setRadius(radius - 1)}
            />
            <RadiusText>{radius} km</RadiusText>
            <SvgIcon
              alt="right"
              src="assets/svg/right.svg"
              onClick={() => setRadius(radius + 1)}
            />
          </RadiusWrapper>
        </Field>
        <Field>
          <Label>Email</Label>
          <Input />
        </Field>
        <Field>
          <Label>City</Label>
          <Input />
        </Field>
        <Field>
          <Label>Address</Label>
          <Input />
        </Field>
        <Button margin="0.7em 0 0 0">Update</Button>
      </Form>
      <Separator margin="0.6em 0" />
      <Button
        secondary
        onClick={() => setModal({ opened: true, body: <PasswordPopup /> })}
      >
        Change Password
      </Button>
    </Wrapper>
  );
};

export default Settings;
