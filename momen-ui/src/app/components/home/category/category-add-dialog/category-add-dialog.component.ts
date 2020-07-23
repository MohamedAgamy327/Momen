import { Component, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { RepositoryService } from 'src/app/core/services';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-category-add-dialog',
  templateUrl: './category-add-dialog.component.html',
  styleUrls: ['./category-add-dialog.component.css']
})

export class CategoryAddDialogComponent {

  addForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    public dialogRef: MatDialogRef<CategoryAddDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private repository: RepositoryService,
    private toastrService: ToastrService
  ) {
    this.createForm();
  }

  createForm() {
    this.addForm = this.formBuilder.group({
      name: ['', Validators.required],
      description: ['']
    }
    );
  }

  public errorHandling = (control: string, error: string) => {
    return this.addForm.controls[control].hasError(error);
  }

  save() {
    this.repository.post('categories', this.addForm.value).subscribe(
      (res: any) => {
        this.toastrService.success('Added Successfully', 'Add');
        this.dialogRef.close(res);
      });
  }



}
