import { Component, OnInit, Input } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Router } from '@angular/router';
import { CoreService } from 'src/app/core/services';

@Component({
  selector: 'app-side-bar',
  templateUrl: './side-bar.component.html',
  styleUrls: ['./side-bar.component.css']
})

export class SideBarComponent implements OnInit {

  @Input() menuList: any;
  @Input() verticalMenuStatus: boolean;

  constructor(
    public translate: TranslateService,
    private router: Router,
    public coreService: CoreService
    ) { }

  ngOnInit() {
  }

}
