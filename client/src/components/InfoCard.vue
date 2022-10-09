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

import Segment from "src/components/Segment";

export default {
  name: "InfoCard",
  components: { Segment },
  setup() {
    const store = useStore();
    const state = reactive({});

    const isLoading = computed(
      () =>
        !(
          store.state.pcInfo.cpuName &&
          store.state.pcInfo.gpuName &&
          store.state.pcInfo.totalRam
        )
    );

    const cpu = computed(() => store.state.pcInfo.cpuName);
    const gpu = computed(() => store.state.pcInfo.gpuName);
    const totalRam = computed(() => store.state.pcInfo.totalRam);

    return { ...toRefs(state), cpu, gpu, totalRam, isLoading };
  },
};
</script>

<style lang="scss" scoped>
.bordered {
  border-bottom: 1px solid $border;
}
</style>
