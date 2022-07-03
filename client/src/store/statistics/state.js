export default function () {
  initialState();
}

const initialState = () => {
  return {
    temperatures: {
      min: 0,
      max: 0,
    },
  };
};
