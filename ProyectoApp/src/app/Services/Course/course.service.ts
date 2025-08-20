import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { ResultModel } from '../../Models/ResultModel';
import { CourseModel } from '../../Models/CourseModel';

@Injectable({
  providedIn: 'root'
})
export class CourseService {

  constructor(private http: HttpClient) { }

  public GetCourseByCourseId(id: number) {
    return this.http.post(environment.BaseUrl + "api/Course/GetCourseByCourseId", id);
  }

  public GetAllCourses(): Observable<ResultModel> {
    return this.http.get<ResultModel>(environment.BaseUrl + "api/Course/CourseList");
  }

  public SaveCourse(Course: CourseModel) {
    return this.http.post(environment.BaseUrl + "api/Course/CourseAdd", Course);
  }

  public UpdateCourse(Course: CourseModel) {
    return this.http.put(environment.BaseUrl + "api/Course/CourseUpdt", Course);
  }


  public DeleteCourse(id: number) {
    return this.http.delete(environment.BaseUrl + "api/Course/CourseDelete/" + id);
  }

}
