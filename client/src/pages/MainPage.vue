<template>
  <q-page class="flex flex-center column">
    <div class="text-h1"> New edition client </div>
    <InfoCard />
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
  </q-page>
</template>

<script>
import { reactive, toRefs, computed } from "vue";
import { useStore } from "vuex";
import { useQuasar } from "quasar";
import { useRouter } from "vue-router";

import InfoCard from "src/components/InfoCard";

import { useEventBus } from "src/composables/eventBusComposable";
import eventBusKeys from "src/consts/eventBusKeys";

import { signalR } from "src/boot/signalr";

export default {
  name: "MainPage",
  components: { InfoCard },
  setup(props) {
    const state = reactive({ cpu: "", gpu: "", ram: "" });
    const store = useStore();
    const router = useRouter();
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
      await signalR.connect("http://localhost:11111", "reporter");
    };

    const stop = () => {
      signalR.disconnect;
    };

    return { ...toRefs(state), start, stop };
  },
};
</script>

<style lang="scss" scoped></style>
