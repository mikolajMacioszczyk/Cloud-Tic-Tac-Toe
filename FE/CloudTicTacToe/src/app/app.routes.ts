import { Routes } from '@angular/router';
import { RegisterComponent } from './components/register/register.component';
import { BoardComponent } from './components/board/board.component';
import { HomeComponent } from './components/home/home.component';

export const routes: Routes = [
    { path: '', component: HomeComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'game', component: BoardComponent },
];
