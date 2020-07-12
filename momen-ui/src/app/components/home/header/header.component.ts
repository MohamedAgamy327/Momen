import { BreadcrumbService } from 'ng5-breadcrumb';
import { Component, OnInit } from '@angular/core';
import { CoreService, CredentialService } from 'src/app/core/services';
import { PageTitleService } from 'src/app/core/services/page-title.service';
import { MatDialog } from '@angular/material';
declare var require;
const screenfull = require('screenfull');

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})

export class HeaderComponent implements OnInit {

  isFullscreen = false;
  header: string;

  constructor(
    private coreService: CoreService,
    private pageTitleService: PageTitleService,
    public credentialService: CredentialService,
    private breadcrumbService: BreadcrumbService,
    private dialog: MatDialog
  ) {
    this.addBreadcrumb();
  }

  addBreadcrumb() {
    this.breadcrumbService.addFriendlyNameForRoute('/home', 'Home');
    this.breadcrumbService.addFriendlyNameForRoute('/home/patients', 'Patients');
    this.breadcrumbService.addFriendlyNameForRoute('/home/occupations', 'Occupations');
    this.breadcrumbService.addFriendlyNameForRoute('/home/know-through-by', 'Know Through By');
    this.breadcrumbService.addFriendlyNameForRoute('/home/medicines', 'Medicines');
    this.breadcrumbService.addFriendlyNameForRoute('/home/medicines-templates', 'Medicines Templates ');
    this.breadcrumbService.addFriendlyNameForRoute('/home/medicine-types', 'Medicines Types ');
    this.breadcrumbService.addFriendlyNameForRoute('/home/frequencies', 'Frequencies');
    this.breadcrumbService.addFriendlyNameForRoute('/home/instructions', 'Instructions');
    this.breadcrumbService.addFriendlyNameForRoute('/home/instructions-templates', 'Instructions Templates');
    this.breadcrumbService.addFriendlyNameForRoute('/home/investigations', 'Investigations');
    this.breadcrumbService.addFriendlyNameForRoute('/home/users', 'Users');
  }

  ngOnInit(): void {
    this.pageTitleService.title.subscribe((val: string) => {
      this.header = val;
    });
  }

  toggleSidebar() {
    this.coreService.sidenavOpen = !this.coreService.sidenavOpen;
  }

  toggleFullscreen() {
    screenfull.toggle();
    this.isFullscreen = !this.isFullscreen;
  }

  logout() {
    this.credentialService.logout();
  }

}
