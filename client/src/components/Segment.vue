<template>
  <q-card flat bordered :class="componentClass" class="layout">
    <q-card-section class="q-pa-none q-ma-none">
      <div class="q-px-sm text-h6">{{ title }}</div>
    </q-card-section>
    <q-card-section class="q-pa-none q-ma-none" style="flex: 1 0 0">
      <slot></slot>
    </q-card-section>
    <InnerLoad :show="isLoading" />
  </q-card>
</template>

<script>
import { computed } from "vue";
import InnerLoad from "src/components/InnerLoad";

export default {
  name: "Segment",
  components: { InnerLoad },
  props: {
    size: {
      type: String,
      required: false,
      default: "md",
    },
    isLoading: {
      type: Boolean,
      required: false,
      default: false,
    },
    title: {
      type: String,
      required: false,
      default: null,
    },
  },
  setup(props) {
    const sizeClass = computed(() => {
      switch (props.size) {
        case "md":
          return { "segment-md": true };
        case "sm":
          return { "segment-sm": true };
        case "xs":
          return { "segment-xs": true };
        case "xxs":
          return { "segment-xxs": true };
        default:
          return { "segment-md": true };
      }
    });

    const componentClass = computed(() => {
      return sizeClass.value;
    });

    return { componentClass };
  },
};
</script>

<style lang="css">
.layout {
  display: grid;
  grid-template-columns: 1fr;
  grid-template-rows: min-content 1fr;
}
.segment-md {
  width: 30em;
  height: 30em;
}

.segment-sm {
  width: 20em;
  height: 20em;
}

.segment-xs {
  width: 15em;
  height: 15em;
}

.segment-xxs {
  width: 10em;
  height: 10em;
}
</style>
