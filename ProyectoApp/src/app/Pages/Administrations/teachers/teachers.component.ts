import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { TeacherModel } from '../../../Models/TeacherModel';
import { SelectModel } from '../../../Models/SelectModel';
import { ResultModel } from '../../../Models/ResultModel';
import { TeachersService } from '../../../Services/Teacher/teachers.service';



@Component({
  selector: 'app-teachers',
  templateUrl: './teachers.component.html',
  styleUrls: ['./teachers.component.css']
})
export class TeachersComponent implements OnInit {
 

  constructor(private formBuilder: FormBuilder, private teachersService: TeachersService) { }

  form: FormGroup;
  Action = "Registro";

  ListBooks: SelectModel[];
  List: TeacherModel[] = [];

  showModal = false;


  ngOnInit(): void {
      
    this.ListAllTeachers();

    this.form = this.formBuilder.group(
      {
        TeacherId: '',
        FirstName: '',
        LastName: '',
        Specialty: '',
        Email: ''

      }
    );

  }

  SaveChanges() {

    if (this.Action == "Registro") {
      this.SaveTeacher();
    }

    if (this.Action == "Modificacion") {
      this.UpdateTeacher();
    }

  }

  SaveTeacher() {

    let Fields = this.GetFields();
    this.teachersService.SaveTeacher(Fields).subscribe(
      ResultModel => {
        let Resu = ResultModel as ResultModel;
        if (!Resu.HasError) {
          alert(Resu.Messages);
          this.ListAllTeachers();
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

  ViewTeacher(id: number) {
    this.ShowModal(true, "Modificacion");
    this.GetTeacherByTeacherId(id);
  }

  UpdateTeacher() {

    let Fields = this.GetFields();

    this.teachersService.UpdateTeacher(Fields).subscribe(
      ResultModel => {

        let Resu = ResultModel as ResultModel;
        if (!Resu.HasError) {

          alert(Resu.Messages);
          this.ListAllTeachers();
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

  ListAllTeachers() {

    this.teachersService.GetAllTeachers().subscribe(

      ResultModel => {

        let Resu = ResultModel as ResultModel;

        if (!Resu.HasError) {

          let Array = Resu.Data as TeacherModel[];

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

  GetTeacherByTeacherId(id: number) {
   
    this.teachersService.GetTeacherByTeacherId(id).subscribe(

      ResultModel => {
        let Resu = ResultModel as ResultModel;

        if (!Resu.HasError) {
          let Teacher = Resu.Data as TeacherModel;
          this.SetFields(Teacher);
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


  DeleteTeacher(id: number) {

    let respuesta = confirm("Esta seguro que desea eliminar el Teachere?");

    if (respuesta)
      this.teachersService.DeleteTeacher(id).subscribe(
        ResultModel => {
          let Resu = ResultModel as ResultModel;

          if (!Resu.HasError) {
            this.ListAllTeachers();
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

    this.form.controls['TeacherId'].setValue("");
    this.form.controls['FirstName'].setValue("");
    this.form.controls['LastName'].setValue("");
    this.form.controls['Specialty'].setValue("");
    this.form.controls['Email'].setValue("");
     
  }

  GetFields() {

    let Field = new TeacherModel();

    Field.TeacherId = this.form.get("TeacherId").value;
    Field.FirstName = this.form.get("FirstName").value;
    Field.LastName = this.form.get("LastName").value;
    Field.Specialty = this.form.get("Specialty").value;
    Field.Email = this.form.get("Email").value;
     
    return Field;

  }

  SetFields(Teacher: TeacherModel) {

    this.form.controls['TeacherId'].setValue(Teacher.TeacherId);
    this.form.controls['FirstName'].setValue(Teacher.FirstName);
    this.form.controls['LastName'].setValue(Teacher.LastName);
    this.form.controls['Specialty'].setValue(Teacher.Specialty);
    this.form.controls['Email'].setValue(Teacher.Email);
     
  }

}  
