
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})

export class FileValidationService {

  checkInvalidFilesTypes(files) {
    const invalidTypes = [...files].some(file => !file.type.startsWith('image/'));
    return invalidTypes;
  }

}
