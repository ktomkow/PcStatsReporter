export default function () {
  initialState();
}

const initialState = () => {
  return {
    temperatures: {
      min: null,
      max: null,
    },
  };
};
