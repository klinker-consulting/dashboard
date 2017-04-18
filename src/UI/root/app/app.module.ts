import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';

import { appReducer } from './app.reducer';
import { AppComponent } from './app.component';
import { AutoRefreshModule } from './auto-refresh/auto-refresh.module';

@NgModule({
    imports: [
        CommonModule,
        BrowserModule,
        StoreModule.provideStore(appReducer),
        AutoRefreshModule
    ],
    declarations: [
        AppComponent
    ],
    bootstrap: [ AppComponent ]
})
export class AppModule {}