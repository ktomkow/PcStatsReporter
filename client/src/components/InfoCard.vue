<template>
  <Segment size="md" :is-loading="isLoading">
    <div class="bordered q-py-sm">
      <div class="flex row">
        <q-icon name="o_info" size="lg" class="q-mx-lg outlined" />
        <div class="text-h4">About</div>
      </div>
    </div>

    <div v-if="!isLoading" class="flex column justify-around">
      <div class="q-py-sm">
        <div class="flex row">
          <q-icon name="o_smart_toy" size="lg" class="q-mx-lg" />
          <div class="text-h6">{{ cpu }}</div>
        </div>
      </div>
      <div class="q-py-sm">
        <div class="flex row">
          <q-icon name="desktop_windows" size="lg" class="q-mx-lg" />
          <div class="text-h6">{{ gpu }}</div>
        </div>
      </div>
      <div class="q-py-sm">
        <div class="flex row">
          <q-icon name="memory" size="lg" class="q-mx-lg" />
          <div class="text-h6">{{ totalRam }} GB</div>
        </div>
      </div>
    </div>
  </Segment>
</template>

<script>
import { reactive, toRefs, computed } from "vue";
import { useStore } from "vuex";
import { useEventBus } from "src/composables/eventBusComposable";
import eventBusKeys from "src/consts/eventBusKeys";

import Segment from "src/components/Segment";

export default {
  name: "InfoCard",
  components: { Segment },
  setup(props) {
    const state = reactive({
      cpu: "null",
      gpu: "null",
      totalRam: "null",
    });

    const storePcInfo = ({ cpuName, gpuName, totalRam }) => {
      state.cpu = cpuName;
      state.gpu = gpuName;
      state.totalRam = formatTotalRam(totalRam);
    };

    function formatTotalRam(ram) {
      return ram.toFixed(2);
    }

    useEventBus(eventBusKeys.PC_INFO_ARRIVED, storePcInfo);

    const isLoading = computed(
      () => !(state.cpu && state.gpu && state.totalRam)
    );

    return { ...toRefs(state), isLoading };
  },
};
</script>

<style lang="scss" scoped>
.bordered {
  border-bottom: 1px solid $border;
}
</style>
