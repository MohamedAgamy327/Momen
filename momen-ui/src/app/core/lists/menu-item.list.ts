import { MenuItem } from '../models';

export const MenuItemList: MenuItem[] = [
  {
    name: 'Patients',
    type: 'sub',
    icon: 'pregnant_woman',
    children: [
      { state: 'home/patients', name: 'Patient List' },
      { state: 'home/know-through-by', name: 'Know Through by List'},
      { state: 'home/occupations', name: 'Occupation List' }
    ]
  },
  {
    name: 'Medicines',
    type: 'sub',
    icon: 'opacity',
    children: [
      { state: 'home/medicines', name: 'Medicine List' },
      { state: 'home/medicines-templates', name: 'Template List' },
      { state: 'home/medicine-types', name: 'Type List' },
      { state: 'home/frequencies', name: 'Frequecny List' }
    ]
  },
  {
    name: 'Instructions',
    type: 'sub',
    icon: 'library_books',
    children: [
      { state: 'home/instructions', name: 'Instruction List' },
      { state: 'home/instructions-templates', name: 'Template List' }
    ]
  },
  {
    state: 'home/investigations',
    name: 'Investigation List',
    type: 'link',
    icon: 'check_box'
  },
  {
    state: 'home/users',
    name: 'User List',
    type: 'link',
    icon: 'supervisor_account'
  }
];
