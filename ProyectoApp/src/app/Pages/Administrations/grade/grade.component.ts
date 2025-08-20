import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { GradeModel } from '../../../Models/GradeModel';
import { SelectModel } from '../../../Models/SelectModel';
import { ResultModel } from '../../../Models/ResultModel';
import { GradesService } from '../../../Services/Grades/grades.service';
import { StudentService } from '../../../Services/Student/studet.service';
import { CourseService } from '../../../Services/Course/course.service';

@Component({
  selector: 'app-grade',
  templateUrl: './grade.component.html',
  styleUrls: ['./grade.component.css']
})
export class GradeComponent implements OnInit {


  constructor(private formBuilder: FormBuilder, private gradeService: GradesService, private studentService: StudentService, private courseService: CourseService) { }

  form: FormGroup;
  Action = "Registro";

  ListBooks: SelectModel[];
  List: GradeModel[] = [];
  showModal = false;

  ListStudents!: SelectModel[];
  ListCourses!: SelectModel[];


  ngOnInit(): void {

    this.GetSelecStudents();
    this.GetSelecCourses();
    this.ListAllGrades();

    this.form = this.formBuilder.group(
      {
        GradeId: '',
        StudentId: '',
        CourseId: '',
        GradeValue: [null, [Validators.required, Validators.min(0), Validators.max(10)]],
        GradeDate: '',

      }
    );

  }

  SaveChanges() {

    if (this.Action == "Registro") {
      this.SaveGrade();
    }

    if (this.Action == "Modificacion") {
      this.UpdateGrade();
    }

  }

  SaveGrade() {

    let Fields = this.GetFields();
    this.gradeService.SaveGrade(Fields).subscribe(
      ResultModel => {
        let Resu = ResultModel as ResultModel;
        if (!Resu.HasError) {
          alert(Resu.Messages);
          this.ListAllGrades();
          this.ShowModal(false, "Registro");
        } else {
          alert(Resu.Messages);
        }
      }, error => {

        if (error.status == 401) {
          alert("No Autorizado");
        } else {
          alert(JSON.stringify(error));
        }

      }
    );

  }

  ViewGrade(id: number) {
    this.ShowModal(true, "Modificacion");
    this.GetGradeByGradeId(id);
  }

  UpdateGrade() {

    let Fields = this.GetFields();

    this.gradeService.UpdateGrade(Fields).subscribe(
      ResultModel => {

        let Resu = ResultModel as ResultModel;
        if (!Resu.HasError) {

          alert(Resu.Messages);
          this.ListAllGrades();
          this.ShowModal(false, "Registro");

        } else {
          alert(Resu.Messages);
        }
      }, error => {

        if (error.status == 401) {
          alert("No Autorizado");
        } else {
          alert(JSON.stringify(error));
        }

      }
    );
  }

  ListAllGrades() {

    this.gradeService.GetAllGrades().subscribe(

      ResultModel => {

        let Resu = ResultModel as ResultModel;

        if (!Resu.HasError) {

          let Array = Resu.Data as GradeModel[];

          if (Resu.Data) {
            this.List = Array;
          } else {
            console.log('sin datos para mostrar')
          }

        } else {
          alert(Resu.Messages);
        }

      }, error => {

        if (error.status == 401) {
          alert("No Autorizado");
        } else {
          alert(JSON.stringify(error));
        }

      }
    );

  }

  GetGradeByGradeId(id: number) {

    this.gradeService.GetGradeByGradeId(id).subscribe(

      ResultModel => {
        let Resu = ResultModel as ResultModel;

        if (!Resu.HasError) {
          let Grade = Resu.Data as GradeModel;
          this.SetFields(Grade);
        }

        console.log(Resu);

      }, error => {

        if (error.status == 401) {
          alert("No Autorizado");
        } else {
          alert(JSON.stringify(error));
        }

      }
    );

  }


  ShowModal(View: boolean, Action: string) {

    if (!View) {
      this.CleanFields();
    }
    this.showModal = View;
    this.Action = Action;
  }


  DeleteGrade(id: number) {

    let respuesta = confirm("Esta seguro que desea eliminar el Gradee?");

    if (respuesta)
      this.gradeService.DeleteGrade(id).subscribe(
        ResultModel => {
          let Resu = ResultModel as ResultModel;

          if (!Resu.HasError) {
            this.ListAllGrades();
            alert(Resu.Messages);
          } else {
            alert(Resu.Messages);
          }

        }, error => {
          alert(JSON.stringify(error));
        }
      );
  }


  CleanFields() {

    this.form.controls['GradeId'].setValue("");
    this.form.controls['StudentId'].setValue("");
    this.form.controls['CourseId'].setValue("");
    this.form.controls['GradeValue'].setValue("");
    this.form.controls['GradeDate'].setValue("");

  }

  GetFields() {

    let Field = new GradeModel();

    Field.GradeId = this.form.get("GradeId").value;
    Field.StudentId = this.form.get("StudentId").value;
    Field.CourseId = this.form.get("CourseId").value;
    Field.GradeValue = this.form.get("GradeValue").value;
    Field.GradeDate = this.form.get("GradeDate").value;

    return Field;

  }

  SetFields(Grade: GradeModel) {

    this.form.controls['GradeId'].setValue(Grade.GradeId);
    this.form.controls['StudentId'].setValue(Grade.StudentId);
    this.form.controls['CourseId'].setValue(Grade.CourseId);
    this.form.controls['GradeValue'].setValue(Grade.GradeValue);
    this.form.controls['GradeDate'].setValue(Grade.GradeDate);

  }

  public GetSelecStudents() {
    this.studentService.GetAllStudents().subscribe(
      ResultModel => {
        let Resu = ResultModel as unknown as ResultModel;

        if (!Resu.HasError) {

          this.ListStudents = Resu.Data.map(s => {
            return {
              Value: s.StudentId,
              Text: `${s.FirstName} ${s.LastName}`
            };
          });

        }
      }, error => {
        console.log(error);
        if (error.status == 401) {
          alert("No Autorizado");
        } else {
          alert(JSON.stringify(error));
        }
      }
    );
  }

  public GetSelecCourses() {
    this.courseService.GetAllCourses().subscribe(
      ResultModel => {
        let Resu = ResultModel as unknown as ResultModel;

        if (!Resu.HasError) {

          this.ListCourses = Resu.Data.map(s => {
            return {
              Value: s.CourseId,
              Text: s.CourseName
            };
          });

        }
      }, error => {
        console.log(error);
        if (error.status == 401) {
          alert("No Autorizado");
        } else {
          alert(JSON.stringify(error));
        }
      }
    );
  }


}  
