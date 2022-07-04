<template>
  <q-card flat bordered class="card flex column justify-center">
    <q-card-section>
      <div class="text-center text-h4">{{ displayValue }}</div>
    </q-card-section>
    <q-card-section v-if="label">
      <div class="text-center text-weight-bold">{{ label }}</div>
    </q-card-section>
  </q-card>
</template>

<script>
import { computed } from "vue";

export default {
  name: "SimpleDigitalDisplay",
  props: {
    value: {
      type: Number,
      required: false,
      default: null,
    },
    unit: {
      type: String,
      required: false,
      default: null,
    },
    label: {
      type: String,
      required: false,
      default: null,
    },
    round: {
      type: Boolean,
      required: false,
      default: true,
    },
  },
  setup(props, { emit }) {
    const round = (number) => {
      if (!props.round) {
        return number;
      }

      return Math.round(number);
    };

    const displayValue = computed(() => {
      if (!props.value) {
        return "0";
      }

      if (!props.unit) {
        return round(props.value).toString();
      }

      return round(props.value).toString() + " " + props.unit;
    });

    return { displayValue };
  },
};
</script>

<style lang="scss" scoped>
.card {
  width: 14em;
  height: 14em;
}
</style>
