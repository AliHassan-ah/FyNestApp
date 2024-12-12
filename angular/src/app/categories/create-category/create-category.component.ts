import {
  Component,
  Injector,
  EventEmitter,
  Output,
  ChangeDetectorRef,
} from "@angular/core";
import { AppComponentBase } from "@shared/app-component-base";
import { Location } from "@angular/common";

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

  selectedFile: File | null = null;
  isDragging = false;

  @Output() onSave = new EventEmitter<any>();

  constructor(
    private injector: Injector,
    private cd: ChangeDetectorRef,
    private location: Location
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
    if (this.uploadedThumbnail) {
      console.log("Product saved with thumbnail:", this.uploadedThumbnail);
    } else {
      console.log("No thumbnail uploaded.");
    }
  }
}
