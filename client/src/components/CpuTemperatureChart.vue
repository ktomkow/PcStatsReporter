<template>
  <Segment :is-loading="isLoading" size="md">
    <LineChart
      :event-bus-key="chartBusKey"
      title="CPU Temperature"
      :line-color="lineColor"
    />
  </Segment>
</template>

<script>
import { reactive, toRefs, ref } from "vue";
import { colors } from "quasar";

import Segment from "src/components/Segment";

import { useEventBus } from "src/composables/eventBusComposable";
import eventBusKeys from "src/consts/eventBusKeys";
import { eventBus } from "src/boot/eventBus";
import ColorId from "src/models/Colors/ColorId";

import LineChart from "src/components/LineChart";

export default {
  name: "CpuTemperatureChart",
  components: { Segment, LineChart },
  setup() {
    const { getPaletteColor } = colors;

    const state = reactive({ isLoading: true });
    const chartBusKey = "mainCpuTemperature";

    useEventBus(eventBusKeys.CPU_SAMPLE_ARRIVED, cpuSampleArrived);

    const lineColor = ref(getPaletteColor(ColorId.CpuTempLine));
    useEventBus(eventBusKeys.COLOR_UPDATED, colorChanged);
    function colorChanged(newColor) {
      if (newColor.id === ColorId.CpuTempLine) {
        lineColor.value = newColor.value;
      }
    }

    function cpuSampleArrived(data) {
      state.isLoading = false;

      eventBus.emit(chartBusKey, {
        value: data.temperature,
        date: data.registeredAt,
      });
    }

    return { ...toRefs(state), chartBusKey, lineColor };
  },
};
</script>

<style lang="scss" scoped></style>
