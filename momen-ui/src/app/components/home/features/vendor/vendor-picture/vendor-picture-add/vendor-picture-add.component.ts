import { Component, OnInit } from '@angular/core';
import { PageTitleService, VendorPictureService, FileValidationService } from 'src/app/core/services';
import { ActivatedRoute } from '@angular/router';
import { FileUploader } from 'ng2-file-upload';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-vendor-picture-add',
  templateUrl: './vendor-picture-add.component.html',
  styleUrls: ['./vendor-picture-add.component.css']
})
export class VendorPictureAddComponent implements OnInit {

  uploader: FileUploader = new FileUploader({});
  hasBaseDropZoneOver = false;
  hasAnotherDropZoneOver = false;
  vendorPictureFormData: FormData;

  constructor(
    private pageTitleService: PageTitleService,
    public route: ActivatedRoute,
    private vendorPictureService: VendorPictureService,
    private fileValidationService: FileValidationService,
    private toastrService: ToastrService
  ) { }

  ngOnInit(): void {
    this.pageTitleService.setTitle('Vendor Picture  Add');
  }

  fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }

  fileOverAnother(e: any): void {
    this.hasAnotherDropZoneOver = e;
  }

  uploadVendorPicture() {
    this.vendorPictureFormData = new FormData();
    if (this.fileValidationService.checkInvalidImages(this.uploader.queue.map(m => m.file.rawFile))) {
      this.toastrService.error('Invalid images', 'Error');
      return;
    } else {
      const fileList: any = this.uploader.queue.map(m => m.file.rawFile);
      for (const file of fileList.length) {
        this.vendorPictureFormData.append('pictures', file, file.name);
      }
      this.postVendorPicture(this.route.snapshot.params.vendorId);
    }
  }

  postVendorPicture(vendorId: number) {
    this.vendorPictureFormData.append('vendorId', String(vendorId));
    this.vendorPictureService.create(this.vendorPictureFormData).subscribe(
      (res: any) => {
        this.uploader = new FileUploader({});
        this.toastrService.success('Vendor Pictures  Uploaded Successfully', 'Upload');
      });
  }

}
