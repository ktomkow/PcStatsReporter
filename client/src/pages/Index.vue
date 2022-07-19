<template>
  <q-page class="flex flex-center column">
    <div class="flex row" style="gap: 2 em">
      <RamChart v-if="totalRam != 0" :total="totalRam" :used="usedRam" />
      <q-card flat bordered>
        <q-card-section>
          <LineChart
            event-bus-key="cpuTemp"
            title="CPU Temperature"
            line-color="red"
          />
        </q-card-section>
      </q-card>
      <q-card flat bordered>
        <q-card-section>
          <LineChart
            event-bus-key="gpuTemp"
            title="GPU Temperature"
            line-color="red"
          />
        </q-card-section>
      </q-card>
      <q-card flat bordered>
        <q-card-section>
          <LineChart
            event-bus-key="cpuLoad"
            title="CPU LOAD"
            line-color="blue"
          />
        </q-card-section>
      </q-card>
      <q-card flat bordered>
        <q-card-section>
          <LineChart
            event-bus-key="gpuLoad"
            title="GPU LOAD"
            line-color="green"
          />
        </q-card-section>
      </q-card>
      <LoadChart
        v-if="!!cpuAverageLoad"
        :value="cpuAverageLoad"
        title="Cpu Load"
      />
      <LoadChart v-if="!!gpuCoreLoad" :value="gpuCoreLoad" title="Gpu load" />
      <LoadBarChart
        v-if="cpuLoadData && cpuLoadData.length > 0"
        :values="cpuLoadData"
      />
      <SimpleDigitalDisplay
        v-if="!!cpuPackageTemperature"
        :value="cpuPackageTemperature"
        label="CPU Package"
        unit="℃"
        round
      />
    </div>
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
    <q-inner-loading :showing="isLoading">
      <q-spinner-gears size="6em" color="primary" />
    </q-inner-loading>
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

import eventBusKeys from "src/consts/eventBusKeys";
import { eventBus } from "src/boot/eventBus";
import { useStore } from "vuex";
import LoadChart from "src/components/LoadChart";
import LoadBarChart from "src/components/LoadBarChart";
import RamChart from "src/components/RamChart";
import LineChart from "src/components/LineChart";

export default defineComponent({
  name: "PageIndex",
  components: {
    SimpleDigitalDisplay,
    LoadChart,
    RamChart,
    LoadBarChart,
    LineChart,
  },
  setup() {
    const state = reactive({
      cpuAverageTemperature: 0,
      cpuCoresTemperatures: [],
      cpuPackageTemperature: 0,
      isLoading: true,
      cpuIntervalId: null,
      gpuIntervalId: null,
      ramIntervalId: null,
      cpuLoadData: [],
      cpuAverageLoad: null,
      gpuCoreLoad: null,
      totalRam: 0,
      usedRam: 0,
    });

    const store = useStore();

    const minTemperature = computed(
      () => store.state.statistics.temperatures.min
    );

    const maxTemperature = computed(
      () => store.state.statistics.temperatures.max
    );

    onMounted(() => {
      state.cpuIntervalId = setInterval(async () => {
        try {
          const result = await api.get("api/cpu");
          state.isLoading = false;

          const cpuData = result.data;
          state.cpuPackageTemperature = cpuData.packageTemperature;
          state.cpuAverageTemperature = calculateAverageTemperature(cpuData);
          state.cpuCoresTemperatures = mapCoreTemperatures(cpuData);
          eventBus.emit(eventBusKeys.CPU_DATA_ARRIVED, cpuData);
          eventBus.emit("cpuTemp", {
            value: cpuData.packageTemperature,
            date: new Date(),
          });
          state.cpuAverageLoad = {
            id: "AVERAGE LOAD",
            value: cpuData.averageLoad,
            isAverage: true,
          };
          state.cpuLoadData = [
            ...mapCoresLoad(cpuData),
            // ...mapCoresLoad(cpuData),
            // ...mapCoresLoad(cpuData),
            // ...mapCoresLoad(cpuData),
            // ...mapCoresLoad(cpuData),
            // ...mapCoresLoad(cpuData),
          ];
          eventBus.emit("cpuLoad", {
            value: cpuData.averageLoad,
            date: new Date(),
          });
        } catch (e) {
          console.error(e);
          state.isLoading = true;
        }
      }, 750);

      state.ramIntervalId = setInterval(async () => {
        try {
          const result = await api.get("api/ram");
          state.isLoading = false;

          const ramData = result.data;
          state.totalRam = ramData.total;
          state.usedRam = ramData.used;
        } catch (e) {
          console.error(e);
          state.isLoading = true;
        }
      }, 1000);

      state.gpuIntervalId = setInterval(async () => {
        try {
          const result = await api.get("api/nvidia");
          state.isLoading = false;

          const gpuData = result.data;
          state.gpuCoreLoad = {
            id: "CORE LOAD",
            value: gpuData.loadCore,
          };

          eventBus.emit("gpuTemp", {
            value: gpuData.loadCore,
            date: new Date(),
          });
          eventBus.emit("gpuLoad", {
            value: gpuData.temperature,
            date: new Date(),
          });
        } catch (e) {
          console.error(e);
          state.isLoading = true;
        }
      }, 1000);
    });

    onUnmounted(() => {
      if (!!state.cpuIntervalId) {
        clearInterval(state.cpuIntervalId);
      }
      if (!!state.ramIntervalId) {
        clearInterval(state.ramIntervalId);
      }
      if (!!state.gpuIntervalId) {
        clearInterval(state.gpuIntervalId);
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
      const cores = cpuData.cores.map((x) => {
        let i = 1;
        return x.load.map((y) => {
          return {
            id: x.id + "/" + i++,
            value: y,
            isAverage: false,
          };
        });
      });

      const result = [];
      for (const core of cores) {
        result.push(...core);
      }

      return result;
    };

    return { ...toRefs(state), minTemperature, maxTemperature };
  },
});
</script>
