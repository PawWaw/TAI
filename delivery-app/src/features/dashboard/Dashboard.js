import React from 'react'
import styled from "styled-components"
import OrderInfo from './OrderInfo';
import Statistics from './Statistics';


const Wrapper = styled.div`
    display: flex;
    flex: 1;
    flex-direction: column;
    align-items: center;
    justify-content: space-evenly;
`;

const Dashboard = () => {
    return (
        <Wrapper>
            <OrderInfo/>
            <Statistics/>
        </Wrapper>
    )
}

export default Dashboard
