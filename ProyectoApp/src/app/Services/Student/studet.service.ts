import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { ResultModel } from '../../Models/ResultModel';
import { StudentModel } from '../../Models/StudentModel';

@Injectable({
  providedIn: 'root'
})
export class StudentService {

  constructor(private http: HttpClient) { }

  public GetStudentByStudentId(id: number) {
    return this.http.post(environment.BaseUrl + "api/Student/GetStudentByStudentId", id);
  }

  public GetAllStudents(): Observable<ResultModel> {
    return this.http.get<ResultModel>(environment.BaseUrl + "api/Student/StudentList");
  }

  public SaveStudent(Student: StudentModel) {
    return this.http.post(environment.BaseUrl + "api/Student/StudentAdd", Student);
  }

  public UpdateStudent(Student: StudentModel) {
    return this.http.put(environment.BaseUrl + "api/Student/StudentUpdt", Student);
  }

  public DeleteStudent(id: number) {
    return this.http.delete(environment.BaseUrl + "api/Student/StudentDelete/" + id);
  }
   
}
