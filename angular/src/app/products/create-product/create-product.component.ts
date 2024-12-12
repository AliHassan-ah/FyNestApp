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
  templateUrl: "./create-product.component.html",
  styleUrls: ["./create-product.component.css"],
  standalone: false,
})
export class CreateProductComponent extends AppComponentBase {
  saving = false;
  uploadedImage: string | ArrayBuffer | null = null;
  uploadedThumbnail: string | ArrayBuffer | null = null;
  uploadedFile: { name: string; size: number } | null = null; // name and size of the uploaded file

  selectedFile: File | null;
  isDragging = false;
  uploadedImages: {
    url: string | ArrayBuffer | null;
    name: string;
    size: number;
  }[] = [];

  @Output() onSave = new EventEmitter<any>();

  constructor(
    private injector: Injector,
    private cd: ChangeDetectorRef,
    private location: Location
  ) {
    super(injector);
  }
  triggerFileInput(): void {
    const fileInput = document.getElementById(
      "productImage"
    ) as HTMLInputElement;
    fileInput.click();
  }
  triggerThumbnailInput(): void {
    const fileInput = document.getElementById(
      "productThumbnail"
    ) as HTMLInputElement;
    fileInput.click();
  }

  onFileChange(event: any): void {
    const files = event.target.files;
    this.readFiles(files);
  }

  onThumbnailChange(event: any): void {
    const file = event.target.files[0];
    if (file) {
      this.processFile(file);
    }
  }

  goBack(): void {
    this.location.back();
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
      this.readFiles(event.dataTransfer?.files);
    }
  }
  private readFiles(files: FileList): void {
    Array.from(files).forEach((file) => {
      if (file.type.startsWith("image/")) {
        const reader = new FileReader();
        reader.onload = () => {
          this.uploadedImages.push({
            url: reader.result,
            name: file.name,
            size: Math.round(file.size / 1024),
          });
          this.cd.detectChanges();
        };
        reader.readAsDataURL(file);
      } else {
        console.error("Invalid file type. Please upload an image.");
      }
    });
  }
  private processFile(file: File): void {
    if (file.type.startsWith("image/")) {
      const reader = new FileReader();
      reader.onload = () => {
        this.uploadedThumbnail = reader.result; // Assign the processed file to `uploadedThumbnail`
        this.uploadedFile = {
          name: file.name,
          size: Math.round(file.size / 1024), // Size in KB
        };
        this.cd.detectChanges();
      };
      reader.readAsDataURL(file);
    } else {
      console.error("Invalid file type. Please upload an image.");
    }
  }

  removeImage(index: number): void {
    this.uploadedImages.splice(index, 1);
  }
  removeThumbnail(): void {
    this.uploadedThumbnail = null;
    this.uploadedFile = null;
  }

  save(): void {
    // Logic to save the product
    console.log("Product saved!");
  }
}
