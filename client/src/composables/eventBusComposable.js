import { onMounted, onUnmounted } from "vue";
import { eventBus } from "src/boot/event-bus";

export function useEventBus(key, func) {
  onMounted(() => {
    eventBus.on(key, func);
  });

  onUnmounted(() => {
    eventBus.off(key, func);
  });
}
