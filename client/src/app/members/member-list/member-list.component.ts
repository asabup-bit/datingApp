import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { take } from 'rxjs/operators';
import { Member } from 'src/app/_models/Member';
import { Pagination } from 'src/app/_models/Pagination';
import { User } from 'src/app/_models/user';
import { UserParams } from 'src/app/_models/userParams';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
  
})
export class MemberListComponent implements OnInit {

  members: Member[];
  pagination: Pagination;
  userParams: UserParams;
  user: User;
  genderList = [{ value: 'male', display: 'Males' }, { value: 'female', display: 'Females' }];

 
  constructor( private memberServices:MembersService, ) {
    
    this.userParams =this.memberServices.getUserParams();
   }

  ngOnInit(): void {

   this.loadMembers();
  }

  loadMembers(){
    this.memberServices.setUserParams(this.userParams);
    this.memberServices.getMembers(this.userParams).subscribe( response =>
      {
        this.members =response.result;
        this.pagination =response.pagination;
      }) 
  }

  pageChanged(event:any){
    this.userParams.pageNumber=event.page;
    this.memberServices.setUserParams(this.userParams);
    this.loadMembers();
  }

  resetFilters() {
    this.userParams = this.memberServices.resetUserParams();
    this.loadMembers();
  }
  
  }

 
  


