import { setCssVar, getCssVar } from "quasar";

import Theme, { createEmptyTheme } from "src/models/Colors/Theme";
import ColorId, { ColorIds } from "src/models/Colors/ColorId";

export function setThemeToCss(theme: Theme): void {
  for (const color of theme.colors) {
    setCssVar(color.id, color.value);
  }
}

export function loadThemeFromCss(): Theme {
  const theme: Theme = createEmptyTheme();
  theme.name = "none";

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
