import { boot } from "quasar/wrappers";
import { eventBus } from "src/boot/eventBus";
import eventBusKeys from "src/consts/eventBusKeys";

// "async" is optional;
// more info on params: https://v2.quasar.dev/quasar-cli/boot-files
export default boot(async ({ store }) => {
  eventBus.on(eventBusKeys.PC_INFO_ARRIVED, storeData);

  function storeData(data) {
    const payload = {
      cpuName: data.cpuName,
      gpuName: data.gpuName,
      totalRam: data.totalRam,
    };

    store.dispatch("pcInfo/store", payload);
  }
});
