<template>
  <q-card flat bordered class="segment">
    <q-card-section class="fit">
      <slot></slot>
    </q-card-section>
  </q-card>
</template>

<script>
export default {
  name: "Segment",
};
</script>

<style lang="scss" scoped>
.segment {
  width: 30em;
  height: 30em;
}
</style>
