import React from "react";
import styled from "styled-components";
import { ModalHeader } from "../../app/common/ModalHeader";
import { SvgIcon } from "../../app/common/SvgIcon";



const Field = styled.div`
  display: flex;
  flex-direction: column;
  margin: ${(props) => props.margin};
`;

const LabelHeader = styled.h2`
  font-size: 1em;
  color: black;
`;

const LabelData = styled.h3`
  font-size: 0.8em;
  color: #9a9a9a;
  margin-top: -0.7em;
`;

const ButtonsWrapper = styled.div`
  display: flex;
  flex-direction: row;
  justify-content: space-around;
  margin-top: 15px;
`;

const OrderPopup = () => {
  return (
    <>
      <ModalHeader>Found order!</ModalHeader>
      <Field>
        <LabelHeader>Restaurant:</LabelHeader>
        <LabelData>ul. Kwiatowa 15/7 Katowice</LabelData>
      </Field>
      <Field>
        <LabelHeader>Client:</LabelHeader>
        <LabelData>ul. Sezamkowa 5/10 Katowice</LabelData>
      </Field>
      <Field>
        <LabelHeader>Distance:</LabelHeader>
        <LabelData>15km</LabelData>
      </Field>
      <Field>
        <LabelHeader>Map:</LabelHeader>
        <SvgIcon src="assets/svg/scooter.svg" width="100%" />
      </Field>
      <ButtonsWrapper>
          <SvgIcon src="assets/svg/tick.svg" height="30px"/>
          <SvgIcon src="assets/svg/reject.svg" height="30px"/>
      </ButtonsWrapper>
    </>
  );
};

export default OrderPopup;
