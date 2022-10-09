<template>
  <div class="fit bg-blue-2"></div>
</template>

<script>
import { reactive, toRefs, computed } from "vue";
import { useStore } from "vuex";
import { useQuasar } from "quasar";
import { useRouter } from "vue-router";

export default {
  name: "ThemesDrawer",
  components: {},
  setup(props) {
    const state = reactive({});
    const store = useStore();
    const router = useRouter();
    const q = useQuasar();
    return { ...toRefs(state) };
  },
};
</script>

<style lang="scss" scoped></style>
