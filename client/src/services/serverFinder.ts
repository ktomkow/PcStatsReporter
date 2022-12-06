import axios from "axios";

export interface Result {
  success: boolean;
  address?: string;
}

export async function find(): Promise<Result> {
  const addressBase = "http://192.168.0.";
  const port = 11111;

  console.log("lets go");
  // not very good approach so far
  for (let i = 0; i < 255; i++) {
    const address = addressBase + i + ":" + port;
    try {
      await axios.get(address, { timeout: 100 });

      console.log("Success", address);
      return { success: true, address: address };
      // eslint-disable-next-line no-empty
    } catch (e) {
      console.debug(address, e);
    }
  }

  return { success: false };
}
