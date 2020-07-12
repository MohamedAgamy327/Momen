import { ChildrenItems } from './menu-item-children.model';

export interface MenuItem {
  state?: string;
  name: string;
  type: string;
  icon: string;
  children?: ChildrenItems[];
}
