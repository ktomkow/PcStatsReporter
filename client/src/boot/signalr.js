import { boot } from "quasar/wrappers";
import { eventBus } from "src/boot/eventBus";
import eventBusKeys from "src/consts/eventBusKeys";

import { HubConnectionBuilder } from "@microsoft/signalr";

const signalR = {
  connection: null,
  isConnected: function () {
    return this.connection !== null;
  },
  connect: async function (url, hubName) {
    try {
      this.connection = new HubConnectionBuilder()
        .withUrl(url + "/" + hubName)
        .build();

      this.connection.on("registerBasicData", (pcInfoData) => {
        eventBus.emit(eventBusKeys.PC_INFO_ARRIVED, pcInfoData);
      });

      this.connection.on("handleCpuData", (data) => {
        eventBus.emit(eventBusKeys.CPU_SAMPLE_ARRIVED, data);
      });

      this.connection.on("handleGpuData", (data) => {
        eventBus.emit(eventBusKeys.GPU_SAMPLE_ARRIVED, data);
      });

      this.connection.on("handleRamData", (data) => {
        eventBus.emit(eventBusKeys.RAM_SAMPLE_ARRIVED, data);
      });

      await this.connection.start();

      return { result: true };
    } catch (e) {
      console.warn("SignalR connection error", e);
      this.connection = null;
      return { result: false };
    }
  },
  disconnect: function () {
    this.connection.disconnect();
    this.connection = null;
  },
};

// "async" is optional;
// more info on params: https://v2.quasar.dev/quasar-cli/boot-files
export default boot(async (/* { app, router, ... } */) => {
  // await signalR.connect("http://localhost:11111", "reporter");
});

export { signalR };
