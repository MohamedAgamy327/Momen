import { Component, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA, MatSnackBar } from '@angular/material';
import { RepositoryService } from 'src/app/core/services';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-category-edit-dialog',
  templateUrl: './category-edit-dialog.component.html',
  styleUrls: ['./category-edit-dialog.component.css']
})

export class CategoryEditDialogComponent {

  editForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    public dialogRef: MatDialogRef<CategoryEditDialogComponent>,
    @Inject(MAT_DIALOG_DATA) private data: any,
    private repository: RepositoryService,
    private toastrService: ToastrService
  ) {
    this.createForm();
  }

  createForm() {
    this.editForm = this.formBuilder.group({
      id: [this.data.id],
      name: [this.data.name, Validators.required],
      description: [this.data.description]
    });
  }

  public errorHandling = (control: string, error: string) => {
    return this.editForm.controls[control].hasError(error);
  }

  update() {
    this.repository.put(`categories/${this.data.id}`, this.editForm.value).subscribe(
      (res: any) => {
        this.toastrService.success('Edited Successfully', 'Edit');
        this.dialogRef.close(res);
      });
  }

}
