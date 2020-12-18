import React from "react";
import styled from "styled-components";

const Wrapper = styled.div`
    display: flex;
    flex-direction: column;
    cursor: pointer;
    align-items: center;
`;

const Logo = styled.img`
    background-color: "#F05454";
    width: 160px;
    alt: "logo";
`;

const Title = styled.div`
    display: flex;
    flex-direction: column;
`;

const Main = styled.h1`
    font-size: 2em;
    color: white;
    margin-top: -10px;
`;

const Small = styled.h2`
    font-size: 1em;
    color: #6B99C7;
    align-self: flex-end;
    margin-top: -1em;
`;

const VerticalLogo = () => {
  return (
    <Wrapper>
        <Logo src="assets/svg/dragon.svg"/>
        <Title>
            <Main>DRAGORANT</Main>
            <Small>DELIVER</Small>
        </Title>
    </Wrapper>
  );
};

export default VerticalLogo;
