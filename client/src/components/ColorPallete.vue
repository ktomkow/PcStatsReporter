<template>
  <Segment class="bg-blue-1">
    <div>
      <q-btn
        :dense="$q.screen.xs"
        no-caps
        label="RAM"
        icon-right="colorize"
        color="ram"
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
        color="cputemp"
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
import { reactive, toRefs, computed, watch, onMounted } from "vue";
import { useStore } from "vuex";
import { useQuasar } from "quasar";
import { useRouter } from "vue-router";
import { setCssVar, getCssVar, colors } from "quasar";

import { THEME } from "src/consts/localStorageKeys";

import Segment from "src/components/Segment.vue";

import Theme from "src/models/Colors/Theme";
import Color from "src/models/Colors/Color";

interface State {
  ram: string;
  cputemp: string;
  theme: Theme;
}

export default {
  name: "ColorPallete",
  components: { Segment },
  setup(props: any) {
    const $q = useQuasar();
    const { getPaletteColor } = colors;
    const model: State = {
      ram: "#000000",
      cputemp: "#000000",
      theme: {
        name: "",
        colors: [
          { id: "ram", value: "" },
          { id: "cputemp", value: "" },
        ],
      },
    };

    const state = reactive(model);

    onMounted(() => {
      const theme: Theme | null = $q.localStorage.getItem(THEME);
      if (theme) {
        const r = theme.colors.find((x) => x.id === "ram");
        if (r && r.value) {
          setCssVar("ram", r.value);
        }

        const c = theme.colors.find((x) => x.id === "cputemp");
        if (c && c.value) {
          setCssVar("cputemp", c.value);
        }
      }

      console.log(
        "🚀 ~ file: ColorPallete.vue ~ line 73 ~ onMounted ~ theme",
        theme
      );
      state.ram = getCssVar("ram") ?? "#000000";
      state.cputemp = getCssVar("cputemp") ?? "#000000";

      const ramColor = state.theme.colors.find((x) => x.id === "ram");
      if (ramColor) {
        ramColor.value = state.ram;
      }

      const cpuTempColor = state.theme.colors.find((x) => x.id === "cputemp");
      if (cpuTempColor) {
        cpuTempColor.value = state.cputemp;
      }
    });

    watch(
      () => state.ram,
      (nv: any, old: any) => {
        setCssVar("ram", nv);
      }
    );

    watch(
      () => state.cputemp,
      (nv: any, old: any) => {
        setCssVar("cputemp", nv);
      }
    );

    const store = useStore();
    const router = useRouter();

    const save = () => {
      const theme: Theme = {
        name: "testowy",
        colors: [
          { id: "ram", value: state.ram },
          { id: "cputemp", value: state.cputemp },
        ],
      };

      $q.localStorage.set(THEME, theme);
    };

    return { ...toRefs(state), save };
  },
};
</script>
