import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { SelectModel } from '../../../Models/SelectModel';
import { StudentModel } from '../../../Models/StudentModel';
import { ResultModel } from '../../../Models/ResultModel';
import { StudentService } from '../../../Services/Student/studet.service';

@Component({
  selector: 'app-students',
  templateUrl: './students.component.html',
  styleUrls: ['./students.component.css']
})
export class StudentsComponent implements OnInit {


  constructor(private formBuilder: FormBuilder, private studentService: StudentService) { }

  form: FormGroup;
  Action = "Registro";

  ListBooks: SelectModel[];
  List: StudentModel[] = [];

  showModal = false;


  ngOnInit(): void {

    this.ListAllStudents();

    this.form = this.formBuilder.group(
      {

        StudentId: '',
        IdentificationNumber: '',
        FirstName: '',
        LastName: '',
        BirthDate: '',
        Email: '',

      }
    );

  }

  SaveChanges() {

    if (this.Action == "Registro") {
      this.SaveStudent();
    }

    if (this.Action == "Modificacion") {
      this.UpdateStudent();
    }

  }

  SaveStudent() {

    let Fields = this.GetFields();
    this.studentService.SaveStudent(Fields).subscribe(
      ResultModel => {
        let Resu = ResultModel as ResultModel;
        if (!Resu.HasError) {
          alert(Resu.Messages);
          this.ListAllStudents();
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

  ViewStudent(id: number) {
    this.ShowModal(true, "Modificacion");
    this.GetStudentByStudentId(id);
  }

  UpdateStudent() {

    let Fields = this.GetFields();

    this.studentService.UpdateStudent(Fields).subscribe(
      ResultModel => {

        let Resu = ResultModel as ResultModel;
        if (!Resu.HasError) {

          alert(Resu.Messages);
          this.ListAllStudents();
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

  ListAllStudents() {

    this.studentService.GetAllStudents().subscribe(

      ResultModel => {

        let Resu = ResultModel as ResultModel;

        if (!Resu.HasError) {

          let Array = Resu.Data as StudentModel[];

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

  GetStudentByStudentId(id: number) {

    this.studentService.GetStudentByStudentId(id).subscribe(

      ResultModel => {
        let Resu = ResultModel as ResultModel;

        if (!Resu.HasError) {
          let Student = Resu.Data as StudentModel;
          this.SetFields(Student);
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


  DeleteStudent(id: number) {

    let respuesta = confirm("Esta seguro que desea eliminar el Studente?");

    if (respuesta)
      this.studentService.DeleteStudent(id).subscribe(
        ResultModel => {
          let Resu = ResultModel as ResultModel;

          if (!Resu.HasError) {
            this.ListAllStudents();
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

    this.form.controls['StudentId'].setValue("");
    this.form.controls['IdentificationNumber'].setValue("");
    this.form.controls['FirstName'].setValue("");
    this.form.controls['LastName'].setValue("");
    this.form.controls['BirthDate'].setValue("");
    this.form.controls['Email'].setValue("");

  }

  GetFields() {

    let Field = new StudentModel();

    Field.StudentId = this.form.get("StudentId").value;
    Field.IdentificationNumber = this.form.get("IdentificationNumber").value;
    Field.FirstName = this.form.get("FirstName").value;
    Field.LastName = this.form.get("LastName").value;
    Field.BirthDate = this.form.get("BirthDate").value;
    Field.Email = this.form.get("Email").value;

    return Field;

  }

  SetFields(Student: StudentModel) {

    this.form.controls['StudentId'].setValue(Student.StudentId);
    this.form.controls['IdentificationNumber'].setValue(Student.IdentificationNumber);
    this.form.controls['FirstName'].setValue(Student.FirstName);
    this.form.controls['LastName'].setValue(Student.LastName);
    this.form.controls['BirthDate'].setValue(Student.BirthDate);
    this.form.controls['Email'].setValue(Student.Email);
  }

}  
