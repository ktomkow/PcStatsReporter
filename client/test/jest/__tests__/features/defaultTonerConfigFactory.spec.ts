import { describe, expect, it } from "@jest/globals";

import createDefaultTonerConfigCollection from "src/features/themes/defaultTonerConfigFactory";
import ColorId from "src/models/Colors/ColorId";
import TonerConfig from "src/models/Colors/TonerConfig";

describe("defaultTonerConfigFactory", () => {
  it("createDefaultTonerConfigCollection", () => {
    const result: TonerConfig[] = createDefaultTonerConfigCollection();
    expect(1).toBe(1);

    expect(result).toContainEqual({
      source: ColorId.MainBackground,
      target: ColorId.MainBackgroundLighter,
      darken: false,
    });

    expect(result).toContainEqual({
      source: ColorId.MainBackground,
      target: ColorId.MainBackgroundDarker,
      darken: true,
    });
  });
});
