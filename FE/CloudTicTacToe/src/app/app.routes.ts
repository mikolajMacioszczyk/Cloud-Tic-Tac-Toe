import { Routes } from '@angular/router';
import { RegisterComponent } from './components/register/register.component';
import { BoardComponent } from './components/board/board.component';
import { HomeComponent } from './components/home/home.component';
import { RankingComponent } from './components/ranking/ranking.component';

export const routes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'game', component: BoardComponent },
    { path: 'ranking', component: RankingComponent },
];
