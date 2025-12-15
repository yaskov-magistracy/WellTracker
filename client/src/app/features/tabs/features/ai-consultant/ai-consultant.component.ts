import {
  afterEveryRender, afterNextRender,
  ChangeDetectionStrategy,
  Component,
  computed, effect, ElementRef,
  inject, Injector,
  linkedSignal,
  OnInit,
  signal,
  viewChild, WritableSignal
} from '@angular/core';
import {
  IonButton,
  IonButtons,
  IonContent, IonFab, IonFabButton,
  IonHeader, IonIcon, IonItem, IonLabel, IonList,
  IonMenu,
  IonMenuButton, IonTextarea,
  IonTitle,
  IonToolbar, ToastController
} from "@ionic/angular/standalone";
import {AiConsultantService} from "./services/ai-consultant.service";
import {errorRxResource} from "../../../../core/error-handling/errorRxResource";
import {Chat} from "./types/Chat";
import {asap, asapScheduler, EMPTY, of, switchMap, tap} from "rxjs";
import {FormControl, ReactiveFormsModule, Validators} from "@angular/forms";
import {CustomValidatorsService} from "../../../../core/validation/custom-validators.service";
import {Message} from "./types/Message";
import {SendMessageInChatResponseDTO} from "./dal/DTO/responses/SendMessageInChatResponseDTO";
import {MarkdownComponent} from "ngx-markdown";

@Component({
  selector: 'app-ai-consultant',
  templateUrl: './ai-consultant.component.html',
  styleUrls: ['./ai-consultant.component.scss'],
  imports: [
    IonButtons,
    IonHeader,
    IonMenuButton,
    IonTitle,
    IonToolbar,
    IonMenu,
    IonContent,
    IonList,
    IonItem,
    IonButton,
    IonTextarea,
    IonIcon,
    ReactiveFormsModule,
    IonFab,
    IonFabButton,
    MarkdownComponent
  ],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export default class AiConsultantComponent {

  #aiConsultantS = inject(AiConsultantService);
  #toastCtrl = inject(ToastController);
  #customValidatorsS = inject(CustomValidatorsService);
  #injector = inject(Injector);

  constructor() {
    afterEveryRender(() => this.onScrollMainContent(), { injector: this.#injector });
  }

  protected mainContent = viewChild.required<ElementRef<HTMLDivElement>>('mainContent')
  protected messagesElement = computed(() => this.mainContent().nativeElement);

  #take = 100000;
  #skip = 0;
  #userTemporarilyMessageId = 'userTemporarilyMessageId';
  #botTemporarilyMessageId = 'botTemporarilyMessageId';
  #chatMessagesRetrieveBlocked = signal(false);

  currentMessageControl = new FormControl<string>('',
    { validators: [ Validators.required, this.#customValidatorsS.nonEmptyTrimmedValidator], nonNullable: true });

  chatsResource = errorRxResource({
    stream: () => this.#aiConsultantS.getChats$()
  });

  chats = linkedSignal(() => this.chatsResource.value() ?? []);

  currentChatMessagesResource = errorRxResource({
    params: () => this.currentChat(),
    stream: ({ params: chat }) => {
      if (this.#chatMessagesRetrieveBlocked()) {
        return of(undefined);
      }
      return chat ?
        this.#aiConsultantS.getChatMessages$(chat.id, this.#take, this.#skip) :
        of([]);
    }
  });

  currentChat = linkedSignal<Chat | null>(() => {
    const currentChats = this.chatsResource.value();
    return currentChats?.[0] ?? null;
  });

  currentChatMessages: WritableSignal<Message[]> = linkedSignal(
    {
      source: () => this.currentChatMessagesResource.value(),
      computation: (source, previous) => {
        return source ?? previous?.value ?? [];
      }
    }
  );

  protected showScrollDownButton = signal(false);


  protected sendMessage() {
    const messageText = this.currentMessageControl.value.trim();
    this.#addTemporarilyMessages(messageText);
    afterNextRender(() => this.scrollToTheBottomOfMessagesList(), { injector: this.#injector })
    this.currentMessageControl.setValue('');
    this.currentMessageControl.disable();
    if (this.currentChat()) {
      this.#sendMessageInExistingChat$(messageText).subscribe();
      return;
    }
    this.#chatMessagesRetrieveBlocked.set(true);
    this.#aiConsultantS.createChat$(messageText.slice(0, 20))
      .pipe(
        tap(newChat => {
          this.currentChat.set(newChat);
          this.chats.update(chats => [newChat, ...chats]);
        }),
        switchMap(() => this.#sendMessageInExistingChat$(messageText))
      ).subscribe(() => this.#chatMessagesRetrieveBlocked.set(false))
  }

  protected onScrollMainContent() {
    this.showScrollDownButton.set(
      this.messagesElement().scrollHeight - this.messagesElement().scrollTop - this.messagesElement().clientHeight > 100
    );
  }

  async addNewChat() {
    if (this.currentChat()) {
      this.currentChat.set(null);
      return;
    }
    const toast = await this.#toastCtrl.create({
      message: 'Уже в новом чате',
      duration: 1000,
      position: 'top',
      color: 'medium'
    });
    await toast.present();
  }

  protected scrollToTheBottomOfMessagesList() {
    this.messagesElement().scrollTo({
      top: this.messagesElement().scrollHeight,
      behavior: 'smooth'
    })
  }

  #sendMessageInExistingChat$(messageText: string) {
    return this.#aiConsultantS.sendMessageInChat$(messageText, this.currentChat()!.id)
      .pipe(
        tap(botAnswer => {
          this.currentMessageControl.enable();
          this.#replaceTemporarilyMessages(botAnswer);
          this.chats.update(chats => chats.sort(c => c.id === this.currentChat()!.id ? -1 : 1))
        }),
      );
  }

  #addTemporarilyMessages(message: string) {
    const currentTime = new Date().getTime();
    const userMessage: Message = {
      id: this.#userTemporarilyMessageId,
      message,
      dateTime: new Date(currentTime - 1).toISOString(),
      isBot: false
    };
    const botMessage: Message = {
      id: this.#botTemporarilyMessageId,
      message: 'Думаю...',
      dateTime: new Date(currentTime).toISOString(),
      isBot: true
    };
    this.currentChatMessages.update(messages => [...messages, userMessage, botMessage]);
  }

  #replaceTemporarilyMessages(botAnswer: SendMessageInChatResponseDTO) {
    this.currentChatMessages.update(messages => messages
      .map(msg => {
        if (msg.id === this.#userTemporarilyMessageId) { return botAnswer.sent; }
        else if (msg.id === this.#botTemporarilyMessageId) { return botAnswer.received; }
        return msg;
      })
    );
  }
}
