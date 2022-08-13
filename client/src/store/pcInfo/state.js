export default function () {
  return initialState();
}

const initialState = () => {
  return {
    cpuName: null,
    gpuName: null,
    totalRam: null,
  };
};
