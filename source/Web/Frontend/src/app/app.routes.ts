import { Routes } from '@angular/router';
import AppLayoutComponent from './layouts/layout/layout.component';

export const routes: Routes = [
    {
        path: '',
        component: AppLayoutComponent,
        children: [
            { path: 'home', loadComponent: () => import('./pages/home/home.component') },
            { path: 'events/add', loadComponent: () => import('./pages/add-event/add-event.component') },
            { path: '', redirectTo: 'home', pathMatch: 'full' }
        ]
    },
    { path: '**', redirectTo: '' }
];
