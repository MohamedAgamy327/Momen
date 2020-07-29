import { Component, Inject } from '@angular/core';
import { RepositoryService } from 'src/app/core/services';
import { ToastrService } from 'ngx-toastr';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-delete-dialog',
  templateUrl: './delete-dialog.component.html',
  styleUrls: ['./delete-dialog.component.css']
})

export class DeleteDialogComponent {

  constructor(
    public dialogRef: MatDialogRef<DeleteDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
    private repository: RepositoryService,
    private toastrService: ToastrService
  ) { }

  confirmDelete(): void {
    this.repository.delete(`${this.data.controller}/${this.data.id}`).subscribe(
      (res: any) => {
        this.toastrService.success('Deleted Successfully', 'Delete');
        this.dialogRef.close(res);
      });
  }

}
