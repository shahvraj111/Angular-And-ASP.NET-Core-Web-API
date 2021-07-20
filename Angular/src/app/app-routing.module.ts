import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {DepartmentComponent} from './department/department.component';

const routes: Routes = [
  {
    path:'Department',
    component:DepartmentComponent
  },
];
  
@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
