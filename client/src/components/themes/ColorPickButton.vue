<template>
  <div>
    <q-btn :color="colorId" icon="format_paint" :label="label">
      <q-popup-proxy transition-show="scale" transition-hide="scale">
        <q-color v-model="color" dark />
      </q-popup-proxy>
    </q-btn>
  </div>
</template>

<script lang="ts">
import { reactive, toRefs, watch } from "vue";
import { useStore } from "vuex";
import { useQuasar } from "quasar";
import { useRouter } from "vue-router";
import { setCssColor } from "src/features/themes/themeCssTransfer";

export default {
  name: "ColorPickButton",
  components: {},
  props: {
    label: {
      type: String,
      required: true,
    },
    colorId: {
      required: true,
    },
  },
  setup(props: any) {
    const state = reactive({ color: "" });
    const store = useStore();
    const router = useRouter();
    const q = useQuasar();

    watch(
      () => state.color,
      (newValue: any) => {
        setCssColor({ id: props.colorId, value: newValue });
      }
    );

    return { ...toRefs(state) };
  },
};
</script>

<style lang="scss" scoped></style>
