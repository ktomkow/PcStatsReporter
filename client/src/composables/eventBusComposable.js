import { onMounted, onUnmounted } from "vue";
import { eventBus } from "src/boot/eventBus";

export function useEventBus(key, func) {
  onMounted(() => {
    eventBus.on(key, func);
  });

  onUnmounted(() => {
    eventBus.off(key, func);
  });
}
