<template>
  <div>
    <VChart :option="options" style="height: 50em; width: 50em" />
  </div>
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

    function randomData() {
      now = new Date(+now + oneDay);
      value = value + Math.random() * 21 - 10;
      return {
        name: now.toString(),
        value: [
          [now.getFullYear(), now.getMonth() + 1, now.getDate()].join("/"),
          Math.round(value),
        ],
      };
    }

    let now = new Date(1997, 9, 3);
    let oneDay = 24 * 3600 * 1000;
    let value = Math.random() * 1000;
    for (var i = 0; i < 1000; i++) {
      myData.value.push(randomData());
    }

    const options = ref({
      title: {
        text: "Dynamic Data & Time Axis",
      },
      tooltip: {
        trigger: "axis",
        formatter: function (params) {
          params = params[0];
          var date = new Date(params.name);
          return (
            date.getDate() +
            "/" +
            (date.getMonth() + 1) +
            "/" +
            date.getFullYear() +
            " : " +
            params.value[1]
          );
        },
        axisPointer: {
          animation: false,
        },
      },
      xAxis: {
        type: "time",
      },
      yAxis: {
        type: "value",
        boundaryGap: [0, "100%"],
        splitLine: {
          show: false,
        },
      },
      series: [
        {
          name: "Fake Data",
          type: "line",
          showSymbol: false,
          data: myData,
        },
      ],
    });

    setInterval(function () {
      myData.value.shift();
      for (var i = 0; i < 5; i++) {
        myData.value.shift();
        myData.value.push(randomData());
      }
      // myChart.setOption({
      //   series: [
      //     {
      //       myData: myData,
      //     },
      //   ],
      // });
    }, 1000);

    return { ...toRefs(state), options };
  },
};
</script>

<style lang="scss" scoped></style>
