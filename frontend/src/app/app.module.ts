import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {
  MatButtonModule,
  MatCheckboxModule,
  MatInputModule,
  MatCardModule,
  MatListModule,
  MatToolbarModule,
  MatExpansionModule,
  MatRadioModule,
  MatDialogModule
} from '@angular/material';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { QuestionComponent } from './question/question.component';
import { ApiService } from './api.service';
import { QuestionsComponent } from './questions/questions.component';
import { HomeComponent } from './home/home.component';
import { NavComponent } from './nav/nav.component';
import { QuizComponent } from './quiz/quiz.component';
import { QuizzesComponent } from './quizzes/quizzes.component';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component'
import { AuthService } from './auth.service'
import { AuthInterceptor } from './auth.interceptor';
import { PlayComponent } from './play/play.component';
import { PlayQuizComponent } from './play-quiz/play-quiz.component';
import { FinishComponent } from './finish/finish.component';

const routes = [
  { path: '', component: HomeComponent },
  { path: 'question', component: QuestionComponent },
  { path: 'question/:quizId', component: QuestionComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'login', component: LoginComponent },
  { path: 'quiz', component: QuizComponent },
  { path: 'play', component: PlayComponent },
  { path: 'playQuiz/:quizId', component: PlayQuizComponent },
];

@NgModule({
  declarations: [
    AppComponent,
    QuestionComponent,
    QuestionsComponent,
    HomeComponent,
    NavComponent,
    QuizComponent,
    QuizzesComponent,
    RegisterComponent,
    LoginComponent,
    PlayComponent,
    PlayQuizComponent,
    FinishComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    RouterModule.forRoot(routes),
    FormsModule,
    ReactiveFormsModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatCheckboxModule,
    MatInputModule,
    MatCardModule,
    MatListModule,
    MatToolbarModule,
    MatExpansionModule,
    MatRadioModule,
    MatDialogModule
  ],
  providers: [ApiService, AuthService, {
    provide: HTTP_INTERCEPTORS,
    useClass: AuthInterceptor,
    multi: true
  }],
  bootstrap: [AppComponent],
  entryComponents:[FinishComponent]
})
export class AppModule { }
