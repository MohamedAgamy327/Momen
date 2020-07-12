import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'addspace'
})

export class AddSpacePipe implements PipeTransform {

  transform(val: string): string {
    return val.replace(/([A-Z])/g, ' $1').trim();
  }

}
