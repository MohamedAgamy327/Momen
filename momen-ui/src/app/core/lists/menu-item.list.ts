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
  },
  {
    name: 'Vendors',
    type: 'sub',
    icon: 'admin_panel_settings',
    children: [
      { state: 'home/vendors', name: 'Vendor List' },
      { state: 'home/vendors/add', name: 'Add Vendor' }
    ]
  },
  {
    state: 'home/customers',
    name: 'Customer List',
    type: 'link',
    icon: 'people_alt'
  }
];
