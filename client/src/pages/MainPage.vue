<template>
  <q-page class="flex flex-center column">
    <div class="text-h1"> New edition client </div>
    <div class="cards-grid">
      <RamChart />
      <CpuTemperatureChart />
      <InfoCard />
      <GpuTemperatureChart />
      <CpuLoadTabs />
      <ColorPallete />
    </div>
    <div>
      <div class="bg-red-3 q-pa-md">{{ cpu }}</div>
      <div class="bg-green-3 q-pa-md">{{ gpu }}</div>
      <div class="bg-blue-3 q-pa-md">{{ ram }}</div>
    </div>
    <q-btn
      class="q-pa-md q-ma-md"
      color="primary"
      icon="play_arrow"
      label="start"
      @click="start"
    />
    <q-btn
      class="q-pa-md q-ma-md"
      color="primary"
      icon="pause"
      label="stop"
      @click="stop"
    />
    <q-btn color="primary" label="find Server" @click="findServer" />
  </q-page>
</template>

<script>
import { reactive, toRefs } from "vue";
import { useQuasar } from "quasar";

import InfoCard from "src/components/InfoCard";
import RamChart from "src/components/RamChart";
import CpuTemperatureChart from "src/components/CpuTemperatureChart";
import GpuTemperatureChart from "src/components/GpuTemperatureChart";
import ColorPallete from "src/components/themes/ColorPallete";

import { useEventBus } from "src/composables/eventBusComposable";
import eventBusKeys from "src/consts/eventBusKeys";

import { signalR } from "src/boot/signalr";
import CpuLoadTabs from "src/components/CpuLoadTabs.vue";
import { find } from "src/services/serverFinder";
import { api } from "src/boot/axios";

export default {
  name: "MainPage",
  components: {
    InfoCard,
    RamChart,
    CpuTemperatureChart,
    GpuTemperatureChart,
    CpuLoadTabs,
    ColorPallete,
  },
  setup() {
    const state = reactive({ cpu: "", gpu: "", ram: "" });
    const q = useQuasar();

    function showRegisterData(data) {
      const string = JSON.stringify(data);
      q.notify(string);
    }

    function setCpuSample(data) {
      state.cpu = JSON.stringify(data);
    }
    function setGpuSample(data) {
      state.gpu = JSON.stringify(data);
    }
    function setRamSample(data) {
      state.ram = JSON.stringify(data);
    }

    useEventBus(eventBusKeys.PC_INFO_ARRIVED, showRegisterData);
    useEventBus(eventBusKeys.CPU_SAMPLE_ARRIVED, setCpuSample);
    useEventBus(eventBusKeys.GPU_SAMPLE_ARRIVED, setGpuSample);
    useEventBus(eventBusKeys.RAM_SAMPLE_ARRIVED, setRamSample);

    const start = async () => {
      // await signalR.connect("http://localhost:11111", "reporter");
      if (api.isInitialized) {
        await signalR.connect(api.baseAddress, "reporter");
      }
    };

    const stop = () => {
      signalR.disconnect;
    };

    const findServer = async () => {
      await find();
    };

    return { ...toRefs(state), start, stop, findServer };
  },
};
</script>

<style lang="scss" scoped>
.cards-grid {
  display: grid;
  grid-template-columns: auto auto auto auto auto;
  grid-template-rows: auto auto;
  grid-gap: 1em;
  grid-auto-flow: column;
}
</style>
