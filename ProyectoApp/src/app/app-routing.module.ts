import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router'; 
import { HomeComponent } from './Shared/home/home.component';  
import { AuthenticationGuard } from './Guards/authentication.guard';  
import { TeachersComponent } from './Pages/Administrations/teachers/teachers.component';
import { CourseComponent } from './Pages/Administrations/course/course.component';
import { StudentsComponent } from './Pages/Administrations/students/students.component';
import { GradeComponent } from './Pages/Administrations/grade/grade.component';

 
const routes: Routes = [

  { path: 'home', component: HomeComponent, canActivate: [AuthenticationGuard] },
   
  { path: 'teachers', component: TeachersComponent, canActivate: [AuthenticationGuard] },
  { path: 'courses', component: CourseComponent, canActivate: [AuthenticationGuard] },
  { path: 'students', component: StudentsComponent, canActivate: [AuthenticationGuard] },
  { path: 'grades', component: GradeComponent, canActivate: [AuthenticationGuard] },

   
  //  
  { path: '**', redirectTo: 'home' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
