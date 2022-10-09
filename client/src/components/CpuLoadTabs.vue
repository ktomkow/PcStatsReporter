<template>
  <Segment :is-loading="isLoading">
    <div v-if="!isLoading" class="griid bg-orange-1 fit q-pa-md">
      <LineChart
        v-for="i in threads"
        :key="i"
        :event-bus-key="i.busKey"
        :title="'#' + i.id"
        y-axis-label-hidden
        line-color="green"
      />
    </div>
  </Segment>
</template>

<script>
import { reactive, toRefs, computed } from "vue";

import Segment from "src/components/Segment.vue";
import LineChart from "src/components/LineChart";

import { useEventBus } from "src/composables/eventBusComposable";
import eventBusKeys from "src/consts/eventBusKeys";
import { eventBus } from "src/boot/eventBus";

export default {
  name: "CpuLoadTabs",
  components: { Segment, LineChart },
  setup() {
    const state = reactive({
      //{ id, busKey}
      threads: [],
    });

    const isLoading = computed(() => state.threads.length < 1);

    useEventBus(eventBusKeys.CPU_SAMPLE_ARRIVED, cpuSampleArrived);

    function cpuSampleArrived(data) {
      if (state.threads.length < 1) {
        insertThreads(data.cores);
      }

      for (const core of data.cores) {
        for (const thread of core.threadsLoad) {
          const busKey = buildBusKey(core, thread);
          eventBus.emit(busKey, {
            value: thread.load,
            date: data.registeredAt,
          });
        }
      }
    }

    function insertThreads(cores) {
      for (const core of cores) {
        for (const thread of core.threadsLoad) {
          state.threads.push({
            id: buildThreadId(core, thread),
            busKey: buildBusKey(core, thread),
          });
        }
      }
    }

    function buildThreadId(core, thread) {
      return core.coreNumber + "/" + thread.number;
    }

    function buildBusKey(core, thread) {
      return "CPU_LOAD" + buildThreadId(core, thread);
    }

    return { ...toRefs(state), isLoading };
  },
};
</script>

<style lang="scss" scoped>
.griid {
  display: grid;
  grid-template-columns: 1fr 1fr 1fr;
  grid-auto-rows: 1fr;
  grid-gap: 1em;
  grid-auto-flow: row;
}
</style>
