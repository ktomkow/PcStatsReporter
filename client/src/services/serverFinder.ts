import axios from "axios";
import storage from "src/services/storage/storage";
export interface Result {
  isSuccess: boolean;
  address?: string;
}

interface StoredAddress {
  address: string;
}

export async function find(): Promise<Result> {
  const addressBase = "http://192.168.0.";
  const port = 11111;

  const lastAddressCheckResult = await checkLastAddress();
  if (lastAddressCheckResult.isSuccess) {
    return lastAddressCheckResult;
  } else {
    storage.storage.remove(storage.keys.SERVER_ADDRESS);
  }

  // not very good approach so far
  for (let i = 0; i < 255; i++) {
    const address = addressBase + i + ":" + port;
    const checkResult: Result = await check(address);
    if (checkResult.isSuccess) {
      const addressToStore: StoredAddress = { address: address };
      await storage.storage.set(storage.keys.SERVER_ADDRESS, addressToStore);
      return checkResult;
    }
  }

  return { isSuccess: false };
}

async function check(address: string): Promise<Result> {
  try {
    await axios.get(address + "/api/hc", { timeout: 1000 });

    return { isSuccess: true, address: address };
    // eslint-disable-next-line no-empty
  } catch (e) {
    return { isSuccess: false };
  }
}

async function checkLastAddress(): Promise<Result> {
  const storedAddress: StoredAddress = await storage.storage.get(
    storage.keys.SERVER_ADDRESS
  );

  if (!storedAddress) {
    return { isSuccess: false };
  }

  const storedAddressCheckResult = await check(storedAddress.address);

  return storedAddressCheckResult;
}
