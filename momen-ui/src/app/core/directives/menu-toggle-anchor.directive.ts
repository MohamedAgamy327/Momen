import { Directive, HostListener, Inject } from '@angular/core';

import { MenuToggleLinkDirective } from './menu-toggle-link.directive';

@Directive({
  // tslint:disable-next-line: directive-selector
  selector: '[menuToggle]'
})
export class MenuToggleAnchorDirective {

  protected navlink: MenuToggleLinkDirective;

  constructor( @Inject(MenuToggleLinkDirective) navlink: MenuToggleLinkDirective) {
    this.navlink = navlink;
  }

  @HostListener('click', ['$event'])
  onClick(e: any) {
    this.navlink.toggle();
  }
}
