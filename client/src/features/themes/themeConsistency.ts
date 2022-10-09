// todo: tests and implementation
import { LocalStorage, setCssVar, getCssVar } from "quasar";

import Theme, { createEmptyTheme } from "src/models/Colors/Theme";
import ColorId, { ColorIds } from "src/models/Colors/ColorId";

// removes colors that do not match colors id enum
export function removeColorsOutOfHandling(theme: Theme): Theme | null {
  return null;
}

// takes color from current css variable and add it to theme colors collections
export function addMissingColors(theme: Theme): Theme | null {
  return null;
}
