import { MenuItem } from '../models';

export const MenuItemList: MenuItem[] = [
  {
    state: 'home/categories',
    name: 'Category List',
    type: 'link',
    icon: 'category'
  },
  {
    state: 'home/contracts',
    name: 'Contract List',
    type: 'link',
    icon: 'assignment'
  }
  ,
  {
    state: 'home/vendors',
    name: 'Vendor List',
    type: 'link',
    icon: 'admin_panel_settings'
  }
];
