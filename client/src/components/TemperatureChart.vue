<template>
  <q-card flat bordered>
    <q-card-section>
      <VChart :option="options" style="height: 30em; width: 30em" />
    </q-card-section>
  </q-card>
</template>

<script>
import { reactive, toRefs, computed, ref } from "vue";
import "echarts";
import VChart from "vue-echarts";
import { use } from "echarts";

import {
  TitleComponent,
  TooltipComponent,
  GridComponent,
} from "echarts/components";
import { LineChart } from "echarts/charts";
import { UniversalTransition } from "echarts/features";
import { CanvasRenderer } from "echarts/renderers";

import { useEventBus } from "src/composables/eventBusComposable";

use([
  TitleComponent,
  TooltipComponent,
  GridComponent,
  LineChart,
  CanvasRenderer,
  UniversalTransition,
]);

export default {
  name: "TemperatureChart",
  components: { VChart },
  props: {
    eventBusKey: {
      type: String,
      required: true,
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
        text: "Dynamic Data & Time Axis",
      },
      grid: {
        left: "8%",
        right: "4%",
        bottom: "4%",
        top: "8%",
      },
      tooltip: {
        trigger: "axis",
        formatter: function (params) {
          params = params[0];
          var date = new Date(params.name);
          return (
            date.getDate() +
            "." +
            (date.getMonth() + 1) +
            "." +
            date.getFullYear() +
            " - " +
            params.value[1] +
            "℃"
          );
        },
        axisPointer: {
          animation: false,
        },
      },
      xAxis: {
        type: "time",
        show: false,
      },
      yAxis: {
        type: "value",
        min: 0,
        max: 120,
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
          smooth: true,
          color: "rgb(230,0,38)",
        },
      ],
    });

    return { ...toRefs(state), options };
  },
};
</script>

<style lang="scss" scoped></style>
