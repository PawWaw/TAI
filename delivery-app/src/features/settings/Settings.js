import React, { useState } from "react";
import { useRecoilState, useRecoilValue, useSetRecoilState } from "recoil";
import styled from "styled-components";
import Button from "../../app/common/Button";
import { Field, Form, Input, Label } from "../../app/common/Form";
import { PageHeader } from "../../app/common/PageHeader";
import Separator from "../../app/common/Separator";
import { SvgIcon } from "../../app/common/SvgIcon";
import { modalState } from "../../app/recoil/ModalState";
import { changeEmailSelector, updateSettingsSelector, userState } from "../../app/recoil/UserState";
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
  const [modal, setModal] = useRecoilState(modalState);
  const user = useRecoilValue(userState);
  const updateSettings = useSetRecoilState(updateSettingsSelector)
  const [radius, setRadius] = useState(user.radius);
  const [email, setEmail] = useState(user.email);
  const [city, setCity] = useState(user.city);
  const [address, setAddress] = useState(user.address);

  const handleUpdate = () => {
    const formValues = {
      radius,
      email,
      city,
      address
    }
    updateSettings(formValues)
  }

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
          <Input
            value={email}
            onChange={(e) => setEmail(e.currentTarget.value)}
          />
        </Field>
        <Field>
          <Label>City</Label>
          <Input
            value={city}
            onChange={(e) => setCity(e.currentTarget.value)}
          />
        </Field>
        <Field>
          <Label>Address</Label>
          <Input
            value={address}
            onChange={(e) => setAddress(e.currentTarget.value)}
          />
        </Field>
        <Button margin="0.7em 0 0 0" onClick={handleUpdate}>Update</Button>
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
