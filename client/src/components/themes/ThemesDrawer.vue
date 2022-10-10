<template>
  <div class="fit q-pa-sm q-gutter-sm">
    <ColorPickButton :color-id="ColorId.RamUsageLine" label="Ram" />
    <ColorPickButton :color-id="ColorId.CpuTempLine" label="Cpu" />
    <ColorPickButton :color-id="ColorId.MainBackground" label="Background" />
    <ColorPickButton :color-id="ColorId.Toolbar" label="Toolbar" />
  </div>
</template>

<script lang="ts">
import { reactive, toRefs, computed } from "vue";
import { useStore } from "vuex";
import { useQuasar } from "quasar";
import { useRouter } from "vue-router";

import ColorPickButton from "src/components/themes/ColorPickButton.vue";
import ColorId from "src/models/Colors/ColorId";

export default {
  name: "ThemesDrawer",
  components: { ColorPickButton },
  setup() {
    const state = reactive({});
    const store = useStore();
    const router = useRouter();
    const q = useQuasar();
    return { ...toRefs(state), ColorId };
  },
};
</script>

<style lang="scss" scoped></style>
