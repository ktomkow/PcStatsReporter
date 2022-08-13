export function store(context, { cpuName, gpuName, totalRam }) {
  context.commit("setCpuName", cpuName);
  context.commit("setGpuName", gpuName);
  context.commit("setTotalRam", totalRam);
}
