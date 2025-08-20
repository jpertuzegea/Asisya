import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { ResultModel } from '../../Models/ResultModel';
import { GradeModel } from '../../Models/GradeModel';

@Injectable({
  providedIn: 'root'
})
export class GradesService {

  constructor(private http: HttpClient) { }

  public GetGradeByGradeId(id: number) {
    return this.http.post(environment.BaseUrl + "api/Grade/GetGradeByGradeId", id);
  }

  public GetAllGrades(): Observable<ResultModel> {
    return this.http.get<ResultModel>(environment.BaseUrl + "api/Grade/GradeList");
  }

  public SaveGrade(Grade: GradeModel) {
    return this.http.post(environment.BaseUrl + "api/Grade/GradeAdd", Grade);
  }

  public UpdateGrade(Grade: GradeModel) {
    return this.http.put(environment.BaseUrl + "api/Grade/GradeUpdt", Grade);
  }


  public DeleteGrade(id: number) {
    return this.http.delete(environment.BaseUrl + "api/Grade/GradeDelete/" + id);
  }
}
