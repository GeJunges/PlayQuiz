import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';

@Component({
  selector: 'app-quiz',
  templateUrl: './quiz.component.html',
  styleUrls: ['./quiz.component.css']
})
export class QuizComponent implements OnInit {

  quiz = {};

  constructor(private api: ApiService) {
    this.api.postQuiz(this.quiz);
  }

  ngOnInit() {
    this.api.quizSelected.subscribe(quiz => this.quiz = quiz);
  }
}



