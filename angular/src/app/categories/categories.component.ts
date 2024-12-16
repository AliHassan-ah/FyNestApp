import { Component, Injector, ChangeDetectorRef } from "@angular/core";
import { BsModalService, BsModalRef } from "ngx-bootstrap/modal";
import { Router } from "@angular/router";
import { finalize } from "rxjs/operators";
import {
  CategoryServiceProxy,
  CategoryDto,
} from "@shared/service-proxies/service-proxies";
import {
  PagedListingComponentBase,
  PagedRequestDto,
} from "@shared/paged-listing-component-base";
@Component({
  selector: "app-categories",
  templateUrl: "./categories.component.html",
  styleUrl: "./categories.component.css",
  standalone: false,
})
export class CategoriesComponent extends PagedListingComponentBase<CategoryDto> {
  protected list(
    request: PagedRequestDto,
    pageNumber: number,
    finishedCallback: Function
  ): void {
    throw new Error("Method not implemented.");
  }
  protected delete(entity: CategoryDto): void {
    throw new Error("Method not implemented.");
  }

  searchKeyword: string = "";
  first: number = 0;
  rows: number = 10;
  allCategories: CategoryDto[] = [];

  stateOptions: any[] = [
    { label: "All", value: "all" },
    { label: "Published", value: "published" },
    { label: "Low Stock", value: "lowStock" },
    { label: "Draft", value: "draft" },
  ];
  value: string = "off";
  constructor(
    injector: Injector,
    private _modalService: BsModalService,
    private router: Router,
    private _categoryService: CategoryServiceProxy,
    public cd: ChangeDetectorRef
  ) {
    super(injector, cd);
  }
  ngOnInit() {
    // this.getOrderDetails();
    this.getAllCategories();
  }
  onStateChange(event: any) {
    // if (event.value === "login") {
    //   this.router.navigate(["/account/login"]); // Navigate to login page
    // } else if (event.value === "register") {
    //   this.router.navigate(["/account/register"]); // Navigate to register page
    // }
  }
  createCategoryNavigation(): void {
    this.router.navigate(["/app/create-category"]);
  }
  editCategoryNavigate(id: number) {
    this.router.navigate(["/app/edit-category"], { queryParams: { id } });
  }
  getAllCategories(searchKeyWord?: string) {
    this._categoryService
      .getAllCategories(searchKeyWord, "", this.first, this.rows)
      .subscribe((response: any) => {
        this.allCategories = response.items;
        console.log("res", response);
        this.cd.detectChanges();
      });
  }
  showCategoryDetailPage(id: number) {
    this.router.navigate(["/app/view-category"], { queryParams: { id } });
  }
  deleteCategory(category: CategoryDto): void {
    abp.message.confirm(
      this.l("RoleDeleteWarningMessage", category.categoryName),
      undefined,
      (result: boolean) => {
        if (result) {
          this._categoryService
            .deleteCategory(category.id)
            .pipe(
              finalize(() => {
                abp.notify.success(this.l("SuccessfullyDeleted"));

                this.getAllCategories();
              })
            )
            .subscribe(() => {});
        }
      }
    );
  }
  formatCreationTime(date: string): string {
    const options: Intl.DateTimeFormatOptions = {
      year: "numeric",
      month: "short",
      day: "2-digit",
    };
    return new Date(date).toLocaleDateString("en-US", options);
  }
}
