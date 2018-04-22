import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { AuthService } from '../auth.service';


@Component({
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  form

  constructor(private auth: AuthService, private fb: FormBuilder) {

    this.form = fb.group({
      email: ['', Validators.required],
      password: ['', Validators.required],
    });
  }

  register() {
    this.auth.register(this.form.value);
  }

  ngOnInit() {
  }

}
