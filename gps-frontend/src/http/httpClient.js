import axios from "axios";

const httpClient = axios.create({
  baseURL: "http://localhost:5141/api",
  headers: {
    "Content-Type": "application/json",
  },
  timeout: 10000,
});

// Request interceptor
httpClient.interceptors.request.use(
  (config) => {
    console.log("Request:", config.url);
    return config;
  },
  (error) => Promise.reject(error),
);

// Response interceptor (AUTO UNWRAP ApiResponse)
httpClient.interceptors.response.use(
  (response) => {
    const res = response.data;

    // Handle API-level errors
    if (!res?.success) {
      return Promise.reject({
        message: res?.message || "API Error",
        status: response.status,
      });
    }

    // Return only the actual payload
    return res.data;
  },
  (error) => {
    // 🔥 Preserve cancellation errors (IMPORTANT)
    if (axios.isCancel(error) || error.code === "ERR_CANCELED") {
      return Promise.reject(error);
    }

    // Handle network / server errors
    return Promise.reject({
      message:
        error.response?.data?.message || error.message || "Unexpected error",
      status: error.response?.status || 500,
    });
  },
);
export const createCancelableRequest = () => {
  const controller = new AbortController();

  return {
    signal: controller.signal,
    cancel: () => controller.abort(),
  };
};

export default httpClient;
