<template>
  <q-card flat bordered class="q-pa-lg">
    <q-card-section>
      <div class="text-center">{{ displayValue }}</div>
    </q-card-section>
    <q-card-section v-if="label">
      <div class="text-center">{{ label }}</div>
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
  },
  setup(props, { emit }) {
    const displayValue = computed(() => {
      if (!props.value) {
        return "0";
      }

      if (!props.unit) {
        return props.value.toString();
      }

      return props.value.toString() + " " + props.unit;
    });

    return { displayValue };
  },
};
</script>

<style lang="scss" scoped></style>
