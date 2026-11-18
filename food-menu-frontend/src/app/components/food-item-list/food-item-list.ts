
import { CommonModule } from '@angular/common';
import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { FoodItem } from '../../models/food-item';
import { FoodItemService } from '../../services/food-item.service';
import { FoodItemForm } from '../food-item-form/food-item-form';

@Component({
  selector: 'app-food-item-list',
  standalone: true,
  imports: [CommonModule, FoodItemForm],
  templateUrl: './food-item-list.html',
  styleUrl: './food-item-list.css'
})
export class FoodItemList implements OnInit {

  foodItems: FoodItem[] = [];

  selectedFoodItem: FoodItem | null = null;
  showForm = false;

  errorMessage = '';
  successMessage = '';

  constructor(
  private foodItemService: FoodItemService,
  private cdr: ChangeDetectorRef
) {}

  ngOnInit(): void {
    this.loadFoodItems();
  }

 loadFoodItems(): void {
  this.foodItemService.getAllFoodItems().subscribe({
    next: (data) => {
      console.log('API Data:', data);

      this.foodItems = data;
      this.errorMessage = '';

      this.cdr.detectChanges();
    },
    error: (error) => {
      console.error('API Error:', error);
      this.errorMessage = 'Unable to load food items.';
    }
  });
}

  openAddForm(): void {
    this.selectedFoodItem = null;
    this.showForm = true;
    this.successMessage = '';
  }

  openEditForm(item: FoodItem): void {
    this.selectedFoodItem = { ...item };
    this.showForm = true;
    this.successMessage = '';
  }

  saveFoodItem(item: FoodItem): void {

    if (this.selectedFoodItem) {

      this.foodItemService.updateFoodItem(item.id, item).subscribe({
        next: () => {
          this.successMessage = 'Food item updated successfully.';
          this.showForm = false;
          this.loadFoodItems();
        },
        error: (error) => {
          console.error(error);
          this.errorMessage = 'Unable to update food item.';
        }
      });

    } else {

      this.foodItemService.addFoodItem(item).subscribe({
        next: () => {
          this.successMessage = 'Food item added successfully.';
          this.showForm = false;
          this.loadFoodItems();
        },
        error: (error) => {
          console.error(error);
          this.errorMessage = 'Unable to add food item.';
        }
      });

    }
  }

  cancelForm(): void {
    this.showForm = false;
    this.selectedFoodItem = null;
  }

  deleteFoodItem(id: number): void {

    if (confirm('Are you sure you want to delete this food item?')) {

      this.foodItemService.deleteFoodItem(id).subscribe({
        next: () => {
          this.successMessage = 'Food item deleted successfully.';
          this.loadFoodItems();
        },
        error: (error) => {
          console.error(error);
          this.errorMessage = 'Unable to delete food item.';
        }
      });

    }
  }
}
