<template>
  <Segment size="md" :is-loading="isLoading">
    <VChart v-if="!isLoading" :option="options" />
  </Segment>
</template>

<script>
import { reactive, toRefs, computed, ref, watch } from "vue";
import { useStore } from "vuex";

import { useEventBus } from "src/composables/eventBusComposable";
import eventBusKeys from "src/consts/eventBusKeys";

import VChart from "vue-echarts";
import { use } from "echarts";
import Segment from "src/components/Segment";

import {
  TitleComponent,
  TooltipComponent,
  GridComponent,
} from "echarts/components";
import { GaugeChart } from "echarts/charts";
import { CanvasRenderer } from "echarts/renderers";
import { roundToString } from "src/services/mathService";

use([
  TitleComponent,
  TooltipComponent,
  GridComponent,
  GaugeChart,
  CanvasRenderer,
]);

export default {
  name: "RamChart",
  components: { VChart, Segment },
  setup() {
    const state = reactive({ usedRam: null });
    const store = useStore();

    const totalRam = computed(() => store.state.pcInfo.totalRam);
    const totalRamString = computed(() => roundToString(totalRam.value, 2));

    useEventBus(eventBusKeys.RAM_SAMPLE_ARRIVED, setRamValue);

    function setRamValue(data) {
      state.usedRam = roundToString(data.inUse, 2);
    }

    const ramValue = computed(() => {
      return [
        {
          value: state.usedRam,
          name: "RAM",
        },
      ];
    });

    const isLoading = computed(() => !totalRam.value || !state.usedRam);

    watch(totalRam, (newValue) => {
      options.value.series[0].max = newValue;
    });

    const options = ref({
      title: {
        text: "RAM Usage",
      },
      series: [
        {
          min: 0,
          max: totalRam.value ?? 0,
          type: "gauge",
          startAngle: 90,
          endAngle: -270,
          pointer: {
            show: false,
          },
          progress: {
            show: true,
            overlap: false,
            roundCap: false,
            clip: false,
            itemStyle: {
              borderWidth: 2,
              borderColor: "#464646",
            },
          },

          axisLine: {
            lineStyle: {
              width: 50,
            },
          },
          splitLine: {
            show: false,
            distance: 0,
            length: 10,
          },
          axisTick: {
            show: false,
          },
          axisLabel: {
            show: false,
            distance: 50,
          },
          data: ramValue,
          title: {
            fontSize: 14,
          },
          detail: {
            width: 130,
            height: 14,
            fontSize: 14,
            color: "inherit",
            borderColor: "inherit",
            borderRadius: 20,
            borderWidth: 1,
            formatter: (v) => {
              if (isLoading.value) {
                return "Loading..";
              }

              return (
                roundToString(v, 2) + " GB / " + totalRamString.value + " GB"
              );
            },
          },
        },
      ],
    });

    return { ...toRefs(state), options, isLoading };
  },
};
</script>

<style lang="scss" scoped></style>
