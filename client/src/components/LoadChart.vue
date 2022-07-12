<template>
  <q-card flat bordered>
    <q-card-section>
      <VChart :option="options" style="height: 30em; width: 30em" />
    </q-card-section>
  </q-card>
</template>

<script>
import { reactive, toRefs, ref, computed } from "vue";

import VChart from "vue-echarts";
import { use } from "echarts";

import {
  TitleComponent,
  TooltipComponent,
  GridComponent,
} from "echarts/components";
import { GaugeChart } from "echarts/charts";
import { CanvasRenderer } from "echarts/renderers";

use([
  TitleComponent,
  TooltipComponent,
  GridComponent,
  GaugeChart,
  CanvasRenderer,
]);

export default {
  name: "LoadChart",
  components: { VChart },
  props: {
    values: {
      type: Array,
      required: true,
    },
  },
  setup(props) {
    const state = reactive({});

    const gaugeData = computed(() => {
      return props.values.map((x) => {
        return {
          value: x.value,
          name: x.id,
          title: x.isAverage
            ? {}
            : {
                show: false,
              },
          detail: x.isAverage
            ? {}
            : {
                show: false,
              },
          itemStyle: x.isAverage
            ? {
                color: "#26a69a",
              }
            : { color: "#1976d2" },
        };
      });
    });

    const options = ref({
      series: [
        {
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
          data: gaugeData,
          title: {
            fontSize: 14,
          },
          detail: {
            width: 50,
            height: 14,
            fontSize: 14,
            color: "inherit",
            borderColor: "inherit",
            borderRadius: 20,
            borderWidth: 1,
            formatter: "{value}%",
          },
        },
      ],
    });

    return { ...toRefs(state), options };
  },
};
</script>

<style lang="scss" scoped></style>
