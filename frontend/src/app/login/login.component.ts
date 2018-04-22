import { Component, OnInit } from '@angular/core';
import { AuthService } from '../auth.service';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  form

  constructor(private auth: AuthService, private fb: FormBuilder) {

    this.form = fb.group({
      email: ['', Validators.required],
      password: ['', Validators.required],
    });
  }

  login() {
    this.auth.login(this.form.value);
  }

  ngOnInit() {
  }

}
