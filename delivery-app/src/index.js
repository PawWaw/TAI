import React from "react";
import ReactDOM from "react-dom";
import App from "./app/layout/App";
import "./index.css";
import { Router } from "react-router-dom";
import { createBrowserHistory } from "history";
import store from "./app/redux/store";
import { Provider } from "react-redux";
import { RecoilRoot } from "recoil";

export const history = createBrowserHistory();

ReactDOM.render(
  <Router history={history}>
    <RecoilRoot>
      <Provider store={store}>
        <App />
      </Provider>
    </RecoilRoot>
  </Router>,
  document.getElementById("root")
);
