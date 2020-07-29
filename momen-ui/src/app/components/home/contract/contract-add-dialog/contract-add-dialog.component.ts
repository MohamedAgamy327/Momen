import { Component, Inject, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { RepositoryService, FileValidationService } from 'src/app/core/services';

import { ToastrService } from 'ngx-toastr';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-contract-add-dialog',
  templateUrl: './contract-add-dialog.component.html',
  styleUrls: ['./contract-add-dialog.component.css']
})

export class ContractAddDialogComponent {

  addForm: FormGroup;
  public pdfCtrl: FormControl = new FormControl('', Validators.required);
  @ViewChild('pdfInput') pdfInput;
  pdfFormData: FormData;

  constructor(
    private fileValidationService: FileValidationService,
    private formBuilder: FormBuilder,
    public dialogRef: MatDialogRef<ContractAddDialogComponent>,
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

  save() {
    this.repository.post('contracts', this.addForm.value).subscribe(
      (res: any) => {
        this.toastrService.success('Added Successfully', 'Add');
        this.patchPdf(res.id);
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
