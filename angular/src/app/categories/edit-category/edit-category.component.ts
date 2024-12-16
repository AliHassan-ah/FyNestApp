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
  templateUrl: "./edit-category.component.html",
  styleUrls: ["./edit-category.component.css"],
  standalone: false,
})
export class EditCategoryComponent extends AppComponentBase {
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

  triggerFileInput(): void {
    const fileInput = document.getElementById(
      "categoryImage"
    ) as HTMLInputElement;
    fileInput.click();
  }

  onFileChange(event: any): void {
    const file = event.target.files[0];
    if (file) {
      this.readFile(file);
    }
  }

  onDragOver(event: DragEvent): void {
    event.preventDefault(); // Prevent default behavior (e.g., opening the file in the browser)
    event.stopPropagation();
    this.isDragging = true;
  }

  onDragLeave(event: DragEvent): void {
    event.preventDefault();
    event.stopPropagation();
    this.isDragging = false;
  }

  onDrop(event: DragEvent): void {
    event.preventDefault();
    event.stopPropagation();
    this.isDragging = false;

    if (event.dataTransfer?.files.length) {
      const file = event.dataTransfer.files[0]; // Get only the first file
      this.readFile(file);
    }
  }

  private readFile(file: File): void {
    if (file.type.startsWith("image/")) {
      const reader = new FileReader();
      reader.onload = () => {
        const result = reader.result as string;
        this.uploadedThumbnail = {
          url: result,
          name: file.name,
          size: Math.round(file.size / 1024),
        };
        this.category.categoryThumbnail = result;
        this.cd.detectChanges();
      };
      reader.readAsDataURL(file);
    } else {
      console.error("Invalid file type. Please upload an image.");
    }
  }

  removeThumbnail(): void {
    this.category.categoryThumbnail = null;
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
