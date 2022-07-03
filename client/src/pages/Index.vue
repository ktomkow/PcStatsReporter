<template>
  <q-page class="flex flex-center column">
    <SimpleDigitalDisplay
      v-if="!!cpuAverageTemperature"
      :value="cpuAverageTemperature"
      label="CPU"
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
    <q-inner-loading :showing="isLoading">
      <q-spinner-gears size="6em" color="primary" />
    </q-inner-loading>
  </q-page>
</template>

<script>
import { reactive, toRefs, defineComponent, onMounted, onUnmounted } from "vue";
import { api } from "src/boot/axios";
import SimpleDigitalDisplay from "src/components/SimpleDigitalDisplay";

export default defineComponent({
  name: "PageIndex",
  components: { SimpleDigitalDisplay },
  setup() {
    const state = reactive({
      cpuAverageTemperature: 0,
      cpuCoresTemperatures: [],
      isLoading: true,
      intervalId: null,
    });

    onMounted(() => {
      state.intervalId = setInterval(async () => {
        try {
          const result = await api.get("api/cpu");
          const cpuData = result.data;
          console.log(cpuData);
          state.cpuAverageTemperature = calculateAverageTemperature(cpuData);
          state.cpuCoresTemperatures = mapCoreTemperatures(cpuData);
          state.isLoading = false;
        } catch (e) {
          console.error(e);
          state.isLoading = true;
        }
      }, 1000);
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

    return { ...toRefs(state) };
  },
});
</script>
