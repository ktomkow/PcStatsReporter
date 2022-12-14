export default function () {
  return initialState();
}

export interface State {
  cpuName?: string;
  gpuName?: string;
  totalRam?: number;
}

const initialState = (): State => {
  return {};
};
