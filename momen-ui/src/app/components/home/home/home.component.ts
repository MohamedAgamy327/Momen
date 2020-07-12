import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { CredentialService, CoreService } from 'src/app/core/services';
import { MatDialog } from '@angular/material';
import { TourService } from 'ngx-tour-md-menu';
import { TranslateService } from '@ngx-translate/core';
import { SideBarList, HeaderList, MenuItemList } from 'src/app/core/lists';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'],
  encapsulation: ViewEncapsulation.None,
  // tslint:disable-next-line: no-host-metadata-property
  host: { '(window:resize)': 'onResize($event)' }
})

export class HomeComponent implements OnInit {

  layout = 'ltr';
  dark: boolean;
  compactSidebar: boolean;
  customizerIn = false;
  currentLang = 'en';
  windowSize: number;
  isMobileStatus: boolean;
  isMobile = false;

  sideBarFilterClass: any = SideBarList;
  headerFilterClass: any = HeaderList;
  menuItems: any = MenuItemList;


  constructor(
    public translate: TranslateService,
    public coreService: CoreService,
    public credentialService: CredentialService,
    private dialog: MatDialog,
    public tourService: TourService
  ) {
    this.initializeTour();
  }

  initializeTour() {
    this.tourService.initialize([{
      anchorId: 'start.tour',
      content: 'Welcome to Gene admin panel!',
      placement: 'below',
      title: 'Welcome to Gene',
    },
    {
      anchorId: 'tour-full-screen',
      content: 'By pressing this button you can switch to fullscreen mode.',
      placement: 'below',
      title: 'Full Screen',
    },
    {
      anchorId: 'tour-ui',
      content: 'Show your site stats with unique designed cards',
      placement: 'below',
      title: 'Stats Cards',
    }]);
    if (window.innerWidth > 1199) {
      this.tourService.start();
    }
  }

  ngOnInit(): void {
  }

  onResize(event) {
    this.windowSize = event.target.innerWidth;
    this.resizeSideBar();
  }

  resizeSideBar() {
    if (this.windowSize < 1200) {
      this.isMobileStatus = true;
      this.isMobile = this.isMobileStatus;
      this.coreService.sidenavMode = 'over';
      this.coreService.sidenavOpen = false;
      const mainDiv = document.getElementsByClassName('app');

      // tslint:disable-next-line: prefer-for-of
      for (let i = 0; i < mainDiv.length; i++) {
        if (!(mainDiv[i].classList.contains('sidebar-overlay'))) {
          if (document.getElementById('main-app')) {
            document.getElementById('main-app').className += ' sidebar-overlay';
          }
        }
      }
    } else {
      this.isMobileStatus = false;
      this.isMobile = this.isMobileStatus;
      this.coreService.sidenavMode = 'side';
      this.coreService.sidenavOpen = true;
      const mainDiv = document.getElementsByClassName('app');
      // tslint:disable-next-line: prefer-for-of
      for (let i = 0; i < mainDiv.length; i++) {
        if (mainDiv[i].classList.contains('sidebar-overlay')) {
          document.getElementById('main-app').classList.remove('sidebar-overlay');
        }
      }
    }
  }

  customizerFunction() {
    this.customizerIn = !this.customizerIn;
  }

  collapseSidebar(event) {
    if (event.checked) {
      this.coreService.collapseSidebar = true;
    } else {
      this.coreService.collapseSidebar = false;
    }
  }

  sidebarFilter(selectedFilter) {
    // tslint:disable-next-line: prefer-for-of
    for (let i = 0; i < this.sideBarFilterClass.length; i++) {
      document.getElementById('main-app').classList.remove(this.sideBarFilterClass[i].colorSelect);
      if (this.sideBarFilterClass[i].colorSelect === selectedFilter.colorSelect) {
        document.getElementById('main-app').classList.add(this.sideBarFilterClass[i].colorSelect);
      }
    }
    document.querySelector('.radius-circle').classList.remove('radius-circle');
    document.getElementById(selectedFilter.sideBarSelect).classList.add('radius-circle');
  }

  headerFilter(selectedFilter) {
    // tslint:disable-next-line: prefer-for-of
    for (let i = 0; i < this.headerFilterClass.length; i++) {
      document.getElementById('main-app').classList.remove(this.headerFilterClass[i].colorSelect);
      if (this.headerFilterClass[i].colorSelect === selectedFilter.colorSelect) {
        document.getElementById('main-app').classList.add(this.headerFilterClass[i].colorSelect);
      }
    }
    document.querySelector('.radius-active').classList.remove('radius-active');
    document.getElementById(selectedFilter.headerSelect).classList.add('radius-active');
  }

  addClassOnBody(event) {
    const body = document.body;
    if (event.checked) {
      body.classList.add('dark-theme-active');
    } else {
      body.classList.remove('dark-theme-active');
    }
  }

  changeRTL(isChecked) {
    if (isChecked) {
      this.layout = 'rtl';
    } else {
      this.layout = 'ltr';
    }
  }

  onActivate(e, scrollContainer) {
    scrollContainer.scrollTop = 0;
  }
}
