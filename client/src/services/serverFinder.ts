import axios from "axios";

export interface Result {
  success: boolean;
  address?: string;
}

export async function find(): Promise<Result> {
  const addressBase = "http://192.168.0.";
  const port = 11111;

  // not very good approach so far
  for (let i = 0; i < 255; i++) {
    const address = addressBase + i + ":" + port;
    const checkResult: Result = await check(address);
    if (checkResult.success) {
      return checkResult;
    }
  }

  return { success: false };
}

async function check(address: string): Promise<Result> {
  try {
    await axios.get(address + "/api/hc", { timeout: 100 });

    return { success: true, address: address };
    // eslint-disable-next-line no-empty
  } catch (e) {
    return { success: false };
  }
}
