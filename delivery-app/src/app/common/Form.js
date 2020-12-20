import styled from "styled-components";
import React from "react";

export const Form = styled.form`
  width: 100%;
`;

export const Field = styled.div`
  display: flex;
  flex-direction: column;
  flex: 1;
`;

export const Label = styled.h2`
  font-size: 1em;
  color: ${(props) => props.dark ? "black" : "white"};
`;

export const Input = styled.input`
  width: 100%;
  color: black;
  border: none;
  padding: 0 0.3em;
  font-size: 1em;
  border: 1px solid black;
  &:focus {
    outline: none;
  }
`;

const InputBorder = styled.div`
  border: 1px solid black;
  align-items: center;
  display: flex;
  width: 100%;
  background: white;
  flex-direction: row;
  overflow-x: hidden;
  margin-bottom: 20px;
`;

const CleanInput = styled.input`
  color: black;
  font-size: 1em;
  width: 100%;
  border: none;
  padding: 0;
  &:focus {
    outline: none;
  }
`;

export const IconInput = (props) => {
  return (
    <InputBorder {...props}>
      <img
        src={props.icon}
        alt={props.icon}
        height="22px"
        style={{ margin: "0 5px" }}
      />
      <CleanInput {...props} />
    </InputBorder>
  );
};
