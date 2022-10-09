export default interface Color {
  id: string;
  value: string;
}

function create(value: string): Color {
  return { value: "aaa", id: "default" };
}
