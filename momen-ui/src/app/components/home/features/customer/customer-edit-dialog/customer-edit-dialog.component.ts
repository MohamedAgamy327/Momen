import { Component, Inject, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { CustomerService, FileValidationService } from 'src/app/core/services';

@Component({
  selector: 'app-customer-edit-dialog',
  templateUrl: './customer-edit-dialog.component.html',
  styleUrls: ['./customer-edit-dialog.component.css']
})

export class CustomerEditDialogComponent {

  editForm: FormGroup;
  @ViewChild('pictureInput') pictureInput: any;

  constructor(
    private fileValidationService: FileValidationService,
    private formBuilder: FormBuilder,
    public dialogRef: MatDialogRef<CustomerEditDialogComponent>,
    @Inject(MAT_DIALOG_DATA) private data: any,
    private customerService: CustomerService,
    private toastrService: ToastrService
  ) {
    this.createForm();
  }

  createForm() {
    this.editForm = this.formBuilder.group({
      id: [this.data.id],
      name: [this.data.name, Validators.required],
      email: [this.data.email, [Validators.required, Validators.email]],
      phone: [this.data.phone, [Validators.required, Validators.pattern('^[0-9]*$')]],
      picture: [''],
      pictureSource: ['']
    });
  }

  public errorHandling = (control: string, error: string) => {
    return this.editForm.controls[control].hasError(error);
  }

  uploadPicture(event: any) {
    if (this.fileValidationService.checkInvalidImage(event.target.files[0])) {
      this.editForm.patchValue({ picture: '' });
      this.toastrService.error('Invalid Picture', 'Error');
    } else {
      this.editForm.patchValue({ pictureSource: event.target.files[0] });
    }
  }

  clearPictureInput($event: any) {
    this.pictureInput.clear($event);
    this.editForm.patchValue({ picture: '' });
  }

  update() {
    this.customerService.edit(this.data.id, this.editForm.value).subscribe(
      (res: any) => {
        this.toastrService.success('Edited Successfully', 'Edit');
        if (this.editForm.value.picture) {
          this.uploadFile(res.id);
        } else {
          this.dialogRef.close(res);
        }
      });
  }

  uploadFile(id: any) {
    const formData = new FormData();
    formData.append('id', id);
    formData.append('picture', this.editForm.value.pictureSource, this.editForm.value.pictureSource.name);
    this.customerService.uploadFile(Number(id), formData).subscribe(
      (res: any) => {
        this.toastrService.success('PDF File uploaded successfully', 'upload');
        this.dialogRef.close(res);
      });
  }
}
