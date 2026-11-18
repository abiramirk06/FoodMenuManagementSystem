
import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';

import { FoodItem } from '../../models/food-item';
import { FoodItemService } from '../../services/food-item.service';

@Component({
  selector: 'app-public-menu',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './public-menu.html',
  styleUrl: './public-menu.css'
})
export class PublicMenu implements OnInit {

  foodItems: FoodItem[] = [];

  isLoading = true;
  errorMessage = '';

  searchTerm = '';
  selectedCategory = 'All';
  showAvailableOnly = false;

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
        this.foodItems = data;
        this.isLoading = false;
        this.cdr.detectChanges();
      },

      error: (error) => {
        console.error('Menu loading error:', error);
        this.errorMessage = 'Unable to load the menu.';
        this.isLoading = false;
        this.cdr.detectChanges();
      }
    });
  }

  get categories(): string[] {
    return [
      'All',
      ...new Set(this.foodItems.map(item => item.category))
    ];
  }

  get filteredFoodItems(): FoodItem[] {
    return this.foodItems.filter(item => {

      const matchesSearch =
        item.name.toLowerCase().includes(this.searchTerm.toLowerCase());

      const matchesCategory =
        this.selectedCategory === 'All' ||
        item.category === this.selectedCategory;

      const matchesAvailability =
        !this.showAvailableOnly || item.isAvailable;

      return matchesSearch && matchesCategory && matchesAvailability;
    });
  }

  resetFilters(): void {
    this.searchTerm = '';
    this.selectedCategory = 'All';
    this.showAvailableOnly = false;
  }
}
