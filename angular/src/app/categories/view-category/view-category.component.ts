import {
  Component,
  Injector,
  EventEmitter,
  Output,
  ChangeDetectorRef,
} from "@angular/core";
import {
  CategoryServiceProxy,
  CategoryDto,
} from "@shared/service-proxies/service-proxies";
import { ActivatedRoute } from "@angular/router";

import { AppComponentBase } from "@shared/app-component-base";
import { Location } from "@angular/common";
import { finalize } from "rxjs";
import { BsModalRef } from "ngx-bootstrap/modal";
import { Router } from "@angular/router";

@Component({
  selector: "app-create-product",
  templateUrl: "./view-category.component.html",
  styleUrls: ["./view-category.component.css"],
  standalone: false,
})
export class ViewCategoryComponent extends AppComponentBase {
  saving = false;
  uploadedThumbnail: {
    url: string | ArrayBuffer | null;
    name: string;
    size: number;
  } | null = null;
  category = new CategoryDto();
  selectedFile: File | null = null;
  isDragging = false;
  categoryName: string | null;
  categoryDescripion: string | null;
  categoryId: number | undefined;
  selectedCategory: any;

  @Output() onSave = new EventEmitter<any>();

  constructor(
    private injector: Injector,
    private cd: ChangeDetectorRef,
    private _categoryService: CategoryServiceProxy,
    private location: Location,
    public bsModalRef: BsModalRef,
    private router: Router,
    private activatedRoute: ActivatedRoute
  ) {
    super(injector);
  }
  ngOnInit() {
    this.activatedRoute.queryParams.subscribe((params) => {
      this.categoryId = +params["id"];
    });
    this.getSingleCategory();
  }

  goBack(): void {
    this.location.back();
  }

  getSingleCategory() {
    this._categoryService
      .getSingleCategory(this.categoryId)
      .subscribe((result: any) => {
        this.category = result;
        console.log("Selected cate3", this.category);
        this.cd.detectChanges();
      });
  }

  save(): void {
    this.saving = true;

    const category = new CategoryDto();
    category.init(this.category);
    if (this.uploadedThumbnail) {
      category.categoryThumbnail = this.uploadedThumbnail.url as string;
    }

    this._categoryService
      .updateCategory(this.categoryId, category)
      .pipe(
        finalize(() => {
          this.saving = false;
          this.cd.detectChanges();
        })
      )
      .subscribe(
        () => {
          this.notify.info(this.l("SavedSuccessfully"));
          this.router.navigate(["/app/categories"]);
          this.onSave.emit(null);
        },
        () => {
          this.saving = false;
          this.cd.detectChanges();
        }
      );
  }
}
