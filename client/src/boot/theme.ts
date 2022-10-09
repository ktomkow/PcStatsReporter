import { boot } from "quasar/wrappers";
import { LocalStorage, setCssVar, getCssVar } from "quasar";
import { THEME } from "src/consts/localStorageKeys";
import ColorId, { ColorIds } from "src/models/Colors/ColorId";

import Theme, { createEmptyTheme } from "src/models/Colors/Theme";

import {
  loadThemeFromCss,
  setThemeToCss,
} from "src/features/themes/themeCssTransfer";

// "async" is optional;
// more info on params: https://v2.quasar.dev/quasar-cli/boot-files
export default boot(async (/* { app, router, ... } */) => {
  const theme: Theme | null = LocalStorage.getItem(THEME);
  if (theme) {
    setThemeToCss(theme);
  } else {
    const theme = loadThemeFromCss();
    theme.name = "default";

    LocalStorage.set(THEME, theme);
  }
});
