import {ErrorHandler, inject, Injectable} from '@angular/core';
import {ToastController} from "@ionic/angular";
import { HttpErrorResponse } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CustomErrorHandler implements ErrorHandler {

  #toastController = inject(ToastController);

  async handleError(error: HttpErrorResponse | Error) {
    let errorMessage: string | null = null;
    if (error instanceof HttpErrorResponse) {
      errorMessage = error.error;
    }
    if (!errorMessage) { return; }
    const toast = await this.#toastController.create({
      message: errorMessage,
      duration: 1500,
      position: 'bottom',
      color: 'danger'
    });
    await toast.present();
    }
}
