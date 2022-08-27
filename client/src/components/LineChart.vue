<template>
  <VChart :option="options" />
</template>

<script>
import { reactive, toRefs, ref } from "vue";
import "echarts";
import VChart from "vue-echarts";
import { use } from "echarts";

import { TitleComponent, GridComponent } from "echarts/components";
import { LineChart } from "echarts/charts";
import { UniversalTransition } from "echarts/features";
import { CanvasRenderer } from "echarts/renderers";

import { useEventBus } from "src/composables/eventBusComposable";

use([
  TitleComponent,
  GridComponent,
  LineChart,
  CanvasRenderer,
  UniversalTransition,
]);

export default {
  name: "LineChart",
  components: { VChart },
  props: {
    eventBusKey: {
      type: String,
      required: true,
    },
    title: {
      type: String,
      required: true,
    },
    max: {
      type: Number,
      required: false,
      default: 100,
    },
    min: {
      type: Number,
      required: false,
      default: 0,
    },
    size: {
      type: String,
      required: false,
      default: "md",
    },
    lineColor: {
      type: String,
      required: false,
      default: "blue",
    },
  },
  setup(props) {
    const state = reactive({});
    const myData = ref([]);

    useEventBus(props.eventBusKey, addValue);

    // {value, date}
    function addValue(item) {
      if (myData.value.length > 60) {
        myData.value.shift();
      }

      myData.value.push({
        name: item.date.toString(),
        value: [item.date, item.value],
      });
    }

    const options = ref({
      title: {
        text: props.title,
      },
      grid: {
        left: "8%",
        right: "4%",
        bottom: "4%",
        top: "8%",
      },
      xAxis: {
        type: "time",
        show: false,
      },
      yAxis: {
        type: "value",
        min: props.min,
        max: props.max,
        splitLine: {
          show: true,
        },
      },
      series: [
        {
          name: "Fake Data",
          type: "line",
          showSymbol: false,
          data: myData,
          areaStyle: {},
          smooth: false,
          color: props.lineColor,
        },
      ],
    });

    return { ...toRefs(state), options };
  },
};
</script>
<style lang="scss" scoped>
.md {
  height: 10em;
  width: 10em;
}
</style>
