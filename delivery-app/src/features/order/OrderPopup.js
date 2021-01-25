import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useSetRecoilState } from "recoil";
import styled from "styled-components";
import { ModalHeader } from "../../app/common/ModalHeader";
import { SvgIcon } from "../../app/common/SvgIcon";
import { modalState } from "../../app/recoil/ModalState";
import {
  declineOrder,
  selectDashboardState,
  takeOrder,
} from "../../app/redux/dashboardSlice";
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
  const dispatch = useDispatch();
  const { currentOrder } = useSelector(selectDashboardState);
  const setModal = useSetRecoilState(modalState);

  const [distance, setDistance] = useState(0);
  const [ready, setReady] = useState(false);
  const [restaurantCoords, setRestaurantCoords] = useState(undefined);
  const [clientCoords, setClientCoords] = useState(undefined);

  useEffect(() => {
    const runSync = () => {
      
      findCoordinatesByAddress(currentOrder?.restaurant?.address + " " + currentOrder?.restaurant?.city).then((c) =>
        setRestaurantCoords(c.results[0]?.position)
      );
      findCoordinatesByAddress(currentOrder?.client?.address + " " + currentOrder?.client?.city).then((c) =>
        setClientCoords(c.results[0]?.position)
      );
      setReady(true);
    };
    runSync();
  }, [currentOrder]);

  const handleTakeOrder = () => {
    dispatch(takeOrder(currentOrder.id)).then(() => {
      setModal({
        opened: false,
        body: null,
      });
    });
}

  const handleDeclineOrder = () => {
    setModal({
      opened: false,
      body: null,
    });
    dispatch(declineOrder());
  };

  if (!ready) {
    return <h2>Loading</h2>;
  } else
    return (
      <>
        <ModalHeader>Found order!</ModalHeader>
        <Field>
          <LabelHeader>Restaurant:</LabelHeader>
          <LabelData>{currentOrder?.restaurant?.address + " " + currentOrder?.restaurant?.city}</LabelData>
        </Field>
        <Field>
          <LabelHeader>Client:</LabelHeader>
          <LabelData>{currentOrder?.client?.address + " " + currentOrder?.client?.city}</LabelData>
        </Field>
        <Field>
          <LabelHeader>Distance:</LabelHeader>
          <LabelData>{distance} km</LabelData>
        </Field>
        <Field>
          <LabelHeader>Map:</LabelHeader>
          <MapWrapper>
            {clientCoords && restaurantCoords && (
              <TomTomMap
                locations={`${restaurantCoords?.lon},${restaurantCoords?.lat}:${clientCoords?.lon},${clientCoords?.lat}`}
                setDistance={(distance) => setDistance(distance)}
              />
            )}
          </MapWrapper>
        </Field>
        <ButtonsWrapper>
          <SvgIcon
            src="assets/svg/tick.svg"
            height="30px"
            onClick={handleTakeOrder}
          />
          <SvgIcon
            src="assets/svg/reject.svg"
            height="30px"
            onClick={handleDeclineOrder}
          />
        </ButtonsWrapper>
      </>
    );
};

export default OrderPopup;
