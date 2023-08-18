import {Component, Input, OnInit, ViewChild} from '@angular/core';
import {Message} from "../../_models/message";
import {MessageService} from "../../_services/message.service";
import {CommonModule} from "@angular/common";
import {TimeagoModule} from "ngx-timeago";
import {FormsModule, NgForm} from "@angular/forms";

@Component({
  selector: 'app-member-messages',
  standalone: true,
  templateUrl: './member-messages.component.html',
  imports: [
    CommonModule,
    TimeagoModule,
    FormsModule
  ],
  styleUrls: ['./member-messages.component.css']
})
export class MemberMessagesComponent implements OnInit {
  @ViewChild('messageForm') messageForm?: NgForm;
  @Input() username?: string;
  @Input() messages: Message[] = [];
  messageContent = '';

  constructor(private messageService: MessageService) {
  }

  ngOnInit() {
  }

  sendMessage() {
    if (!this.username) return;

    this.messageService.sendMessage(this.username, this.messageContent).subscribe({
      next: message => {
        this.messages.push(message);
        this.messageForm?.reset();
      }
    })
  }
}
