import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { CredentialService, CoreService } from 'src/app/core/services';
import { TourService } from 'ngx-tour-md-menu';
import { TranslateService } from '@ngx-translate/core';
import { SideBarList, HeaderList, MenuItemList } from 'src/app/core/lists';
import { MatDialog } from '@angular/material/dialog';

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
      for (const i in mainDiv) {
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
      for (const i in mainDiv) {
        if (mainDiv[i].classList.contains('sidebar-overlay')) {
          document.getElementById('main-app').classList.remove('sidebar-overlay');
        }
      }
    }
  }

  customizerFunction() {
    this.customizerIn = !this.customizerIn;
  }

  collapseSidebar(event: any) {
    if (event.checked) {
      this.coreService.collapseSidebar = true;
    } else {
      this.coreService.collapseSidebar = false;
    }
  }

  sidebarFilter(selectedFilter: any) {
    for (const sideClass of this.sideBarFilterClass.length) {
      document.getElementById('main-app').classList.remove(sideClass.colorSelect);
      if (sideClass.colorSelect === selectedFilter.colorSelect) {
        document.getElementById('main-app').classList.add(sideClass.colorSelect);
      }
    }
    document.querySelector('.radius-circle').classList.remove('radius-circle');
    document.getElementById(selectedFilter.sideBarSelect).classList.add('radius-circle');
  }

  headerFilter(selectedFilter: any) {
    for (const headerClass of this.headerFilterClass.length) {
      document.getElementById('main-app').classList.remove(headerClass.colorSelect);
      if (headerClass.colorSelect === selectedFilter.colorSelect) {
        document.getElementById('main-app').classList.add(headerClass.colorSelect);
      }
    }
    document.querySelector('.radius-active').classList.remove('radius-active');
    document.getElementById(selectedFilter.headerSelect).classList.add('radius-active');
  }

  addClassOnBody(event: any) {
    const body = document.body;
    if (event.checked) {
      body.classList.add('dark-theme-active');
    } else {
      body.classList.remove('dark-theme-active');
    }
  }

  changeRTL(isChecked: any) {
    if (isChecked) {
      this.layout = 'rtl';
    } else {
      this.layout = 'ltr';
    }
  }

  onActivate(e: any, scrollContainer: any) {
    scrollContainer.scrollTop = 0;
  }
}
