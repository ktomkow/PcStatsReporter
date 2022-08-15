<template>
  <q-card flat bordered>
    <q-card-section>
      <VChart :option="options" style="height: 30em; width: 30em" />
    </q-card-section>
  </q-card>
</template>

<script>
import { reactive, toRefs, computed, ref } from "vue";
import { useStore } from "vuex";

import { useEventBus } from "src/composables/eventBusComposable";
import eventBusKeys from "src/consts/eventBusKeys";

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
  setup() {
    const state = reactive({ usedRam: null });
    const store = useStore();

    const totalRam = computed(() => store.state.pcInfo.totalRam ?? 0);

    // const isLoading = computed(() => { // to use later
    //   return !!store.state.pcInfo.total && !!state.usedRam;
    // });

    useEventBus(eventBusKeys.RAM_SAMPLE_ARRIVED, setRamValue);

    function setRamValue(data) {
      state.usedRam = data.inUse.toFixed(2);
    }

    const ramValue = computed(() => {
      return [
        {
          value: state.usedRam ?? 0,
          name: "RAM",
        },
      ];
    });

    const options = ref({
      title: {
        text: "RAM Usage",
      },
      series: [
        {
          min: 0,
          max: totalRam.value,
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
              if (!totalRam.value || !state.usedRam) {
                return "Loading..";
              }

              return v + " GB / " + totalRam.value + " GB";
            },
          },
        },
      ],
    });

    return { ...toRefs(state), options };
  },
};
</script>

<style lang="scss" scoped></style>
