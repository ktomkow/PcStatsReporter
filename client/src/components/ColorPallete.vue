<template>
  <Segment class="bg-blue-1">
    <div>
      <q-btn color="ram" icon="check" label="OK" />
      <q-btn
        :dense="$q.screen.xs"
        no-caps
        label="Face"
        icon-right="colorize"
        color="ram"
      >
        <q-popup-proxy transition-show="scale" transition-hide="scale">
          <q-color v-model="ram" dark />
        </q-popup-proxy>
      </q-btn>
    </div>
  </Segment>
</template>

<script>
import { reactive, toRefs, computed, watch } from "vue";
import { useStore } from "vuex";
import { useQuasar } from "quasar";
import { useRouter } from "vue-router";
import { setCssVar, getCssVar, colors } from "quasar";
import Segment from "src/components/Segment.vue";

export default {
  name: "ColorPallete",
  components: { Segment },
  setup(props) {
    const { getPaletteColor } = colors;

    const ram = getCssVar("ram");
    console.log("🚀 ~ file: ColorPallete.vue ~ line 35 ~ setup ~ ram", ram);
    const ramPaleta = getPaletteColor("ram");
    console.log(
      "🚀 ~ file: ColorPallete.vue ~ line 37 ~ setup ~ ramPaleta",
      ramPaleta
    );
    const primaryPaleta = getPaletteColor("primary");
    console.log(
      "🚀 ~ file: ColorPallete.vue ~ line 39 ~ setup ~ primaryPaleta",
      primaryPaleta
    );

    const state = reactive({
      ram: getCssVar("ram"),
    });

    console.log("🚀 ~ file: ColorPallete.vue ~ line 34 ~ setup ~ state", state);

    watch(
      () => state.ram,
      (nv, old) => {
        setCssVar("ram", nv);
        console.log("🚀 ~ file: ColorPallete.vue ~ line 47 ~ setup ~ nv", nv);
      }
    );

    const store = useStore();
    const router = useRouter();
    const q = useQuasar();
    return { ...toRefs(state) };
  },
};
</script>
