export default function () {
  return initialState();
}

const initialState = () => {
  return {
    temperatures: {
      min: null,
      max: null,
    },
  };
};
