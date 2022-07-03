export function processTemperatures(context, temperatures) {
  const currentMin = context.state.temperatures.min;
  const currentMax = context.state.temperatures.max;

  const min = Math.min(...temperatures);
  const max = Math.max(...temperatures);

  if (!currentMin || min < currentMin) {
    context.commit("setMinTemperature", min);
  }

  if (!currentMax || max > currentMax) {
    context.commit("setMaxTemperature", max);
  }
}
