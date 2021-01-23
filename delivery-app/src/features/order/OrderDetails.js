import { format } from "date-fns/esm";
import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import styled from "styled-components";
import { history } from "../..";
import Button from "../../app/common/Button";
import { PageHeader } from "../../app/common/PageHeader";
import {
  deliverOrderById,
  fetchOrderById,
  selectOrdersManagerState,
} from "../../app/redux/ordersManagerSlice";
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

// const statusColors = {
//   Completed: "#70FF00",
//   "In progress": "#FFE600",
//   Rejected: "#FF0000",
// };

const OrderDetails = ({ match }) => {
  const [distance, setDistance] = useState(0);
  const dispatch = useDispatch();
  const { detailedOrder, loading } = useSelector(selectOrdersManagerState);
  const [restaurantCoords, setRestaurantCoords] = useState(undefined);
  const [clientCoords, setClientCoords] = useState(undefined);

  const handleDeliverOrder = () => {
    dispatch(deliverOrderById(detailedOrder?.id)).then(() =>
      history.push("/dashboard")
    );
  };

  useEffect(() => {
    dispatch(fetchOrderById(match.params.id));
  }, []);

  useEffect(() => {
    const runSync = () => {
      if (detailedOrder !== null) {
        findCoordinatesByAddress(
          detailedOrder?.restaurant?.address +
            " " +
            detailedOrder?.restaurant?.city
        ).then((c) => setRestaurantCoords(c.results[0].position));
        findCoordinatesByAddress(
          detailedOrder?.client?.address + " " + detailedOrder?.client?.city
        ).then((c) => setClientCoords(c.results[0].position));
      }
    };
    runSync();
  }, [detailedOrder]);

  if (detailedOrder === null) {
    return <h2>Loading...</h2>;
  }
  return (
    <Wrapper>
      <PageHeader>Order details</PageHeader>
      <Field>
        <FirstLine>
          <HeaderLabel>Status:</HeaderLabel>
          <Order>#{detailedOrder?.id}</Order>
        </FirstLine>
        <DataLabel color="#FFE600">{detailedOrder?.status}</DataLabel>
      </Field>
      <Field>
        <HeaderLabel>Start time:</HeaderLabel>
        <DataLabel>
          {format(new Date(detailedOrder?.startDate), "HH:mm dd-MM-yyy")}
        </DataLabel>
      </Field>
      <Field>
        <HeaderLabel>End time:</HeaderLabel>
        <DataLabel>
          {detailedOrder?.endDate ? (
            format(new Date(detailedOrder?.endDate), "HH:mm dd-MM-yyy")
          ) : ("-")}
        </DataLabel>
      </Field>
      <Field>
        <HeaderLabel>Restaurant:</HeaderLabel>
        <DataLabel>
          {detailedOrder?.restaurant?.address +
            " " +
            detailedOrder?.restaurant?.city}
        </DataLabel>
      </Field>
      <Field>
        <HeaderLabel>Client:</HeaderLabel>
        <DataLabel>
          {detailedOrder?.client?.address + " " + detailedOrder?.client?.city}
        </DataLabel>
      </Field>
      <Field>
        <HeaderLabel>Distance:</HeaderLabel>
        <DataLabel>{distance} km</DataLabel>
      </Field>
      <MapField>
        <HeaderLabel>Map:</HeaderLabel>
        {clientCoords && restaurantCoords && (
          <TomTomMap
            locations={`${restaurantCoords?.lon},${restaurantCoords?.lat}:${clientCoords?.lon},${clientCoords?.lat}`}
            setDistance={(distance) => setDistance(distance)}
          />
        )}
      </MapField>
      {detailedOrder?.status !== "ENDED" && (
        <Button
          secondary
          margin="0.5em 0 0 0"
          onClick={handleDeliverOrder}
          loading={loading ? 1 : 0}
        >
          Delivered
        </Button>
      )}
    </Wrapper>
  );
};

export default OrderDetails;
