import {ApplicationConfig, provideZonelessChangeDetection} from "@angular/core";
import {PreloadAllModules, provideRouter, RouteReuseStrategy, withPreloading} from "@angular/router";
import {IonicRouteStrategy} from "@ionic/angular/standalone";
import {provideIonicAngular} from "@ionic/angular/standalone";
import {routes} from "./app.routes";

export const appConfig: ApplicationConfig = {
  providers: [
    { provide: RouteReuseStrategy, useClass: IonicRouteStrategy },
    provideIonicAngular(),
    provideZonelessChangeDetection(),
    provideRouter(routes, withPreloading(PreloadAllModules))
  ]
}
