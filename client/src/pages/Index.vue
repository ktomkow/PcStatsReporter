<template>
  <q-page class="flex flex-center column">
    <TemperatureChart event-bus-key="dupa" />

    <SimpleDigitalDisplay
      v-if="!!cpuPackageTemperature"
      :value="cpuPackageTemperature"
      label="CPU Package"
      unit="℃"
      round
    />
    <div class="flex row">
      <SimpleDigitalDisplay
        v-for="core in cpuCoresTemperatures"
        :key="core.id"
        :value="core.temperature"
        :label="'CPU #' + core.id"
        unit="℃"
        class="q-ma-sm"
      />
    </div>
    <SimpleDigitalDisplay
      v-if="!!cpuAverageTemperature"
      :value="cpuAverageTemperature"
      label="CPU Core Average"
      unit="℃"
      round
    />
    <div class="flex row">
      <div class="text-h6 q-pa-sm q-ma-sm bg-green-2">
        {{ minTemperature }} ℃
      </div>
      <div class="text-h6 q-pa-sm q-ma-sm bg-red-2">{{ maxTemperature }} ℃</div>
    </div>
    <div class="flex column flex-center">
      <div class="flex row"> </div>
    </div>
    <!-- <q-inner-loading :showing="isLoading">
      <q-spinner-gears size="6em" color="primary" />
    </q-inner-loading> -->
  </q-page>
</template>

<script>
import {
  reactive,
  toRefs,
  defineComponent,
  onMounted,
  onUnmounted,
  computed,
} from "vue";
import { api } from "src/boot/axios";
import SimpleDigitalDisplay from "src/components/SimpleDigitalDisplay";

import { useEventBus } from "src/composables/eventBusComposable";
import eventBusKeys from "src/consts/eventBusKeys";
import { eventBus } from "src/boot/eventBus";
import { useStore } from "vuex";
import TemperatureChart from "src/components/TemperatureChart";

export default defineComponent({
  name: "PageIndex",
  components: { SimpleDigitalDisplay, TemperatureChart },
  setup() {
    const state = reactive({
      cpuAverageTemperature: 0,
      cpuCoresTemperatures: [],
      cpuPackageTemperature: 0,
      isLoading: true,
      intervalId: null,
      averageLoad: 0,
      coresLoad: [],
    });

    const store = useStore();

    const minTemperature = computed(
      () => store.state.statistics.temperatures.min
    );

    const maxTemperature = computed(
      () => store.state.statistics.temperatures.max
    );

    onMounted(() => {
      state.intervalId = setInterval(async () => {
        try {
          const result = await api.get("api/cpu");
          const cpuData = result.data;
          state.cpuPackageTemperature = cpuData.packageTemperature;
          state.cpuAverageTemperature = calculateAverageTemperature(cpuData);
          state.averageLoad = cpuData.averageLoad;
          state.cpuCoresTemperatures = mapCoreTemperatures(cpuData);
          state.coresLoad = mapCoresLoad(cpuData);
          state.isLoading = false;
          eventBus.emit(eventBusKeys.CPU_DATA_ARRIVED, cpuData);
        } catch (e) {
          console.error(e);
          state.isLoading = true;
        }
      }, 100000000);
    });

    onUnmounted(() => {
      if (!!state.intervalId) {
        clearInterval(state.intervalId);
      }
    });

    const calculateAverage = (values) => {
      let sum = 0;

      if (!values || values.length === 0) {
        return null;
      }

      for (let number of values) {
        sum += number;
      }

      return sum / values.length;
    };

    const calculateAverageTemperature = (cpuData) => {
      const coresTemperature = cpuData.cores.map((x) => x.temperature);

      return calculateAverage(coresTemperature);
    };

    const mapCoreTemperatures = (cpuData) => {
      return cpuData.cores.map((x) => {
        return { id: x.id, temperature: x.temperature };
      });
    };

    const mapCoresLoad = (cpuData) => {
      return cpuData.cores.map((x) => {
        return { id: x.id, load: x.load };
      });
    };

    return { ...toRefs(state), minTemperature, maxTemperature };
  },
});
</script>
