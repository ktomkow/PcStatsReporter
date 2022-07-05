<template>
  <q-card flat bordered class="card flex column q-py-sm q-px-sm">
    <vue-speedometer
      :needleHeightRatio="0.75"
      needleTransition="easeElastic"
      :needleTransitionDuration="5000"
      :minValue="0"
      :maxValue="100"
      :customSegmentStops="[0, 30, 50, 75, 90, 100]"
      :segmentColors="['limegreen', 'green', 'gold', 'tomato', 'firebrick']"
      currentValueText="${value}%"
      :value="value"
      class="spedometer"
    />
    <div class="text-center q-pb-xl">{{ label }}</div>
  </q-card>
</template>

<script>
import { reactive, toRefs, computed } from "vue";
import { useStore } from "vuex";
import { useQuasar } from "quasar";
import { useRouter } from "vue-router";

import VueSpeedometer from "vue-speedometer";

export default {
  name: "LoadSpeedometer",
  components: { VueSpeedometer },
  props: {
    label: {
      type: String,
      required: false,
      default: null,
    },
    value: {
      type: Number,
      required: false,
      default: 0,
    },
  },
  setup(props, { emit }) {
    const state = reactive({});
    const store = useStore();
    const router = useRouter();
    const q = useQuasar();
    return { ...toRefs(state) };
  },
};
</script>

<style lang="scss" scoped></style>
