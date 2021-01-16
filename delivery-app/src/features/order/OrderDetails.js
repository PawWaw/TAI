import { format } from "date-fns/esm";
import React, { useEffect, useState } from "react";
import { useRecoilValue, useSetRecoilState } from "recoil";
import styled from "styled-components";
import Button from "../../app/common/Button";
import { PageHeader } from "../../app/common/PageHeader";
import { SvgIcon } from "../../app/common/SvgIcon";
import {
  delieveredOrderSelector,
  selectedOrderState,
  selectOrderSelector,
} from "../../app/recoil/SelectedOrderState";
import { findCoordinatesByAddress } from "../../app/util/util";
import TomTomMap from "../map/TomTomMap";

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

const MapField = styled(Field)`
  flex: 1;
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
  const [distance, setDistance] = useState(0);
  const order = useRecoilValue(selectedOrderState);
  const selectOrder = useSetRecoilState(selectOrderSelector);
  const deliveredOrder = useSetRecoilState(delieveredOrderSelector)
  const [restaurantCoords, setRestaurantCoords] = useState(undefined);
  const [clientCoords, setClientCoords] = useState(undefined);

  useEffect(() => {
    selectOrder(match.params.id);
  }, []);

  useEffect(() => {
    const runSync = () => {
      if (order !== null) {
        findCoordinatesByAddress(order.restaurant).then((c) =>
          setRestaurantCoords(c.results[0].position)
        );
        findCoordinatesByAddress(order.client).then((c) =>
          setClientCoords(c.results[0].position)
        );
      }
    };
    runSync();
  }, [order]);

  if (order === null) {
    return <h2>Loading...</h2>;
  }
  return (
    <Wrapper>
      <PageHeader>Order details</PageHeader>
      <Field>
        <FirstLine>
          <HeaderLabel>Status:</HeaderLabel>
          <Order>#{order.id}</Order>
        </FirstLine>
        <DataLabel color={statusColors[order.status]}>In progress</DataLabel>
      </Field>
      <Field>
        <HeaderLabel>Start time:</HeaderLabel>
        <DataLabel>
          {format(new Date(order?.startDate), "HH:mm dd-MM-yyy")}
        </DataLabel>
      </Field>
      <Field>
        <HeaderLabel>End time:</HeaderLabel>
        <DataLabel>
          {format(new Date(order?.endDate), "HH:mm dd-MM-yyy")}
        </DataLabel>
      </Field>
      <Field>
        <HeaderLabel>Restaurant:</HeaderLabel>
        <DataLabel>{order?.restaurant}</DataLabel>
      </Field>
      <Field>
        <HeaderLabel>Client:</HeaderLabel>
        <DataLabel>{order?.client}</DataLabel>
      </Field>
      <Field>
        <HeaderLabel>Distance:</HeaderLabel>
        <DataLabel>{distance} km</DataLabel>
      </Field>
      <MapField>
        <HeaderLabel>Map:</HeaderLabel>
        <TomTomMap
             locations={`${restaurantCoords?.lon},${restaurantCoords?.lat}:${clientCoords?.lon},${clientCoords?.lat}`}
          setDistance={(distance) => setDistance(distance)}
        />
      </MapField>
      <Button secondary margin="0.5em 0 0 0" onClick={deliveredOrder}>
        Delivered
      </Button>
    </Wrapper>
  );
};

export default OrderDetails;
