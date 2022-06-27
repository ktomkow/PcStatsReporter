<template>
  <q-page class="flex flex-center column">
    <img
      alt="Quasar logo"
      src="~assets/quasar-logo-vertical.svg"
      style="width: 200px; height: 200px"
    />
    <q-btn color="primary" label="shot" @click="onClick" class="q-ma-xl" />
  </q-page>
</template>

<script>
import net from "net";
import { defineComponent, onMounted } from "vue";

export default defineComponent({
  name: "PageIndex",
  setup() {
    onMounted(() => {
      console.warn("Yeah ho mounted");
    });

    const onClick = () => {
      var socket = new net.Socket();
      socket.connect(9090, "localhost", () => {
        console.log("Yeah");
      });
    };

    return { onClick };
  },
});
</script>
