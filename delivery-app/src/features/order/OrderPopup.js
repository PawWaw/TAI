import React, { useEffect, useState } from "react";
import { useRecoilValue, useSetRecoilState } from "recoil";
import styled from "styled-components";
import { ModalHeader } from "../../app/common/ModalHeader";
import { SvgIcon } from "../../app/common/SvgIcon";
import { acceptOrderSelector, declineOrderSelector, newOrderState } from "../../app/recoil/NewOrderState";
import { findCoordinatesByAddress } from "../../app/util/util";
import TomTomMap from "../map/TomTomMap";

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

const MapWrapper = styled.div`
  border: 0.5px solid black;
  height: 20vh;
`;

const ButtonsWrapper = styled.div`
  display: flex;
  flex-direction: row;
  justify-content: space-around;
  margin-top: 15px;
`;

const OrderPopup = () => {
  const order = useRecoilValue(newOrderState)
  const declineOrder = useSetRecoilState(declineOrderSelector)
  const acceptOrder = useSetRecoilState(acceptOrderSelector)
  const [distance, setDistance] = useState(0);
  const [ready, setReady] = useState(false);
  const [restaurantCoords, setRestaurantCoords] = useState(undefined);
  const [clientCoords, setClientCoords] = useState(undefined);

  useEffect(() => {
    const runSync = () => {
      findCoordinatesByAddress(order.restaurant).then((c) =>
        setRestaurantCoords(c.results[0].position)
      );
      findCoordinatesByAddress(order.client).then((c) =>
        setClientCoords(c.results[0].position)
      );
      setReady(true);
    };
    runSync();
  }, []);


  if (!ready) {
    return <h2>Loading</h2>;
  } else
    return (
      <>
        {console.log(clientCoords)}
        <ModalHeader>Found order!</ModalHeader>
        <Field>
          <LabelHeader>Restaurant:</LabelHeader>
          <LabelData>{order.restaurant}</LabelData>
        </Field>
        <Field>
          <LabelHeader>Client:</LabelHeader>
          <LabelData>{order.client}</LabelData>
        </Field>
        <Field>
          <LabelHeader>Distance:</LabelHeader>
          <LabelData>{distance} km</LabelData>
        </Field>
        <Field>
          <LabelHeader>Map:</LabelHeader>
          {console.log(restaurantCoords)}
          <MapWrapper>
            {
              clientCoords && restaurantCoords && (
                <TomTomMap
                  locations={`${restaurantCoords.lon},${restaurantCoords.lat}:${clientCoords.lon},${clientCoords.lat}`}
                  setDistance={(distance) => setDistance(distance)}
                />
              )
            }
          </MapWrapper>
        </Field>
        <ButtonsWrapper>
          <SvgIcon src="assets/svg/tick.svg" height="30px" onClick={acceptOrder} />
          <SvgIcon src="assets/svg/reject.svg" height="30px" onClick={declineOrder} />
        </ButtonsWrapper>
      </>
    );
};

export default OrderPopup;
