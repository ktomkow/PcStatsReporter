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
import { GridComponent } from "echarts/components";
import { LineChart } from "echarts/charts";
import { UniversalTransition } from "echarts/features";
import { CanvasRenderer } from "echarts/renderers";

use([GridComponent, LineChart, CanvasRenderer, UniversalTransition]);

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
    const now = new Date();

    myData.value.push({
      name: now.toString(),
      value: [
        [now.getFullYear(), now.getMonth() + 1, now.getDate()].join("/"),
        Math.round(1),
      ],
    });

    setTimeout(() => {
      myData.value.push({
        name: now.toString(),
        value: [
          [now.getFullYear(), now.getMonth() + 1, now.getDate()].join("/"),
          Math.round(2),
        ],
      });
    }, 1000);

    // function randomData() {
    //   now = new Date(+now + oneDay);
    //   value = value + Math.random() * 21 - 10;
    //   return {
    //     name: now.toString(),
    //     value: [
    //       [now.getFullYear(), now.getMonth() + 1, now.getDate()].join("/"),
    //       Math.round(value),
    //     ],
    //   };
    // }

    // let data = [];
    // let now = new Date(1997, 9, 3);
    // let oneDay = 24 * 3600 * 1000;
    // let value = Math.random() * 1000;
    // for (var i = 0; i < 1000; i++) {
    //   data.push(randomData());
    // }

    const options = ref({
      xAxis: {
        type: "category",
        data: ["Mon", "Tue", "Wed", "Thu", "Fri", "Sat", "Sun"],
      },
      yAxis: {
        type: "value",
      },
      series: [
        {
          data: [150, 230, 224, 218, 135, 147, 260],
          type: "line",
        },
      ],
    });

    return { ...toRefs(state), options };
  },
};
</script>

<style lang="scss" scoped></style>
