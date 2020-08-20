import { Component } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { LanguageFlagList } from 'src/app/core/lists';

@Component({
  selector: 'app-language-drop-down',
  templateUrl: './language-drop-down.component.html',
  styleUrls: ['./language-drop-down.component.css']
})
export class LanguageDropDownComponent {

  currentLang = 'en';
  selectImage = './assets/img/en.png';

  langArray: any[] = LanguageFlagList;

  constructor(
    public translate: TranslateService
  ) {

  }

  setLang(lang: any) {
    for (const data of this.langArray) {
      if (data.value === lang) {
        this.selectImage = data.img;
        break;
      }
    }
    this.translate.use(lang);
  }

}
