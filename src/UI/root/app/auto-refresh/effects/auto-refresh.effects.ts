import { Injectable } from '@angular/core';
import { Action } from '@ngrx/store';
import { Effect, Actions } from '@ngrx/effects';
import { Observable } from 'rxjs/Observable';
import { Subscription } from 'rxjs/Subscription';
import 'rxjs/add/observable/timer';
import 'rxjs/add/observable/range';
import 'rxjs/add/operator/switchMap';
import 'rxjs/add/operator/filter';

import { 
    REFRESH_ELAPSED,
    START_AUTO_REFRESH,
    STOP_AUTO_REFRESH,
    refreshElapsed
} from '../actions/auto-refresh.actions';

@Injectable()
export class AutoRefreshEffects {
    private interval$: Observable<number>;
    private isRefreshing: boolean;

    @Effect()
    stopAutoRefresh$ = this.actions$
        .ofType(STOP_AUTO_REFRESH)
        .switchMap(() => Observable.range(0, 1));
    
    @Effect()
    startAutoRefresh$ = this.actions$
        .ofType(START_AUTO_REFRESH)
        .map(a => {
            this.isRefreshing = true;
            return a;
        })
        .switchMap(() => this.interval$)
        .filter(() => this.isRefreshing)
        .map(() => refreshElapsed());
    
    constructor(private actions$: Actions){
        this.interval$ = Observable.timer(0, 2000);
        this.isRefreshing = false;
        this.stopAutoRefresh$.subscribe(() => this.isRefreshing = false);
    }
}