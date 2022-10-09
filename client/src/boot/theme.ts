import { boot } from "quasar/wrappers";
import { LocalStorage, setCssVar, getCssVar } from "quasar";
import { THEME } from "src/consts/localStorageKeys";
import ColorId, { ColorIds } from "src/models/Colors/ColorId";

import Theme, { createEmptyTheme } from "src/models/Colors/Theme";

// "async" is optional;
// more info on params: https://v2.quasar.dev/quasar-cli/boot-files
export default boot(async (/* { app, router, ... } */) => {
  const theme: Theme | null = LocalStorage.getItem(THEME);
  if (theme) {
    setDefaultThemeToCss(theme);
  } else {
    const theme = loadDefaultThemeFromCss();
    LocalStorage.set(THEME, theme);
  }
});

function setDefaultThemeToCss(theme: Theme): void {
  for (const color of theme.colors) {
    setCssVar(color.id, color.value);
  }
}

function loadDefaultThemeFromCss(): Theme | null {
  const theme: Theme = createEmptyTheme();
  theme.name = "default";

  for (const css of ColorIds) {
    const colorId: ColorId = css;
    const value = getCssVar(css) ?? "#000000";

    theme.colors.push({
      id: colorId,
      value: value,
    });
  }

  return theme;
}
