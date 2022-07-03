import { boot } from "quasar/wrappers";
import { eventBus } from "src/boot/eventBus";
import eventBusKeys from "src/consts/eventBusKeys";

// "async" is optional;
// more info on params: https://v2.quasar.dev/quasar-cli/boot-files
export default boot(async ({ store }) => {
  eventBus.on(eventBusKeys.CPU_DATA_ARRIVED, handleCpuDataArrived);
});

const handleCpuDataArrived = (data) => {
  console.log("handleCpuDataArrived", data);
};
