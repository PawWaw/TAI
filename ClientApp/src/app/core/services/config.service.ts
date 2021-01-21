import { Injectable } from "@angular/core";
import { AppConfiguration } from "read-appsettings-json";

@Injectable({
  providedIn: "root"
})
export class ConfigService {

  public ApiUrl: string;

  constructor() {
    this.ApiUrl = AppConfiguration.Setting().ApiUrl;
  }
}
