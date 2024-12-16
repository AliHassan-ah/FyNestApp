import { Component, ChangeDetectorRef } from "@angular/core";
import { BsModalService, BsModalRef } from "ngx-bootstrap/modal";
import { CreateProductComponent } from "./create-product/create-product.component";
import { Router } from "@angular/router";
import {
  ProductServiceProxy,
  ProductWithDetailDto,
  CategoryServiceProxy,
  CategoryNameDto,
} from "@shared/service-proxies/service-proxies";
interface ProductWithCategory extends ProductWithDetailDto {
  categoryName: string; // Adding the categoryName property
}
@Component({
  selector: "app-products",
  standalone: false,
  templateUrl: "./products.component.html",
  styleUrl: "./products.component.css",
})
export class ProductsComponent {
  stateOptions: any[] = [
    { label: "All", value: "all" },
    { label: "Published", value: "published" },
    { label: "Low Stock", value: "lowStock" },
    { label: "Draft", value: "draft" },
  ];
  value: string = "off";
  // productsWithDetails: ProductWithDetailDto[];
  productsWithDetails: ProductWithCategory[];
  searchKeyword: string = "";
  first: number = 0;
  rows: number = 10;
  totalRecords: number = 0;
  categoryMap: { [key: number]: string } = {};
  constructor(
    private _modalService: BsModalService,
    private router: Router,
    private _productService: ProductServiceProxy,
    private _categoryService: CategoryServiceProxy,
    private cd: ChangeDetectorRef
  ) {}
  ngOnInit(): void {
    this.getAllProduct();
    this.getAllCategories();
  }

  getAllCategories() {
    this._categoryService
      .getAllCategoryNames()
      .subscribe((categories: CategoryNameDto[]) => {
        this.categoryMap = categories.reduce((map, category) => {
          map[category.id] = category.categoryName;
          return map;
        }, {} as { [key: number]: string });
      });
  }
  getAllProduct(searchKeyword?: string) {
    this._productService
      .getAllProductsWithDetails(searchKeyword, "", this.first, this.rows)
      .subscribe((response: any) => {
        console.log("Response", response);
        this.productsWithDetails = response.items.map(
          (product: ProductWithDetailDto) => {
            return {
              ...product,
              categoryName:
                this.categoryMap[product.categoryId] || "Unknown Category",
            } as ProductWithCategory;
          }
        );
        console.log("productD", this.productsWithDetails);
        this.totalRecords = response.totalCount;
        this.cd.detectChanges();
      });
  }
  searchProducts(): void {
    this.getAllProduct(this.searchKeyword);
  }
  onStateChange(event: any) {
    // if (event.value === "login") {
    //   this.router.navigate(["/account/login"]); // Navigate to login page
    // } else if (event.value === "register") {
    //   this.router.navigate(["/account/register"]); // Navigate to register page
    // }
  }
  createProduct(): void {
    // this.showCreateOrEditProductDialog();
    this.router.navigate(["/app/create-product"]);
  }
  editProduct(): void {
    this.showCreateOrEditProductDialog();
  }
  showCreateOrEditProductDialog(id?: number): void {
    let createOrEditProductDialog: BsModalRef;
    if (!id) {
      createOrEditProductDialog = this._modalService.show(
        CreateProductComponent,
        {
          class: "modal-lg",
        }
      );
    } else {
      // createOrEditProductDialog = this._modalService.show(
      //   EditProductDialogComponent,
      //   {
      //     class: "modal-lg",
      //     initialState: {
      //       id: id,
      //     },
      //   }
      // );
    }
    createOrEditProductDialog.content.onSave.subscribe(() => {
      // this.getProducts();
    });

    createOrEditProductDialog.content.onSave.subscribe(() => {
      // this.getProducts();
    });
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
