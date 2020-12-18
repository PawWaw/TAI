import React from 'react'
import styled from "styled-components"

const Wrapper = styled.div`
    display: flex;
    flex-direction: row;
    align-items: center;
    opacity: 85%;
`;

const Line = styled.div`
    background-color: #FFF;
    flex: 1;
    height: 2px;
`;

const Text = styled.p`
    font-size: 0.7rem;
    color: white;
    margin: 0 10px;
`;


const Separator = ({margin}) => {
    return (
        <Wrapper style={{margin: margin}}>
            <Line/>
            <Text>or</Text>
            <Line/>
        </Wrapper>
    )
}

export default Separator
