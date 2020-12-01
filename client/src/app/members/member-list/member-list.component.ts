import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Member } from 'src/app/_models/Member';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.css']
  
})
export class MemberListComponent implements OnInit {

  members: Member[] = [];
  constructor( private memberServices:MembersService) {
   }

  ngOnInit(): void {

   this.loadMember();
  }

  loadMember(){
   
  this.memberServices.getMembers().subscribe( members =>{
      
     this.members =members;
    //console.log(this.members.length);
    });
    // this.members.push({
    //   knowAs: 'bikash'
    // });
    // this.members.push({
    //   knowAs: 'Arshad'
    // });
    // console.log(this.members);
  }

 
  }


