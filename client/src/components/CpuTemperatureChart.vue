<template>
  <Segment :is-loading="isLoading" size="md">
    <LineChart
      :event-bus-key="chartBusKey"
      title="CPU Temperature"
      line-color="red"
    />
  </Segment>
</template>

<script>
import { reactive, toRefs, computed } from "vue";

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

    const chartBusKey = "mainCpuTemperature";

    useEventBus(eventBusKeys.CPU_SAMPLE_ARRIVED, cpuSampleArrived);

    function cpuSampleArrived(data) {
      state.isLoading = false;

      eventBus.emit(chartBusKey, {
        value: data.temperature,
        date: data.registeredAt,
      });
    }

    return { ...toRefs(state), chartBusKey };
  },
};
</script>

<style lang="scss" scoped></style>
