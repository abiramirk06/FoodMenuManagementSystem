
import { Component } from '@angular/core';
import { FoodItemList } from './components/food-item-list/food-item-list';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [FoodItemList],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
}
