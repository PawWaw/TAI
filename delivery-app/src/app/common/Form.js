import styled from "styled-components";

export const Form = styled.form`

`;

export const Field = styled.div`
    display: flex;
    flex-direction: column;
    flex: 1;
`;

export const Label = styled.div`
    font-size: 1rem;
    color: white;
`;

export const Input = styled.input`
    width: 100%;
    color: black;
    border: none;
    padding: 0 0.3rem;
    border: 1px solid black;
    &:focus {
        outline: none;
    }
`;