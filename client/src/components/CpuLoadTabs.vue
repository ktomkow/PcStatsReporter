<template>
  <Segment :is-loading="isLoading">
    <div class="griid bg-orange-1 fit q-pa-md">
      <LineChart
        v-for="i in [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12]"
        :key="i"
        :event-bus-key="key"
        :title="'    #' + i"
        y-axis-label-hidden
        line-color="green"
      />
    </div>
  </Segment>
</template>

<script>
import { reactive, toRefs, computed, ref, onMounted, onUnmounted } from "vue";
import { useStore } from "vuex";
import { useQuasar } from "quasar";
import { useRouter } from "vue-router";

import Segment from "src/components/Segment.vue";
import LineChart from "src/components/LineChart";

import { eventBus } from "src/boot/eventBus";

export default {
  name: "CpuLoadTabs",
  components: { Segment, LineChart },
  setup(props) {
    const state = reactive({});
    const store = useStore();
    const router = useRouter();
    const q = useQuasar();

    const isLoading = computed(() => true);

    const key = "key";

    let id = null;

    onMounted(() => {
      id = setInterval(() => {
        eventBus.emit(key, {
          value: Math.random() * 20 + 50,
          date: new Date(),
        });
      }, 1000);
    });

    onUnmounted(() => {
      clearInterval(id);
    });

    return { ...toRefs(state), key, isLoading };
  },
};
</script>

<style lang="scss" scoped>
.griid {
  display: grid;
  grid-template-columns: 1fr 1fr;
  grid-auto-rows: 1fr;
  grid-gap: 1em;
  grid-auto-flow: row;
}
</style>
