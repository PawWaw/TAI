import React, { useEffect, useState } from "react";
import { Route, Switch } from "react-router-dom";
import HomeLogin from "../../features/home/HomeLogin";
import Modal from "../common/Modal";
import styled from "styled-components";
import Dashboard from "../../features/dashboard/Dashboard";
import Navbar from "../../features/nav/Navbar";
import OrderDetails from "../../features/order/OrderDetails";
import OrderList from "../../features/order/OrderList";
import Settings from "../../features/settings/Settings";
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { useRecoilValue, useSetRecoilState } from "recoil";
import { currentUserSelector, tokenSelector } from "../recoil/UserState";

const MainContainer = styled.div`
  position: relative;
  display: flex;
  padding: 30px;
  width: 100vw;
  height: 100vh;
  font-size: 20px;
  @media screen and (max-height: 600px) {
    font-size: 15px;
  }

  @media screen and (max-width: 300px) {
    font-size: 15px;
  }
  @media screen and (max-width: 300px) {
    padding: 15px;
  }
`;

const AppContainer = styled.div`
  position: relative;
  display: flex;
  flex: 1;
  flex-direction: column;
  overflow: hidden;
`;

const App = () => {

  const [appLoaded, setAppLoaded] = useState(false)
  const token = useRecoilValue(tokenSelector)
  const currentUser = useSetRecoilState(currentUserSelector)

  useEffect(() => {
    if(token) {
      currentUser().finally(() => setAppLoaded(true))
    }
    setAppLoaded(true)
  }, [])
  
  if(!appLoaded) {
    return <h1>Loading...</h1>
  }

  return (
    <>
      <Modal />
      <ToastContainer/>
      <MainContainer>
        <AppContainer>
          <Route exact path="/" component={HomeLogin} />
          <Route
            exact
            path="/(.+)"
            render={() => (
              <>
                <Navbar />
                <Switch>
                  <Route path="/dashboard" component={Dashboard} />
                  <Route exact path="/orders" component={OrderList} />
                  <Route path="/orders/:id" component={OrderDetails} />
                  <Route path="/settings" component={Settings} />
                </Switch>
              </>
            )}
          />
        </AppContainer>
      </MainContainer>
    </>
  );
};

export default App;
