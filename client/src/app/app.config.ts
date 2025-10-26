import {ApplicationConfig, provideZonelessChangeDetection} from "@angular/core";
import {PreloadAllModules, provideRouter, RouteReuseStrategy, withPreloading} from "@angular/router";
import {IonicRouteStrategy} from "@ionic/angular/standalone";
import {provideIonicAngular} from "@ionic/angular/standalone";
import {routes} from "./app.routes";
import * as echarts from 'echarts/core';
import {PieChart} from "echarts/charts";
import {CanvasRenderer} from "echarts/renderers";
import {provideEchartsCore} from "ngx-echarts";
import {TitleComponent} from "echarts/components";

echarts.use([PieChart, CanvasRenderer, TitleComponent]);

export const appConfig: ApplicationConfig = {
  providers: [
    { provide: RouteReuseStrategy, useClass: IonicRouteStrategy },
    provideIonicAngular(),
    provideZonelessChangeDetection(),
    provideEchartsCore({ echarts }),
    provideRouter(routes, withPreloading(PreloadAllModules))
  ]
}
