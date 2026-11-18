
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FoodItemList } from '../../components/food-item-list/food-item-list';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-admin-dashboard',
  standalone: true,
  imports: [FoodItemList],
  templateUrl: './admin-dashboard.html',
  styleUrl: './admin-dashboard.css'
})
export class AdminDashboard {

  adminName = localStorage.getItem('fullName') || 'Admin';

  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/admin/login']);
  }
}
