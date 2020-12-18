import React from "react";
import styled from "styled-components";

const Wrapper = styled.div`
    display: flex;
    cursor: pointer;
    flex-direction: row;
    align-items: flex-end;
`;

const Logo = styled.img`
    background-color: "#F05454";
    height: 40px;
    alt: "logo";
`;

const Title = styled.div`
    display: flex;
    flex-direction: column;
`;

const Main = styled.h1`
    font-size: 1em;
    color: white;
    margin-top: -10px;
`;

const Small = styled.h2`
    font-size: 0.5em;
    color: #6B99C7;
    align-self: flex-end;
    margin-top: -1em;
`;

const HorizontalLogo = (props) => {
  return (
    <Wrapper {...props}>
        <Logo src="assets/svg/dragon.svg" height="38"/>
        <Title>
            <Main>DRAGORANT</Main>
            <Small>DELIVER</Small>
        </Title>
    </Wrapper>
  );
};

export default HorizontalLogo;
