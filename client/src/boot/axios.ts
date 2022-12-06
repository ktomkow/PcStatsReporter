import { boot } from "quasar/wrappers";
import axios, { AxiosInstance } from "axios";
import { find } from "src/services/serverFinder";

export interface Api {
  isInitialized: boolean;
  axios?: AxiosInstance;
  baseAddress?: string;
}

// Be careful when using SSR for cross-request state pollution
// due to creating a Singleton instance here;
// If any client changes this (global) instance, it might be a
// good idea to move this instance creation inside of the
// "export default () => {}" function below (which runs individually
// for each client)
// const api = axios.create({ baseURL: "http://localhost:11111" });
const api: Api = {
  isInitialized: false,
};

export default boot(async () => {
  setTimeout(async () => {
    const baseAddressScanResult = await find();

    if (baseAddressScanResult.isSuccess === false) {
      throw "Not found base address!";
    }

    console.log("API address found", baseAddressScanResult);

    const axiosInstance = axios.create({
      baseURL: baseAddressScanResult.address,
    });

    api.axios = axiosInstance;
    api.isInitialized = true;
    api.baseAddress = baseAddressScanResult.address;
  }, 0);
});

export { api };
