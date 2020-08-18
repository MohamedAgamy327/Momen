import { Component, Inject } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { VendorUserService } from 'src/app/core/services';

@Component({
  selector: 'app-vendor-user-edit-dialog',
  templateUrl: './vendor-user-edit-dialog.component.html',
  styleUrls: ['./vendor-user-edit-dialog.component.css']
})
export class VendorUserEditDialogComponent {

  editForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    public dialogRef: MatDialogRef<VendorUserEditDialogComponent>,
    @Inject(MAT_DIALOG_DATA) private data: any,
    private vendorUserService: VendorUserService,
    private toastrService: ToastrService
  ) {
    this.createForm();
  }

  createForm() {
    this.editForm = this.formBuilder.group({
      id: [this.data.id],
      name: [this.data.name, Validators.required],
      email: [this.data.email, [Validators.required, Validators.email]],
      phone: [this.data.phone, Validators.required],
      role: [this.data.role, Validators.required]
    });
  }

  public errorHandling = (control: string, error: string) => {
    return this.editForm.controls[control].hasError(error);
  }

  update() {
    this.vendorUserService.edit(this.data.id, this.editForm.value).subscribe(
      (res: any) => {
        this.toastrService.success('Edited Successfully', 'Edit');
        this.dialogRef.close(res);
      });
  }
}
