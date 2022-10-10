import { boot } from "quasar/wrappers";
import { colors } from "quasar";

import { setCssColor } from "src/features/themes/themeCssTransfer";

import TonerConfig from "src/models/Colors/TonerConfig";
import ColorId from "src/models/Colors/ColorId";
import Color from "src/models/Colors/Color";

import eventBusKeys from "src/consts/eventBusKeys";
import { eventBus } from "src/boot/eventBus";
// "async" is optional;
// more info on params: https://v2.quasar.dev/quasar-cli/boot-files
export default boot(async (/* { app, router, ... } */) => {
  const tonerConfig: TonerConfig[] = [];
  tonerConfig.push(
    {
      source: ColorId.MainBackground,
      target: ColorId.MainBackgroundLighter,
      darken: false,
    },
    {
      source: ColorId.MainBackground,
      target: ColorId.MainBackgroundDarker,
      darken: true,
    }
  );
  eventBus.on(eventBusKeys.COLOR_UPDATED, (color) => handleColorChange(color));

  function handleColorChange(input: any) {
    const color: Color = input as Color;

    if (!color) {
      return;
    }

    const config = tonerConfig.filter((x) => x.source === color.id);
    if (!config) {
      return;
    }

    for (const item of config) {
      const newValue = colors.lighten(color.value, item.darken ? -20 : 10);
      setCssColor({ id: item.target, value: newValue });
    }
  }
});
