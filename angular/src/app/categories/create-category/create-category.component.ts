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
import { AppComponentBase } from "@shared/app-component-base";
import { Location } from "@angular/common";
import { finalize } from "rxjs";
import { BsModalRef } from "ngx-bootstrap/modal";
import { Router } from "@angular/router";

@Component({
  selector: "app-create-product",
  templateUrl: "./create-category.component.html",
  styleUrls: ["./create-category.component.css"],
  standalone: false,
})
export class CreateCategoryComponent extends AppComponentBase {
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

  @Output() onSave = new EventEmitter<any>();

  constructor(
    private injector: Injector,
    private cd: ChangeDetectorRef,
    private _categoryService: CategoryServiceProxy,
    private location: Location,
    public bsModalRef: BsModalRef,
    private router: Router
  ) {
    super(injector);
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
    const file = event.target.files[0]; // Get only the first file
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
        this.uploadedThumbnail = {
          url: reader.result,
          name: file.name,
          size: Math.round(file.size / 1024),
        };
        this.cd.detectChanges();
      };
      reader.readAsDataURL(file);
    } else {
      console.error("Invalid file type. Please upload an image.");
    }
  }

  removeThumbnail(): void {
    this.uploadedThumbnail = null; // Clear the thumbnail data
    console.log("Thumbnail removed!");
  }

  save(): void {
    this.saving = true;

    const category = new CategoryDto();
    category.init(this.category);
    if (this.uploadedThumbnail) {
      category.categoryThumbnail = this.uploadedThumbnail.url as string;
    }

    this._categoryService
      .createCategoty(category)
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
          // this.onSave.emit(null);
        },
        () => {
          this.saving = false;
          this.cd.detectChanges();
        }
      );
  }
}
