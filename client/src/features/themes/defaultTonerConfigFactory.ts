import TonerConfig from "src/models/Colors/TonerConfig";
import ColorId from "src/models/Colors/ColorId";

export default function createDefaultTonerConfigCollection(): TonerConfig[] {
  return [
    {
      source: ColorId.MainBackground,
      target: ColorId.MainBackgroundDarker,
      darken: true,
    },
    {
      source: ColorId.MainBackground,
      target: ColorId.MainBackgroundLighter,
      darken: false,
    },
  ];
}
