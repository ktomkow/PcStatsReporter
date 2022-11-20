import ColorId from "src/models/Colors/ColorId";

export default interface TonerConfig {
  source: ColorId;
  target: ColorId;
  darken: Boolean;
}
