import { ErrorHandler, inject, provideAppInitializer } from '@angular/core';
import { config } from 'rxjs';

export const provideRxjsOnUnhandledError = () => {
  return provideAppInitializer(() => {
    const errorHandler = inject(ErrorHandler);
    config.onUnhandledError = err => {
      errorHandler.handleError(err);
    }
  })
}
