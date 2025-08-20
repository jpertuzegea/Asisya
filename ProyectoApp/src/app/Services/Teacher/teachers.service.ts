import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { ResultModel } from '../../Models/ResultModel';
import { TeacherModel } from '../../Models/TeacherModel';

@Injectable({
  providedIn: 'root'
})
export class TeachersService {

  constructor(private http: HttpClient) { }

  public GetTeacherByTeacherId(id: number) {
    return this.http.post(environment.BaseUrl + "api/Teacher/GetTeacherByTeacherId", id);
  }

  public GetAllTeachers(): Observable<ResultModel> {
    return this.http.get<ResultModel>(environment.BaseUrl + "api/Teacher/TeacherList");
  }

  public SaveTeacher(Teacher: TeacherModel) {
    return this.http.post(environment.BaseUrl + "api/Teacher/TeacherAdd", Teacher);
  }

  public UpdateTeacher(Teacher: TeacherModel) {
    return this.http.put(environment.BaseUrl + "api/Teacher/TeacherUpdt", Teacher);
  }


  public DeleteTeacher(id: number) {
    return this.http.delete(environment.BaseUrl + "api/Teacher/TeacherDelete/" + id);
  }
}
