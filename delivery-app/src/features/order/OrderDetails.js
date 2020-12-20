import React from "react";
import styled from "styled-components";
import Button from "../../app/common/Button";
import { PageHeader } from "../../app/common/PageHeader";
import { SvgIcon } from "../../app/common/SvgIcon";

const Wrapper = styled.div`
  flex: 1;
  display: flex;
  flex-direction: column;
`;

const Order = styled.h2`
  color: #f05454;
  font-size: 1.1em;
`;

const Field = styled.div`
  display: flex;
  flex-direction: column;
`;

const FirstLine = styled(Field)`
  flex-direction: row;
  justify-content: space-between;
`;

const HeaderLabel = styled.h3`
  font-size: 1em;
  color: white;
`;

const DataLabel = styled.h4`
  margin-top: -1em;
  font-size: 0.7em;
  color: ${(props) => (props.color ? props.color : "#9A9A9A")};
`;

const statusColors = {
  Completed: "#70FF00",
  "In progress": "#FFE600",
  Rejected: "#FF0000",
};

const OrderDetails = ({ match }) => {
  const id = match.params.id;
  return (
    <Wrapper>
      <PageHeader>Order details</PageHeader>
      <Field>
        <FirstLine>
          <HeaderLabel>Status:</HeaderLabel>
          <Order>#{id}</Order>
        </FirstLine>
        <DataLabel color={statusColors["In progress"]}>In progress</DataLabel>
      </Field>
      <Field>
        <HeaderLabel>Start time:</HeaderLabel>
        <DataLabel>16:04 22-02-2020</DataLabel>
      </Field>
      <Field>
        <HeaderLabel>End time:</HeaderLabel>
        <DataLabel>17:24 22-02-2020</DataLabel>
      </Field>
      <Field>
        <HeaderLabel>Restaurant:</HeaderLabel>
        <DataLabel>ul. Kwiatowa 15/7 Katowice</DataLabel>
      </Field>
      <Field>
        <HeaderLabel>Client:</HeaderLabel>
        <DataLabel>ul. Sezamkowa 5/10 Katowice</DataLabel>
      </Field>
      <Field>
        <HeaderLabel>Distance:</HeaderLabel>
        <DataLabel>15km</DataLabel>
      </Field>
      <Field>
        <HeaderLabel>Map:</HeaderLabel>
        <SvgIcon
          alt="scooter"
          src="assets/svg/scooter.svg"
          width="80%"
        />
      </Field>
      <Button secondary margin="0.7em 0 0 0">
        Delievered
      </Button>
    </Wrapper>
  );
};

export default OrderDetails;
