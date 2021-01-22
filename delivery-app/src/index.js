import React from "react";
import ReactDOM from "react-dom";
import App from "./app/layout/App";
import "./index.css";
import { Router } from "react-router-dom";
import { RecoilRoot } from "recoil";
import { createBrowserHistory } from "history";

export const history = createBrowserHistory();

ReactDOM.render(
  <Router history={history}>
    <RecoilRoot>
      <App />
    </RecoilRoot>
  </Router>,
  document.getElementById("root")
);
