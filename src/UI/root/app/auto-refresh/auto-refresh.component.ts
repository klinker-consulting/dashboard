import { Component } from '@angular/core';
import { Store } from '@ngrx/store';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';

import { IState } from './auto-refresh.reducer';
import {
    startAutoRefresh, 
    stopAutoRefresh,
    refreshElapsed
} from './actions/auto-refresh.actions';

@Component({
    selector: 'auto-refresh',
    template: require('./auto-refresh.component.html'),
    styles: [
        require('./auto-refresh.component.scss')
    ]
})
export class AutoRefreshComponent {
    get lastRefresh$(): Observable<string> {
        return this.store.select<Date>('autoRefresh', 'lastRefresh')
            .map(d => this.formatDate(d));
    }

    get isStarted$(): Observable<boolean> {
        return this.store.select<boolean>('autoRefresh', 'isAutoRefreshing');
    }

    get isStopped$(): Observable<boolean> {
        return this.store.select<boolean>('autoRefresh', 'isAutoRefreshing')
            .map(v => !v);
    }

    constructor(private store: Store<IState>) {

    }

    start(): void {
        this.store.dispatch(startAutoRefresh());
    }

    stop(): void {
        this.store.dispatch(stopAutoRefresh());
    }

    private formatDate(date: Date): string {
        return date ? `${date.toLocaleDateString()} ${date.toLocaleTimeString()}` : '';
    }
}