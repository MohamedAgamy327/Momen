import { Component, Inject, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';

import { RepositoryService, FileValidationService } from 'src/app/core/services';
import { ToastrService } from 'ngx-toastr';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-contract-edit-dialog',
  templateUrl: './contract-edit-dialog.component.html',
  styleUrls: ['./contract-edit-dialog.component.css']
})

export class ContractEditDialogComponent {

  editForm: FormGroup;
  public pdfCtrl: FormControl = new FormControl();
  @ViewChild('pdfInput') pdfInput;
  pdfFormData: FormData;

  constructor(
    private fileValidationService: FileValidationService,
    private formBuilder: FormBuilder,
    public dialogRef: MatDialogRef<ContractEditDialogComponent>,
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

  uploadPdf(event) {
    this.pdfFormData = new FormData();
    if (this.fileValidationService.checkInvalidPDF(event.target.files[0])) {
      this.pdfCtrl.setValue('');
      this.toastrService.error('Invalid PDF', 'Error');
    } else {
      const file = event.target.files[0];
      this.pdfFormData.append('file', file, file.name);
    }
  }

  clearPdfInput($event) {
    this.pdfInput.clear($event);
    this.pdfCtrl.setValue('');
  }

  update() {
    this.repository.put(`contracts/${this.data.id}`, this.editForm.value).subscribe(
      (res: any) => {
        if (this.pdfFormData) {
          this.patchPdf(res.id);
        } else {
          this.dialogRef.close(res);
        }
        this.toastrService.success('Edited Successfully', 'Edit');
      });
  }

  patchPdf(id) {
    this.pdfFormData.append('id', id);
    this.repository.patch(`contracts/${id}/file`, this.pdfFormData).subscribe(
      (res: any) => {
        this.toastrService.success('PDF File Uploaded Successfully', 'Upload');
        this.dialogRef.close(res);
      });
  }

}
