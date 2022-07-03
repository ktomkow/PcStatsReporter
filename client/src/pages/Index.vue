<template>
  <q-page class="flex flex-center column">
    <img
      alt="Quasar logo"
      src="~assets/quasar-logo-vertical.svg"
      style="width: 200px; height: 200px"
    />
    <q-btn color="primary" label="shot" @click="onClick" class="q-ma-xl" />
    <SimpleDigitalDisplay :value="cpuAverageTemperature" />
  </q-page>
</template>

<script>
import { reactive, toRefs, defineComponent, onMounted, onUnmounted } from "vue";
import { api } from "src/boot/axios";
import SimpleDigitalDisplay from "src/components/SimpleDigitalDisplay";

export default defineComponent({
  name: "PageIndex",
  components: { SimpleDigitalDisplay },
  setup() {
    const state = reactive({ cpuAverageTemperature: 0 });

    onMounted(() => {
      console.warn("Yeah ho mounted");
    });

    const calculateAverage = (values) => {
      let sum = 0;

      if (!values || values.length === 0) {
        return null;
      }

      for (let number of values) {
        sum += number;
      }

      return sum / values.length;
    };

    const calculateAverageTemperature = (cpuData) => {
      const coresTemperature = cpuData.cores.map((x) => x.temperature);

      return calculateAverage(coresTemperature);
    };

    const onClick = async () => {
      try {
        const result = await api.get("api/cpu");
        const cpuData = result.data;
        console.log(cpuData);
        state.cpuAverageTemperature = calculateAverageTemperature(cpuData);
      } catch (e) {
        console.error(e);
      }
    };

    return { ...toRefs(state), onClick };
  },
});
</script>
