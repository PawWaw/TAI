import styled, { css } from "styled-components";
import { Spinner } from "./Spinner";

const ButtonContainer = styled.div`
  display: flex;
  align-items: center;
  justify-content: center;
  border: 1px solid black;
  padding: 0.3em 1.5em;
  @media screen and (max-width: 360px) {
    padding: 0.3em 1em;
  }
  margin: ${(props) => props.margin};
  background-color: white;
  box-shadow: 2px 2px 3px 1px rgba(0, 0, 0, 0.25);
  font-family: "Varela", sans-serif;
  color: #30475e;
  width: ${(props) => (props.fluid ? "100%" : "innerWidth")};
  pointer-events: ${(props) => (props.disabled ? "none" : "auto")};
  cursor: pointer;
  font-size: ${(props) => (props.big ? "1.5em" : "1.3em")};

  &:hover {
    opacity: 0.8;
  }
  &:focus {
    outline: none;
  }
  ${(props) =>
    props.secondary &&
    css`
      background-color: #f05454;
      color: white;
    `}
`;

const Button = (props) => (
  <ButtonContainer {...props}>
    {props.loading ? <Spinner /> : props.children}
  </ButtonContainer>
);

export default Button;
