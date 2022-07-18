<template>
  <q-card flat bordered>
    <q-card-section>
      <LineChart :event-bus-key="eventBusKey" title="Temperature" color="red" />
    </q-card-section>
  </q-card>
</template>

<script>
import LineChart from "src/components/LineChart";

export default {
  name: "TemperatureChart",
  components: { LineChart },
  props: {
    eventBusKey: {
      type: String,
      required: true,
    },
  },
  setup() {
    return {};
  },
};
</script>

<style lang="scss" scoped></style>
