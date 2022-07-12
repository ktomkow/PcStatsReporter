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
import { BarChart } from "echarts/charts";
import { UniversalTransition } from "echarts/features";
import { CanvasRenderer } from "echarts/renderers";

use([
  TitleComponent,
  TooltipComponent,
  GridComponent,
  BarChart,
  CanvasRenderer,
  UniversalTransition,
]);

export default {
  name: "LoadBarChart",
  components: { VChart },
  props: {
    values: {
      type: Array,
      required: true,
    },
  },
  setup(props) {
    const state = reactive({});

    const dataX = computed(() => {
      return props.values.map((x) => x.id);
    });

    const dataY = computed(() => {
      return props.values.map((x) => x.value);
    });

    const options = ref({
      title: {
        text: "CPU Load",
      },
      tooltip: {
        trigger: "axis",
        axisPointer: {
          type: "shadow",
        },
      },
      grid: {
        left: "3%",
        right: "4%",
        bottom: "3%",
        containLabel: true,
      },
      xAxis: [
        {
          type: "category",
          data: dataX,
          axisTick: {
            alignWithLabel: true,
          },
        },
      ],
      yAxis: [
        {
          type: "value",
          max: 100,
        },
      ],
      series: [
        {
          name: "Load",
          type: "bar",
          data: dataY,
        },
      ],
    });

    return { ...toRefs(state), options };
  },
};
</script>

<style lang="scss" scoped></style>
