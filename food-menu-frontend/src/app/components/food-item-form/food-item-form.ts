
import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { FoodItem } from '../../models/food-item';

@Component({
  selector: 'app-food-item-form',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './food-item-form.html',
  styleUrl: './food-item-form.css'
})
export class FoodItemForm {

  @Input() set foodItem(value: FoodItem | null) {
    if (value) {
      this.formData = { ...value };
    } else {
      this.resetForm();
    }
  }

  @Output() saved = new EventEmitter<FoodItem>();
  @Output() cancelled = new EventEmitter<void>();

  formData: FoodItem = {
    id: 0,
    name: '',
    category: '',
    description: '',
    price: 0,
    isAvailable: true
  };

  get isEditMode(): boolean {
    return this.formData.id > 0;
  }

  submitForm(): void {
    this.saved.emit({ ...this.formData });
  }

  cancelForm(): void {
    this.cancelled.emit();
  }

  resetForm(): void {
    this.formData = {
      id: 0,
      name: '',
      category: '',
      description: '',
      price: 0,
      isAvailable: true
    };
  }
}
