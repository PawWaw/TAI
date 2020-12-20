import React from "react";
import { useRecoilState } from "recoil";
import styled from "styled-components";
import { modalState } from "../recoil/ModalState";

const Background = styled.div`
  z-index: 100;
  position: absolute;
  width: 100%;
  height: 100%;
  display: flex;
  align-items: center;
  justify-content: center;
  background-color: rgba(0, 0, 0, 0.85);
  visibility: ${(props) => (props.opened ? "visible" : "hidden")};
`;

const Container = styled.div`
  width: 80%;
  max-width: 700px;
  display: flex;
  flex-direction: column;
  background-color: #f3f3f3;
  padding: 15px;
  color: black;
`;

const Modal = () => {
  const [modal, setModal] = useRecoilState(modalState);
  const closeModal = () => {
    setModal({
      opened: false,
      body: null,
    });
  };
  return (
    <Background opened={modal.opened}>
      <Container>
        <img
          alt="close"
          src="assets/svg/close.svg"
          height="15px"
          style={{ alignSelf: "flex-end" }}
          onClick={closeModal}
        />
        {modal.body}
      </Container>
    </Background>
  );
};

export default Modal;
