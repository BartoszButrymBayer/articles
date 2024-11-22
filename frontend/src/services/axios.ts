import axios from "axios";

const axiosConfig = {
  baseURL: "http://localhost:5081/api",
  headers: { "Content-Type": "application/json" },
};

export default axios.create({ ...axiosConfig });
