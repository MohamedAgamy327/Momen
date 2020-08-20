import { Component, Inject, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { FileValidationService, ContractService } from 'src/app/core/services';
import { ToastrService } from 'ngx-toastr';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-contract-edit-dialog',
  templateUrl: './contract-edit-dialog.component.html',
  styleUrls: ['./contract-edit-dialog.component.css']
})

export class ContractEditDialogComponent {

  editForm: FormGroup;
  @ViewChild('pdfInput') pdfInput: any;

  constructor(
    private fileValidationService: FileValidationService,
    private formBuilder: FormBuilder,
    public dialogRef: MatDialogRef<ContractEditDialogComponent>,
    @Inject(MAT_DIALOG_DATA) private data: any,
    private contractService: ContractService,
    private toastrService: ToastrService
  ) {
    this.createForm();
  }

  createForm() {
    this.editForm = this.formBuilder.group({
      id: [this.data.id],
      name: [this.data.name, Validators.required],
      description: [this.data.description],
      pdf: ['', Validators.required],
      pdfSource: ['']
    });
  }

  public errorHandling = (control: string, error: string) => {
    return this.editForm.controls[control].hasError(error);
  }

  uploadPdf(event: any) {
    if (this.fileValidationService.checkInvalidPDF(event.target.files[0])) {
      this.editForm.patchValue({ pdf: '' });
      this.toastrService.error('Invalid PDF', 'Error');
    } else {
      this.editForm.patchValue({ pdfSource: event.target.files[0] });
    }
  }

  clearPdfInput($event: any) {
    this.pdfInput.clear($event);
    this.editForm.patchValue({ pdf: '' });
  }

  update() {
    this.contractService.edit(this.data.id, this.editForm.value).subscribe(
      (res: any) => {
        if (this.editForm.value.pdf) {
          this.uploadFile(res.id);
        } else {
          this.dialogRef.close(res);
        }
        this.toastrService.success('Edited Successfully', 'Edit');
      });
  }

  uploadFile(id: any) {
    const formData = new FormData();
    formData.append('id', id);
    formData.append('pdf', this.editForm.value.pdfSource, this.editForm.value.pdfSource.name);
    this.contractService.uploadFile(id, formData).subscribe(
      (res: any) => {
        this.toastrService.success('PDF File Uploaded Successfully', 'Upload');
        this.dialogRef.close(res);
      });
  }

}
