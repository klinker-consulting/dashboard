import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EffectsModule } from '@ngrx/effects';

import { AutoRefreshEffects } from './effects/auto-refresh.effects';
import { AutoRefreshComponent } from './auto-refresh.component';

@NgModule({
    imports: [
        CommonModule,
        EffectsModule.run(AutoRefreshEffects)
    ],
    declarations: [
        AutoRefreshComponent
    ],
    exports: [
        AutoRefreshComponent
    ]
})
export class AutoRefreshModule {}