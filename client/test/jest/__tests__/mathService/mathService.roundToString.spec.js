import { describe, expect, it } from "@jest/globals";

import { roundToString } from "src/services/mathService";

describe("roundToString input null or other strange stuff", () => {
  const cases = [
    [null],
    [undefined],
    [NaN],
    [[]],
    [[123]],
    ["5"],
    ["text"],
    [() => {}],
    [() => 5],
    ["🍕"],
  ];

  test.each(cases)("When input %p then ''", (input) => {
    const result = roundToString(input, 0);

    expect(result).toEqual("");
    expect(typeof result).toEqual("string");
  });
});

describe("roundToString digits null or other strange stuff", () => {
  const cases = [
    [-211],
    [-1],
    [1.2],
    [null],
    [undefined],
    [NaN],
    [[]],
    [[123]],
    ["5"],
    ["text"],
    [() => {}],
    [() => 5],
    ["🍕"],
  ];
  test.each(cases)("When digits %p then ''", (digits) => {
    const result = roundToString(123, digits);

    expect(result).toEqual("");
    expect(typeof result).toEqual("string");
  });
});

describe("roundToString to int", () => {
  const cases = [
    [10, "10"],
    [-23, "-23"],
    [-23.1, "-23"],
    [-23.5, "-24"],
    [-23.9, "-24"],
    [1.1, "1"],
    [1.5, "2"],
    [1.9, "2"],
  ];

  test.each(cases)(
    "When input %p and digits 0 then %p",
    (input, expectedResult) => {
      const result = roundToString(input, 0);

      expect(result).toEqual(expectedResult);
      expect(typeof result).toEqual("string");
    }
  );
});

describe("roundToString when 2 fraction parts and round to 2 fraction parts (no 0 as last digit) then round", () => {
  const cases = [
    [1.12, "1.12"],
    [0.11, "0.11"],
    [-25.39, "-25.39"],
    [1571.32, "1571.32"],
  ];

  test.each(cases)(
    "When input %p and digits 2 then %p",
    (input, expectedResult) => {
      const result = roundToString(input, 2);

      expect(result).toEqual(expectedResult);
      expect(typeof result).toEqual("string");
    }
  );
});

describe("roundToString when 0 or 1 or 2 or 3 fraction parts and round to 3 fraction parts then round and complete using '0'", () => {
  const cases = [
    [0, "0.000"],
    [0.0, "0.000"],
    [0.1, "0.100"],
    [-94.7, "-94.700"],
    [1571.33, "1571.330"],
    [14.23, "14.230"],
    [15.8, "15.800"],
    [11.333, "11.333"],
  ];

  test.each(cases)(
    "When input %p and digits 3 then %p",
    (input, expectedResult) => {
      const result = roundToString(input, 3);

      expect(result).toEqual(expectedResult);
      expect(typeof result).toEqual("string");
    }
  );
});
