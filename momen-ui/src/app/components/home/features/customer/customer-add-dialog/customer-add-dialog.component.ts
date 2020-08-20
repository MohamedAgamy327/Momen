import { Component, Inject, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { CustomerService, FileValidationService } from 'src/app/core/services';

@Component({
  selector: 'app-customer-add-dialog',
  templateUrl: './customer-add-dialog.component.html',
  styleUrls: ['./customer-add-dialog.component.css']
})

export class CustomerAddDialogComponent {

  addForm: FormGroup;
  @ViewChild('pictureInput') pictureInput: any;

  constructor(
    private fileValidationService: FileValidationService,
    private formBuilder: FormBuilder,
    public dialogRef: MatDialogRef<CustomerAddDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private customerService: CustomerService,
    private toastrService: ToastrService,
  ) {
    this.createForm();
  }

  createForm() {
    this.addForm = this.formBuilder.group({
      name: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', Validators.required],
      picture: ['', Validators.required],
      pictureSource: ['']
    });
  }

  public errorHandling = (control: string, error: string) => {
    return this.addForm.controls[control].hasError(error);
  }

  uploadPicture(event: any) {
    if (this.fileValidationService.checkInvalidImage(event.target.files[0])) {
      this.addForm.patchValue({ picture: '' });
      this.toastrService.error('Invalid Picture', 'Error');
    } else {
      this.addForm.patchValue({ pictureSource: event.target.files[0] });
    }
  }

  clearPictureInput($event: any) {
    this.pictureInput.clear($event);
    this.addForm.patchValue({ picture: '' });
  }

  save() {
    this.customerService.create(this.addForm.value).subscribe(
      (res: any) => {
        this.toastrService.success('Added Successfully', 'Add');
        this.uploadFile(res.id);
      });
  }

  uploadFile(id: any) {
    const formData = new FormData();
    formData.append('id', id);
    formData.append('picture', this.addForm.value.pictureSource, this.addForm.value.pictureSource.name);
    this.customerService.uploadFile(Number(id), formData).subscribe(
      (res: any) => {
        this.toastrService.success('PDF File uploaded successfully', 'upload');
        this.dialogRef.close(res);
      });
  }

}
