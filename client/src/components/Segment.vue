<template>
  <q-card flat bordered :class="componentClass">
    <q-card-section class="fit q-pa-none q-ma-none">
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
