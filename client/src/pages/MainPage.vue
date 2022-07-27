<template>
  <q-page class="flex flex-center column">
    <div class="text-h1"> New edition client </div>
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

import { useEventBus } from "src/composables/eventBusComposable";
import eventBusKeys from "src/consts/eventBusKeys";

import { signalR } from "src/boot/signalr";

export default {
  name: "MainPage",
  setup(props) {
    const state = reactive({});
    const store = useStore();
    const router = useRouter();
    const q = useQuasar();

    function showRegisterData(data) {
      const string = JSON.stringify(data);
      q.notify(string);
    }

    useEventBus(eventBusKeys.PC_INFO_ARRIVED, showRegisterData);

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
