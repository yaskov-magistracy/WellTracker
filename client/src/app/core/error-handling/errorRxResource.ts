import { rxResource, RxResourceOptions } from '@angular/core/rxjs-interop';
import { effect, ErrorHandler, inject, ResourceRef } from '@angular/core';

export function errorRxResource<T, R>(opts: RxResourceOptions<T, R>): ResourceRef<T | undefined> {
  const errorHandler = opts.injector?.get(ErrorHandler) ?? inject(ErrorHandler);
  const resource = rxResource(opts);
  effect(() => {
    const error = resource.error();
    if (!(error instanceof Error)) { return; }
    if ('cause' in error) { errorHandler.handleError(error.cause); }
    else { errorHandler.handleError(error); }
  }, { injector: opts.injector });
  return resource;
}
