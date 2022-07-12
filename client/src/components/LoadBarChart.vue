<template>
  <q-card flat bordered>
    <q-card-section>
      <VChart :option="options" style="height: 30em; width: 30em" />
      <q-btn color="primary" icon="check" label="rand" @click="onClick" />
      <q-btn color="primary" icon="check" label="add" @click="onClick2" />
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
    let i = 2;
    const dataX = ref(["1", "2"]);
    const dataY = ref([10, 5]);

    const onClick = () => {
      dataY.value.splice(0);
      for (let j = 0; j < i; j++) {
        dataY.value.push(Math.random() * 50 + 50);
      }
    };

    const onClick2 = () => {
      dataX.value.push((++i).toString());
      dataY.value.push(Math.random() * 50 + 50);
    };

    const options = ref({
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

    return { ...toRefs(state), options, onClick, onClick2 };
  },
};
</script>

<style lang="scss" scoped></style>
