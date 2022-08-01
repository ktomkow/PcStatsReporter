<template>
  <q-inner-loading :showing="show">
    <q-spinner-gears size="6em" color="primary" />
  </q-inner-loading>
</template>

<script>
export default {
  name: "InnerLoad",
  props: {
    show: {
      type: Boolean,
      required: true,
    },
  },
};
</script>
