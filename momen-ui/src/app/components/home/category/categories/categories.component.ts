import { RepositoryService, PageTitleService } from 'src/app/core/services';
import { Category } from 'src/app/core/models';
import { CategoryEditDialogComponent } from './../category-edit-dialog/category-edit-dialog.component';
import { CategoryAddDialogComponent } from '../category-add-dialog/category-add-dialog.component';
import { Component, OnInit, ViewChild, ViewEncapsulation } from '@angular/core';
import { MatTableDataSource, MatPaginator, MatSort, MatSnackBar, MatDialog } from '@angular/material';
import { DeleteDialogComponent } from '../../delete-dialog/delete-dialog.component';

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
    private repository: RepositoryService,
    private dialog: MatDialog
  ) { }

  ngOnInit() {
    this.pageTitleService.setTitle('Categories');
    this.getCategories();
  }

  getCategories() {
    this.repository.get('categories').subscribe(
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

  delete(category) {
    const dialogRef = this.dialog.open(DeleteDialogComponent, {
      data: { id: category.id, controller: 'categories', type: 'this category' }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        const index = this.categories.findIndex(f => f.id === result.id);
        this.categories.splice(index, 1);
        this.refreshData();
      }
    });
  }

}
