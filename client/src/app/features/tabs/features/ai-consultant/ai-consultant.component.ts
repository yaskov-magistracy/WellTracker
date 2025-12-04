import {
  AfterViewChecked,
  ChangeDetectionStrategy,
  Component,
  computed, effect, ElementRef,
  inject, Injector,
  linkedSignal,
  OnInit,
  signal, untracked,
  viewChild
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
import {httpResource} from "@angular/common/http";
import {AiConsultantService} from "./services/ai-consultant.service";
import {errorRxResource} from "../../../../core/error-handling/errorRxResource";
import {Chat} from "./types/Chat";
import {of} from "rxjs";
import {FormControl, ReactiveFormsModule, Validators} from "@angular/forms";
import {CustomValidatorsService} from "../../../../core/validation/custom-validators.service";

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
    IonLabel,
    IonButton,
    IonTextarea,
    IonIcon,
    ReactiveFormsModule,
    IonFab,
    IonFabButton
  ],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export default class AiConsultantComponent implements OnInit {

  #aiConsultantS = inject(AiConsultantService);
  #toastCtrl = inject(ToastController);
  #customValidatorsS = inject(CustomValidatorsService);
  #injector = inject(Injector);

  #take = 100000;
  #skip = 0;

  protected mainContent = viewChild.required<ElementRef<HTMLDivElement>>('mainContent')
  protected scrollElement = computed(() => this.mainContent().nativeElement);

  currentMessageControl = new FormControl<string>('',
    [
      Validators.required,
      this.#customValidatorsS.nonEmptyTrimmedValidator
    ]);

  chatsResource = errorRxResource({
    stream: () => this.#aiConsultantS.getChats$()
  });

  currentChatMessagesResource = errorRxResource({
    params: () => this.currentChat(),
    stream: ({ params: chat }) => {
      return chat ?
        this.#aiConsultantS.getChatMessages$(chat.id, this.#take, this.#skip) :
        of([]);
    }
  });

  currentChat = linkedSignal<Chat | null>(() => {
    const currentChats = this.chatsResource.value();
    return currentChats?.[0] ?? null;
  });

  currentChatMessages = linkedSignal(() =>
    this.currentChatMessagesResource.value() ?? []);

  protected showScrollDownButton = signal(false);

  ngOnInit() {
    this.#listenCurrentChatChange();
  }

  protected onScrollMainContent() {
    const a = this.scrollElement().scrollHeight;
    const b = this.scrollElement().scrollTop;
    const c = this.scrollElement().clientHeight
    this.showScrollDownButton.set(
      this.scrollElement().scrollHeight - this.scrollElement().scrollTop - this.scrollElement().clientHeight > 100
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
    this.scrollElement().scrollTo({
      top: this.scrollElement().scrollHeight,
      behavior: 'smooth'
    })
  }

  #listenCurrentChatChange() {
    effect(() => {
      const currentChat = this.currentChat();
      untracked(() => this.onScrollMainContent());
    }, { injector: this.#injector });
  }
}
