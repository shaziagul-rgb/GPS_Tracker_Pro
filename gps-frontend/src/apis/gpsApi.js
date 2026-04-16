import httpClient from "../http/httpClient";

export const getGpsData = (signal) => {
  return httpClient.get("/gps", {
    signal,
  });
};
