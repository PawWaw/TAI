import Axios from "axios";

Axios.defaults.baseURL = "http://localhost:5000/deliveryapi";

// Axios.interceptors.request.use(
//     (config) => {
//       const token = window.localStorage.getItem("jwt");
//       if (token) {
//         config.headers.Authorization = `Bearer ${token}`;
//       }
//       return config;
//     },
//     (error) => {
//       return Promise.reject(error);
//     }
//   );

// Axios.interceptors.response.use(undefined, (error) => {
//     const { status, headers } = error.response;
//     if (
//       status === 401 &&
//       headers["www-uthenticate"].includes(
//         'Bearer error="invalid_token", error_description="The token expired'
//       )
//     ) {
//       window.localStorage.removeItem("jwt");
//       history.push("/");
//       toast.info("Your session has expired. Please login again");
//     }
//     // if (status === 500) {
//     //   toast.error("Server error - check terminal for more info!");
//     // }
//     throw error.response;
//   });

const responseBody = (response) => response.data;

const requests = {
  get: (url) => Axios.get(url).then(responseBody),
  post: (url, body) => Axios.post(url, body).then(responseBody),
  put: (url, body) => Axios.put(url, body).then(responseBody),
  delete: (url) => Axios.delete(url).then(responseBody),
};
