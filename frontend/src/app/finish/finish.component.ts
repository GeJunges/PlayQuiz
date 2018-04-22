import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material';


@Component({
  selector: 'app-finish',
  templateUrl: './finish.component.html',
  styleUrls: ['./finish.component.css']
})
export class FinishComponent implements OnInit {

  constructor(@Inject(MAT_DIALOG_DATA) private data) {
    
   }


  ngOnInit() {
  }

}
