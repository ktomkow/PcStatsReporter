import { describe, expect, it } from "@jest/globals";

import { roundToString } from "src/services/mathService";

describe("roundToString null or other strange stuff", () => {
  it("When input is null then empty string", () => {
    const input = null;
    const result = roundToString(input);
    expect(result).toBe("");
  });

  it("When input is undefined then empty string", () => {
    const input = undefined;
    const result = roundToString(input);
    expect(result).toBe("");
  });

  it("When input is empty array then empty string", () => {
    const input = [];
    const result = roundToString(input);
    expect(result).toBe("");
  });

  it("When input is array then empty string", () => {
    const input = [1, 2, 3];
    const result = roundToString(input);
    expect(result).toBe("");
  });

  it("When input is number but a string then empty string", () => {
    const input = "5";
    const result = roundToString(input);
    expect(result).toBe("");
  });

  it("When input is string then empty string", () => {
    const input = "text";
    const result = roundToString(input);
    expect(result).toBe("");
  });

  it("When input is emoji then empty string", () => {
    const input = "🍕";
    const result = roundToString(input);
    expect(result).toBe("");
  });

  it("When input is NaN then empty string", () => {
    const input = NaN;
    const result = roundToString(input);
    expect(result).toBe("");
  });

  it("When input is function then empty string", () => {
    const input = () => {};
    const result = roundToString(input);
    expect(result).toBe("");
  });

  it("When input is function that return number then empty string", () => {
    const input = () => {
      return 5;
    };

    const result = roundToString(input);
    expect(result).toBe("");
  });
});

describe("roundToString to int", () => {
  const roundToIntCases = [
    [10, "10"],
    [-23, "-23"],
    [1.1, "1"],
    [1.5, "2"],
    [1.9, "2"],
  ];

  test.each(roundToIntCases)("When %p then %p", (input, expectedResult) => {
    const result = roundToString(input, 0);

    expect(result).toEqual(expectedResult);
    expect(typeof result).toEqual("string");
  });
});
