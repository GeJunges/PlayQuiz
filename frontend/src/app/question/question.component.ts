import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';
import { ActivatedRoute } from '@angular/router'

@Component({
  selector: 'app-question',
  templateUrl: './question.component.html',
  styleUrls: ['./question.component.css']
})
export class QuestionComponent implements OnInit {

  question = {};
  quizId;

  constructor(private api: ApiService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.quizId = this.route.snapshot.paramMap.get('quizId');
    this.api.questionSelected.subscribe(question => this.question = question);
  }

  post(question) {
    question.quizId = this.quizId;
    this.api.postQuestion(question);
  }
}

