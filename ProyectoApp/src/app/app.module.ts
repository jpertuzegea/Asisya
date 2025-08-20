import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FooterComponent } from './Shared/footer/footer.component';
import { HomeComponent } from './Shared/home/home.component';
import { NavBarComponent } from './Shared/nav-bar/nav-bar.component';
import { WorkSpaceComponent } from './Shared/work-space/work-space.component';
import { LoginComponent } from './Pages/Security/login/login.component';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms'; 
import { InterceptorService } from './Services/Interceptors/interceptor.service'; 
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';  
import { TeachersComponent } from './Pages/Administrations/teachers/teachers.component';
import { CourseComponent } from './Pages/Administrations/course/course.component';
import { StudentsComponent } from './Pages/Administrations/students/students.component';
import { GradeComponent } from './Pages/Administrations/grade/grade.component';

@NgModule({
  declarations: [
    AppComponent,
    FooterComponent,
    HomeComponent,
    NavBarComponent,
    WorkSpaceComponent,
    LoginComponent,
    TeachersComponent,
    CourseComponent,
    StudentsComponent,
    GradeComponent,
  ],

  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    FormsModule,
    HttpClientModule,
    CommonModule
  ],

  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: InterceptorService,
      multi: true
    }
  ],

  bootstrap: [AppComponent]

})
export class AppModule { }
