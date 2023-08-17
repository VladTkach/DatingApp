import {Component, Input} from '@angular/core';
import {Member} from "../../_models/member";
import {MembersService} from "../../_services/members.service";
import {ToastrService} from "ngx-toastr";

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css']
})
export class MemberCardComponent {
  @Input() member: Member | undefined;

  constructor(private membersService: MembersService, private toastr: ToastrService) {
  }

  addLike(member: Member){
    this.membersService.addLike(member.userName).subscribe({
      next: () => this.toastr.success('You have liked ' + member.userName)
    })
  }
}
