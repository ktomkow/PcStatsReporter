<template>
  <q-card flat bordered>
    <q-card-section>
      <VChart :option="options" style="height: 30em; width: 30em" />
    </q-card-section>
  </q-card>
</template>

<script>
import { reactive, toRefs, computed, ref } from "vue";

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
  name: "RamChart",
  components: { VChart },
  props: {
    total: {
      type: Number,
      required: true,
    },
    used: {
      type: Number,
      required: true,
    },
  },
  setup(props) {
    const state = reactive({});

    const ramValue = computed(() => {
      return [
        {
          value: props.used,
          name: "UsedRAM",
        },
      ];
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
          data: ramValue,
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
