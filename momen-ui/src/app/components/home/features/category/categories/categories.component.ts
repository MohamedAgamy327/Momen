import { PageTitleService, CategoryService } from 'src/app/core/services';
import { Category } from 'src/app/core/models';
import { CategoryEditDialogComponent } from '../category-edit-dialog/category-edit-dialog.component';
import { CategoryAddDialogComponent } from '../category-add-dialog/category-add-dialog.component';
import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatSort } from '@angular/material/sort';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { MatDialog } from '@angular/material/dialog';
import { DeleteDialogComponent } from 'src/app/components/home';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css'],
  encapsulation: ViewEncapsulation.None
})

export class CategoriesComponent implements OnInit {

  filter: string;
  @ViewChild(MatSort, { static: true }) sort: MatSort;
  @ViewChild(MatPaginator, { static: true }) paginator: MatPaginator;
  displayedColumns: string[] = ['name', 'description', 'action'];
  categories: Category[];
  dataSource = new MatTableDataSource<Category>();

  constructor(
    private pageTitleService: PageTitleService,
    private categoryService: CategoryService,
    private toastrService: ToastrService,
    private dialog: MatDialog
  ) { }

  ngOnInit() {
    this.pageTitleService.setTitle('Categories');
    this.getCategories();
  }

  getCategories() {
    this.categoryService.getAll().subscribe(
      (res: any) => {
        this.categories = res;
        this.refreshData();
      });
  }

  refreshData() {
    this.dataSource = new MatTableDataSource(this.categories);
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  clearFilter() {
    this.filter = '';
    this.applyFilter(this.filter);
  }

  add() {
    const dialogRef = this.dialog.open(CategoryAddDialogComponent, {
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.categories.unshift(result);
        this.refreshData();
      }
    });
  }

  edit(category) {
    const dialogRef = this.dialog.open(CategoryEditDialogComponent, {
      data: category
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        const index = this.categories.findIndex(f => f.id === result.id);
        this.categories[index] = result;
        this.refreshData();
      }
    });
  }

  showDelete(category) {
    const dialogRef = this.dialog.open(DeleteDialogComponent, {
      data: { type: 'this category' }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.delete(category.id);
      }
    });
  }

  delete(id) {
    this.categoryService.delete(id).subscribe(
      (res: any) => {
        this.toastrService.success('Deleted Successfully', 'Delete');
        const index = this.categories.findIndex(f => f.id === res.id);
        this.categories.splice(index, 1);
        this.refreshData();
      });
  }

}
