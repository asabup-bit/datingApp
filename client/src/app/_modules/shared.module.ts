import { NgModule } from '@angular/core';
import {  CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ToastrModule } from 'ngx-toastr';
import { NgbNavModule } from '@ng-bootstrap/ng-bootstrap';
import { NgxGalleryModule } from '@kolkov/ngx-gallery';
//import { NgxSpinnerModule } from "ngx-spinner";




@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    BsDropdownModule.forRoot(),
    ToastrModule.forRoot({
      positionClass:'toast-bottom-right'
    }),
    NgxGalleryModule,
   // NgxSpinnerModule,

  ],
  exports:[
    BsDropdownModule,
    ToastrModule,
    NgxGalleryModule,
   // NgxSpinnerModule
    
  ]
})
export class SharedModule { }
