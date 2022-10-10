enum ColorId {
  CpuTempLine = "cpu-temp-line",
  RamUsageLine = "ram-usage-line",
  MainBackground = "main-background",
  MainBackgroundDarker = "main-background-darker",
  MainBackgroundLighter = "main-background-lighter",
  Toolbar = "toolbar",
}

export default ColorId;
export const ColorIds: Array<ColorId> = Object.values(ColorId);
