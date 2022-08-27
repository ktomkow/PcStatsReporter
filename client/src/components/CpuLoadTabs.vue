<template>
  <Segment>
    <div class="griid bg-orange-1 fit q-pa-md">
      <LineChart :event-bus-key="key" title="  #1" line-color="green" />
      <LineChart :event-bus-key="key" title="  #2" line-color="green" />
      <LineChart :event-bus-key="key" title="  #3" line-color="green" />
      <LineChart :event-bus-key="key" title="  #4" line-color="green" />
      <LineChart :event-bus-key="key" title="  #5" line-color="green" />
      <LineChart :event-bus-key="key" title="  #6" line-color="green" />

      <!-- <div class="q-pa-xl bg-red-2">a</div>
      <div class="q-pa-xl bg-green-2">b</div>
      <div class="q-pa-xl bg-blue-2">c</div> -->
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

    return { ...toRefs(state), key };
  },
};
</script>

<style lang="scss" scoped>
.griid {
  display: grid;
  grid-template-columns: 50% 50%;
  grid-template-rows: 33% 33% 33%;
  grid-gap: 1em;
  grid-auto-flow: row;
}
</style>
