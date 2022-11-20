import type { Config } from "@jest/types";

const esModules = ["quasar", "quasar/lang", "lodash-es"].join("|");

const config: Config.InitialOptions = {
  globals: {
    __DEV__: true,
    // TODO: Remove if resolved natively
    // See https://github.com/vuejs/vue-jest/issues/175
    "vue-jest": {
      pug: { doctype: "html" },
    },
    "ts-jest": {
      tsconfig: {
        rootDir: ".",
      },
    },
  },
  // Jest assumes we are testing in node environment, specify jsdom environment instead
  testEnvironment: "jsdom",
  // noStackTrace: true,
  // bail: true,
  // cache: false,
  verbose: true,
  // watch: true,
  reporters: ["jest-teamcity"],
  collectCoverage: false,
  coverageDirectory: "<rootDir>/test/jest/coverage",
  collectCoverageFrom: [
    "<rootDir>/src/**/*.vue",
    "<rootDir>/src/**/*.js",
    "<rootDir>/src/**/*.ts",
    "<rootDir>/src/**/*.jsx",
  ],
  // Needed in JS codebases too because of feature flags
  coveragePathIgnorePatterns: ["/node_modules/", ".d.ts$"],
  coverageThreshold: {
    global: {
      //  branches: 50,
      //  functions: 50,
      //  lines: 50,
      //  statements: 50
    },
  },
  testMatch: [
    "<rootDir>/test/jest/__tests__/**/*.(spec|test).(ts|js)",
    "<rootDir>/src/**/*.jest.(spec|test).(ts|js)",
  ],
  moduleFileExtensions: ["vue", "js", "jsx", "json", "ts"],
  moduleNameMapper: {
    "^quasar$": "quasar/dist/quasar.esm.prod.js",
    "^~/(.*)$": "<rootDir>/$1",
    "^src/(.*)$": "<rootDir>/src/$1",
    "^app/(.*)$": "<rootDir>/$1",
    "^components/(.*)$": "<rootDir>/src/components/$1",
    "^layouts/(.*)$": "<rootDir>/src/layouts/$1",
    "^pages/(.*)$": "<rootDir>/src/pages/$1",
    "^assets/(.*)$": "<rootDir>/src/assets/$1",
    "^boot/(.*)$": "<rootDir>/src/boot/$1",
    ".*css$": "@quasar/quasar-app-extension-testing-unit-jest/stub.css",
  },
  transform: {
    ".*\\.vue$": "vue-jest",
    ".*\\.js$": "babel-jest",
    ".+\\.(css|styl|less|sass|scss|svg|png|jpg|ttf|woff|woff2)$":
      "jest-transform-stub",
    "^.+\\.tsx?$": "ts-jest",
    // use these if NPM is being flaky, care as hosting could interfere with these
    // '.*\\.vue$': '@quasar/quasar-app-extension-testing-unit-jest/node_modules/vue-jest',
    // '.*\\.js$': '@quasar/quasar-app-extension-testing-unit-jest/node_modules/babel-jest'
  },
  transformIgnorePatterns: [`node_modules/(?!(${esModules}))`],
  snapshotSerializers: ["jest-serializer-vue"],
};

export default config;
