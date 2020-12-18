import React from "react";
import { Route, Switch } from "react-router-dom";
import HomeLogin from "../../features/home/HomeLogin";
import Modal from "../common/Modal";
import styled from "styled-components";
import Dashboard from "../../features/dashboard/Dashboard";
import Navbar from "../../features/nav/Navbar";

const MainContainer = styled.div`
  display: flex;
  width: 100vw;
  height: 100vh;
  font-size: 20px;
  @media screen and (max-height: 600px) {
    font-size: 15px;
  }
`;

const AppContainer = styled.div`
  display: flex;
  flex: 1;
  padding: 30px;
  flex-direction: column;
`;

const App = () => {
  return (
    <MainContainer>
      <Modal />
      <Route exact path="/" component={HomeLogin} />
      <Route
        exact
        path="/(.+)"
        render={() => (
          <AppContainer>
            <Navbar />
            <Switch>
              <Route path="/dashboard" component={Dashboard} />
            </Switch>
          </AppContainer>
        )}
      />
    </MainContainer>
  );
};

export default App;
