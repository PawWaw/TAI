import Axios from "axios";
import { toast } from "react-toastify";
import { history } from "../..";

Axios.defaults.baseURL = "https://localhost:44308/api";

Axios.interceptors.request.use(
    (config) => {
      const token = window.localStorage.getItem("jwt");
      if (token) {
        config.headers.Authorization = `Bearer ${token}`;
      }
      return config;
    },
    (error) => {
      return Promise.reject(error);
    }
  );

Axios.interceptors.response.use(undefined, (error) => {
  const { status, headers } = error.response;
  if (status === 401) {
    if( headers["www-authenticate"].includes("Bearer error")) {
      window.localStorage.removeItem("jwt");
      //history.push("/");
    }
  }
  else {
    throw error.response
  }
});

const responseBody = (response) => response.data;

const requests = {
  get: (url) => Axios.get(url).then(responseBody),
  post: (url, body) => Axios.post(url, body).then(responseBody),
  put: (url, body) => Axios.put(url, body).then(responseBody),
  delete: (url) => Axios.delete(url).then(responseBody),
};

const User = {
  login: (values) => requests.post("/deliverers/user/login", values),
  register: (values) => requests.post("/deliverers/user/register", values),
  current: () => requests.get("/deliverers/user"),
  updateSettings: (values) => requests.put("/deliverers/user", values),
  updatePassword: (values) => requests.put("/deliverers/user/password", values),
};

const Orders = {
  list: (param) => requests.get(`/orders/isCurrent?current=${param}`),
  details: (id) => requests.get(`/orders/${id}`),
  find: ()  => requests.get("/orders/find"),
  take: (id) => requests.post("/orders/take", { id }),
  delivered: (id) => requests.post("/orders/delivered", { id }),
};

const Dashboard = {
  statisticts: () => requests.get("/deliverers/statistics"),
};

export default {
  User,
  Dashboard,
  Orders,
};
