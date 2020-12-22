import React, { useEffect, useState } from "react";
import styled from "styled-components";
import { ModalHeader } from "../../app/common/ModalHeader";
import { SvgIcon } from "../../app/common/SvgIcon";
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
  const [distance, setDistance] = useState(0);
  const restaurant = "ul. Kwiatowa 15/7 Katowice";
  const [ready, setReady] = useState(false);
  const [restaurantCoords, setRestaurantCoords] = useState(undefined);
  const client = "ul. Kwiatowa 15/7 Katowice";
  const [clientCoords, setClientCoords] = useState(undefined);

  useEffect(() => {
    const runSync = () => {
      findCoordinatesByAddress("Mariacka 24, 40-015 Katowice").then((c) =>
        setRestaurantCoords(c.results[0].position)
      );
      findCoordinatesByAddress("Bankowa 12, 40-007 Katowice").then((c) =>
        setClientCoords(c.results[0].position)
      );
      setReady(true);
    };
    runSync();
  }, []);

  console.log(ready);

  if (!ready) {
    return <h2>Loading</h2>;
  } else
    return (
      <>
        {console.log(clientCoords)}
        <ModalHeader>Found order!</ModalHeader>
        <Field>
          <LabelHeader>Restaurant:</LabelHeader>
          <LabelData>{restaurant}</LabelData>
        </Field>
        <Field>
          <LabelHeader>Client:</LabelHeader>
          <LabelData>{client}</LabelData>
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
              //"4.8786,52.3679:4.8798,52.3679"
              //"50.25746,19.0279:50.26078,19.02825"
              clientCoords && restaurantCoords && (
                <TomTomMap
                  locations={`${restaurantCoords.lon},${restaurantCoords.lat}:${clientCoords.lon},${clientCoords.lat}`}
                  // locations="19.0279,50.25746:19.02825,50.26078"
                  setDistance={(distance) => setDistance(distance)}
                />
              )
            }
          </MapWrapper>
        </Field>
        <ButtonsWrapper>
          <SvgIcon src="assets/svg/tick.svg" height="30px" />
          <SvgIcon src="assets/svg/reject.svg" height="30px" />
        </ButtonsWrapper>
      </>
    );
};

export default OrderPopup;
