import styled, { css } from "styled-components";

export const Button = styled.button`

  padding: 0.3rem 1.5rem;
  background-color: white;
  border: 1px solid black;
  font-family: "Varela", sans-serif;
  color: #30475e;
  font-size: 1.4rem;
  width: ${(props) => (props.fluid ? "100%" : "innerWidth")};
  &:focus {
    outline: none;
    opacity: 0.8;
  }

  ${(props) =>
    props.secondary &&
    css`
      background-color: #f05454;
      color: white;
    `}
`;
