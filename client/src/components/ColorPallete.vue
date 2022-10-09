<template>
  <Segment class="bg-blue-1">
    <div>
      <q-btn
        :dense="$q.screen.xs"
        no-caps
        label="RAM"
        icon-right="colorize"
        color="ram-usage-line"
      >
        <q-popup-proxy transition-show="scale" transition-hide="scale">
          <q-color v-model="ram" dark />
        </q-popup-proxy>
      </q-btn>
      <q-btn
        :dense="$q.screen.xs"
        no-caps
        label="CPU temp"
        icon-right="colorize"
        color="cpu-temp-line"
      >
        <q-popup-proxy transition-show="scale" transition-hide="scale">
          <q-color v-model="cputemp" dark />
        </q-popup-proxy>
      </q-btn>
      <q-btn color="primary" icon="save" label="save" @click="save" />
    </div>
  </Segment>
</template>

<script lang="ts">
import { reactive, toRefs, watch, onMounted } from "vue";
import { useQuasar } from "quasar";
import { setCssVar, getCssVar } from "quasar";
import { setCssColor } from "src/features/themes/themeCssTransfer";
import { THEME } from "src/consts/localStorageKeys";

import Segment from "src/components/Segment.vue";

import Theme from "src/models/Colors/Theme";
import ColorId from "src/models/Colors/ColorId";

interface State {
  ram: string;
  cputemp: string;
  theme: Theme;
}

export default {
  name: "ColorPallete",
  components: { Segment },
  setup() {
    const $q = useQuasar();
    const model: State = {
      ram: "#000000",
      cputemp: "#000000",
      theme: {
        name: "",
        colors: [
          { id: ColorId.RamUsageLine, value: "" },
          { id: ColorId.CpuTempLine, value: "" },
        ],
      },
    };

    const state = reactive(model);

    onMounted(() => {
      const theme: Theme | null = $q.localStorage.getItem(THEME);
      if (theme) {
        const r = theme.colors.find((x) => x.id === ColorId.RamUsageLine);
        if (r && r.value) {
          setCssVar(ColorId.RamUsageLine, r.value);
        }

        const c = theme.colors.find((x) => x.id === ColorId.CpuTempLine);
        if (c && c.value) {
          setCssVar(ColorId.CpuTempLine, c.value);
        }
      }

      state.ram = getCssVar(ColorId.RamUsageLine) ?? "#000000";
      state.cputemp = getCssVar(ColorId.CpuTempLine) ?? "#000000";

      const ramColor = state.theme.colors.find(
        (x) => x.id === ColorId.RamUsageLine
      );

      if (ramColor) {
        ramColor.value = state.ram;
      }

      const cpuTempColor = state.theme.colors.find(
        (x) => x.id === ColorId.CpuTempLine
      );

      if (cpuTempColor) {
        cpuTempColor.value = state.cputemp;
      }
    });

    watch(
      () => state.ram,
      (newValue: any) => {
        setCssColor({ id: ColorId.RamUsageLine, value: newValue });
      }
    );

    watch(
      () => state.cputemp,
      (newValue: any) => {
        setCssColor({ id: ColorId.CpuTempLine, value: newValue });
      }
    );

    const save = () => {
      const theme: Theme = {
        name: "testowy",
        colors: [
          { id: ColorId.RamUsageLine, value: state.ram },
          { id: ColorId.CpuTempLine, value: state.cputemp },
        ],
      };

      $q.localStorage.set(THEME, theme);
    };

    return { ...toRefs(state), save };
  },
};
</script>
