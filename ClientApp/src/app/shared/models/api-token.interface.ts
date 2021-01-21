import { User } from "./user.interface";

export interface ApiToken {
  accessToken: string;
  user: User;
}
