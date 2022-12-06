import { LocalStorage } from "quasar";

export enum StorageKey {
  SERVER_ADDRESS = "SERVER_ADDRESS",
}

export interface Storage {
  get(key: StorageKey): Promise<any>;
  set(key: StorageKey, value: any): Promise<void>;
  remove(key: StorageKey): Promise<void>;
}

class MyLocalStorage implements Storage {
  remove(key: StorageKey): Promise<void> {
    return new Promise((resolve) => {
      LocalStorage.remove(key);
      resolve();
    });
  }

  get(key: StorageKey): Promise<any> {
    return new Promise((resolve) => {
      const result: any = LocalStorage.getItem(key);
      resolve(result);
    });
  }

  set(key: StorageKey, value: any): Promise<void> {
    return new Promise((resolve) => {
      LocalStorage.set(key, value);
      resolve();
    });
  }
}

const storage: Storage = new MyLocalStorage();

export default {
  storage: storage,
  keys: StorageKey,
};
