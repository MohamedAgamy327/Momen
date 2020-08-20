
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})

export class FileValidationService {

  checkInvalidFilesTypes(files: any) {
    const invalidTypes = [...files].some(file => !file.type.startsWith('image/'));
    return invalidTypes;
  }

  checkInvalidPDF(file: any) {
    const invalidTypes = !file.type.includes('application/pdf');
    return invalidTypes;
  }

  checkInvalidImages(files: any) {
    const invalidTypes = [...files].some(file => !file.type.includes('image/'));
    return invalidTypes;
  }

  checkInvalidImage(file: any) {
    const invalidTypes = !file.type.includes('image/');
    return invalidTypes;
  }

}
