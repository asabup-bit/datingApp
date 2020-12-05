import { NgModule } from '@angular/core';
import {  CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ToastrModule } from 'ngx-toastr';
import { NgbNavModule } from '@ng-bootstrap/ng-bootstrap';
import { NgxGalleryModule } from '@kolkov/ngx-gallery';
import { FileUploadModule } from 'ng2-file-upload';
//import { NgxSpinnerModule } from "ngx-spinner";
import {BsDatepickerModule} from 'ngx-bootstrap/datepicker';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { ButtonsModule } from 'ngx-bootstrap/buttons';
import { TimeagoModule } from 'ngx-timeago';




@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    BsDropdownModule.forRoot(),
    ToastrModule.forRoot({
      positionClass:'toast-bottom-right'
    }),
    NgxGalleryModule,
  //  NgxSpinnerModule,
   FileUploadModule,
   BsDatepickerModule.forRoot(),
   PaginationModule.forRoot(),
   ButtonsModule.forRoot(),
   TimeagoModule.forRoot()

  ],
  exports:[
    BsDropdownModule,
    ToastrModule,
    NgxGalleryModule,
  //  NgxSpinnerModule,
   FileUploadModule,
   BsDatepickerModule,
   PaginationModule,
   ButtonsModule,
   TimeagoModule,
    
  ]
})
export class SharedModule { }
