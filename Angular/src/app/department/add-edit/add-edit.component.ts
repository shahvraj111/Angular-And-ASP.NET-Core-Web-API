import { Component, OnInit,Input } from '@angular/core';
import {SharedService} from 'src/app/shared.service';

@Component({
  selector: 'app-add-edit',
  templateUrl: './add-edit.component.html',
  styleUrls: ['./add-edit.component.css']
})
export class AddEditComponent implements OnInit {

  constructor(private service:SharedService) { }

  @Input() dep:any;
  departmentid:string="";
  departmentname:string="";

  ngOnInit(): void {
    this.departmentid=this.dep.departmentid;
    this.departmentname=this.dep.departmentname;
  }

  addDepartment(){
    
    var val = {
              departmentname:this.departmentname
              };
    this.service.addDepartment(val).subscribe(res=>{
      alert(res.toString());
    });
  }

  updateDepartment(){
    var val = {
              departmentid:this.departmentid,
              departmentname:this.departmentname
              };
    this.service.updateDepartment(val).subscribe(res=>{
    alert(res.toString());
    });
  }
}
