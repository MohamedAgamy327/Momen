import { Component, Inject, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { FileValidationService } from 'src/app/core/services';
import { ToastrService } from 'ngx-toastr';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ContractService } from 'src/app/core/services/api/contract.service';

@Component({
  selector: 'app-contract-add-dialog',
  templateUrl: './contract-add-dialog.component.html',
  styleUrls: ['./contract-add-dialog.component.css']
})

export class ContractAddDialogComponent {

  addForm: FormGroup;
  @ViewChild('pdfInput') pdfInput;

  constructor(
    private fileValidationService: FileValidationService,
    private formBuilder: FormBuilder,
    public dialogRef: MatDialogRef<ContractAddDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private contractService: ContractService,
    private toastrService: ToastrService
  ) {
    this.createForm();
  }

  createForm() {
    this.addForm = this.formBuilder.group({
      name: ['', Validators.required],
      description: [''],
      pdf: ['', Validators.required],
      pdfSource: ['']
    }
    );
  }

  public errorHandling = (control: string, error: string) => {
    return this.addForm.controls[control].hasError(error);
  }

  uploadPdf(event: any) {
    if (this.fileValidationService.checkInvalidPDF(event.target.files[0])) {
      this.addForm.patchValue({ pdf: '' });
      this.toastrService.error('Invalid PDF', 'Error');
    } else {
      this.addForm.patchValue({ pdfSource: event.target.files[0] });
    }
  }

  clearPdfInput($event: any) {
    this.pdfInput.clear($event);
    this.addForm.patchValue({ pdf: '' });
  }

  save() {
    this.contractService.create(this.addForm.value).subscribe(
      (res: any) => {
        this.toastrService.success('Added Successfully', 'Add');
        this.uploadFile(res.id);
      });
  }

  uploadFile(id: string) {
    const formData = new FormData();
    formData.append('id', id);
    formData.append('file', this.addForm.value.pdfSource, this.addForm.value.pdfSource.name);
    this.contractService.uploadFile(id, formData).subscribe(
      (res: any) => {
        this.toastrService.success('PDF File uploaded successfully', 'upload');
        this.dialogRef.close(res);
      });
  }

}
