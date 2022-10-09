import Color from "src/models/Colors/Color";

export default interface Theme {
  name: string;
  colors: Array<Color>;
}

export function createEmptyTheme() {
  const theme: Theme = {
    name: "",
    colors: [],
  };

  return theme;
}
