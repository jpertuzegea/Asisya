import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { SelectModel } from '../../../Models/SelectModel';
import { CourseModel } from '../../../Models/CourseModel';
import { CourseService } from '../../../Services/Course/course.service';
import { TeachersService } from '../../../Services/Teacher/teachers.service';
import { ResultModel } from '../../../Models/ResultModel';

@Component({
  selector: 'app-course',
  templateUrl: './course.component.html',
  styleUrls: ['./course.component.css']
})
export class CourseComponent implements OnInit {

  constructor(private formBuilder: FormBuilder, private courseService: CourseService, private teacherService: TeachersService) { }

  form: FormGroup;
  Action = "Registro";

  ListTeachers: SelectModel[];
  List: CourseModel[] = [];

  showModal = false;


  ngOnInit(): void {
    this.GetSelecTeachers();
    this.ListAllCourses();

    this.form = this.formBuilder.group(
      {
        CourseId: '',
        CourseName: '',
        Description: '',
        TeacherId: '',

      }
    );

  }

  SaveChanges() {

    if (this.Action == "Registro") {
      this.SaveCourse();
    }

    if (this.Action == "Modificacion") {
      this.UpdateCourse();
    }

  }

  SaveCourse() {

    let Fields = this.GetFields();
    this.courseService.SaveCourse(Fields).subscribe(
      ResultModel => {
        let Resu = ResultModel as ResultModel;
        if (!Resu.HasError) {
          alert(Resu.Messages);
          this.ListAllCourses();
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

  ViewCourse(id: number) {
    this.ShowModal(true, "Modificacion");
    this.GetCourseByCourseId(id);
  }

  UpdateCourse() {

    let Fields = this.GetFields();

    this.courseService.UpdateCourse(Fields).subscribe(
      ResultModel => {

        let Resu = ResultModel as ResultModel;
        if (!Resu.HasError) {

          alert(Resu.Messages);
          this.ListAllCourses();
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

  ListAllCourses() {

    this.courseService.GetAllCourses().subscribe(

      ResultModel => {

        let Resu = ResultModel as ResultModel;

        if (!Resu.HasError) {

          let Array = Resu.Data as CourseModel[];

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

  GetCourseByCourseId(id: number) {

    this.courseService.GetCourseByCourseId(id).subscribe(

      ResultModel => {
        let Resu = ResultModel as ResultModel;

        if (!Resu.HasError) {
          let Course = Resu.Data as CourseModel;
          this.SetFields(Course);
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


  DeleteCourse(id: number) {

    let respuesta = confirm("Esta seguro que desea eliminar el Coursee?");

    if (respuesta)
      this.courseService.DeleteCourse(id).subscribe(
        ResultModel => {
          let Resu = ResultModel as ResultModel;

          if (!Resu.HasError) {
            this.ListAllCourses();
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

    this.form.controls['CourseId'].setValue("");
    this.form.controls['CourseName'].setValue("");
    this.form.controls['Description'].setValue("");
    this.form.controls['TeacherId'].setValue("");

  }

  GetFields() {

    let Field = new CourseModel();

    Field.CourseId = this.form.get("CourseId").value;
    Field.CourseName = this.form.get("CourseName").value;
    Field.Description = this.form.get("Description").value;
    Field.TeacherId = this.form.get("TeacherId").value;

    return Field;

  }

  SetFields(Course: CourseModel) {

    this.form.controls['CourseId'].setValue(Course.CourseId);
    this.form.controls['CourseName'].setValue(Course.CourseName);
    this.form.controls['Description'].setValue(Course.Description);
    this.form.controls['TeacherId'].setValue(Course.TeacherId);
  }


  public GetSelecTeachers() {
    this.teacherService.GetAllTeachers().subscribe(
      ResultModel => {
        let Resu = ResultModel as unknown as ResultModel;

        if (!Resu.HasError) {

          this.ListTeachers = Resu.Data.map(s => {
            return {
              Value: s.TeacherId,
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


}  
