import { Component, OnInit } from '@angular/core';
import { ApiService } from '../api.service';
import { ActivatedRoute } from '@angular/router';
import {MatDialog, MatDialogRef, MAT_DIALOG_DATA} from '@angular/material';
import { FinishComponent } from '../finish/finish.component';

@Component({
  selector: 'app-play-quiz',
  templateUrl: './play-quiz.component.html',
  styleUrls: ['./play-quiz.component.css']
})
export class PlayQuizComponent implements OnInit {

  quizId;
  questions

  constructor(private api: ApiService, private router: ActivatedRoute, private dialog: MatDialog) { }

  ngOnInit() {
    this.quizId = this.router.snapshot.paramMap.get('quizId');


    this.api.getQuestions(this.quizId).subscribe(res => {
      this.questions = res;

      this.questions.forEach(question => {
        question.answers = [
          question.correctAnswer,
          question.answer1,
          question.answer2,
          question.answer3,
        ]
        shufle(question.answers);
      });

    });
  }

  finish() {
    var correct = 0;
    this.questions.forEach(question => {
      if (question.correctAnswer == question.selectedAnswer) {
        correct++;
      }
    });
    
    let dialogRef = this.dialog.open(FinishComponent, {
      width: '250px',
      data: { correct, total: this.questions.length }
    });

    console.log(correct);
  }

  step = 0;

  setStep(index: number) {
    this.step = index;
  }

  nextStep() {
    this.step++;
  }

  prevStep() {
    this.step--;
  }
}
function shufle(answer) {
  for (let i = answer.length; i; i--) {
    let j = Math.floor(Math.random() * i);
    [answer[i - 1], answer[j]] = [answer[j], answer[i - 1]];
  }
}

