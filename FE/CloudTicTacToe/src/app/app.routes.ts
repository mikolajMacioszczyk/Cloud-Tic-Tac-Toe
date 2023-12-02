import { Routes } from '@angular/router';
import { RegisterComponent } from './register/register.component';
import { BoardComponent } from './board/board.component';

export const routes: Routes = [
    { path: '', component: RegisterComponent },
    { path: 'game', component: BoardComponent },
];
