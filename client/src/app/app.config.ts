import {ApplicationConfig, ErrorHandler, provideZonelessChangeDetection} from "@angular/core";
import {PreloadAllModules, provideRouter, RouteReuseStrategy, withDebugTracing, withPreloading} from "@angular/router";
import {IonicRouteStrategy} from "@ionic/angular/standalone";
import {provideIonicAngular} from "@ionic/angular/standalone";
import {routes} from "./app.routes";
import * as echarts from 'echarts/core';
import {BarChart, LineChart, PieChart} from "echarts/charts";
import {CanvasRenderer} from "echarts/renderers";
import {provideEchartsCore} from "ngx-echarts";
import {
  DataZoomComponent,
  GraphicComponent,
  GridComponent,
  LegendComponent,
  TitleComponent,
  TooltipComponent
} from "echarts/components";
import {provideHttpClient} from "@angular/common/http";
import {CustomErrorHandler} from "./core/error-handling/custom-error-handler";
import {provideRxjsOnUnhandledError} from "./core/providers/rxjs-onUnhandledError.provider";

echarts.use([PieChart, BarChart, LineChart, GridComponent, CanvasRenderer, GraphicComponent,
  LegendComponent, TitleComponent, DataZoomComponent, TooltipComponent]);

export const appConfig: ApplicationConfig = {
  providers: [
    { provide: RouteReuseStrategy, useClass: IonicRouteStrategy },
    { provide: ErrorHandler, useClass: CustomErrorHandler },
    provideRxjsOnUnhandledError(),
    provideIonicAngular(),
    provideZonelessChangeDetection(),
    provideEchartsCore({ echarts }),
    provideHttpClient(),
    provideRouter(routes, withPreloading(PreloadAllModules))
  ]
}
