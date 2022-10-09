<template>
  <Segment :is-loading="isLoading" size="md">
    <LineChart
      :event-bus-key="chartBusKey"
      title="GPU Temperature"
      line-color="green"
    />
  </Segment>
</template>

<script>
import { reactive, toRefs } from "vue";

import Segment from "src/components/Segment";

import { useEventBus } from "src/composables/eventBusComposable";
import eventBusKeys from "src/consts/eventBusKeys";
import { eventBus } from "src/boot/eventBus";

import LineChart from "src/components/LineChart";

export default {
  name: "CpuTemperatureChart",
  components: { Segment, LineChart },
  setup() {
    const state = reactive({ isLoading: true });

    const chartBusKey = "mainGpuTemperature";

    useEventBus(eventBusKeys.GPU_SAMPLE_ARRIVED, cpuSampleArrived);

    function cpuSampleArrived(data) {
      state.isLoading = false;

      eventBus.emit(chartBusKey, {
        value: data.coreTemperature,
        date: new Date(),
      });
    }

    return { ...toRefs(state), chartBusKey };
  },
};
</script>

<style lang="scss" scoped></style>
