import { ActionReducer, Action } from '@ngrx/store';
import { Subscription } from 'rxjs/Subscription';
import { Observable } from 'rxjs/Observable';
import { IntervalObservable } from 'rxjs/observable/IntervalObservable';

import { 
    START_AUTO_REFRESH,
    STOP_AUTO_REFRESH,
    REFRESH_ELAPSED,
    refreshElapsed
} from './actions/auto-refresh.actions';

export interface IState {
    isAutoRefreshing: boolean;
    lastRefresh: Date;
}

const initialState: IState = {
    isAutoRefreshing: false,
    lastRefresh: null
}

export const autoRefreshReducer: ActionReducer<IState> = (state: IState = initialState, action: Action) => {
    switch(action.type) {
        case START_AUTO_REFRESH:
            return Object.assign({}, state, { isAutoRefreshing: true });
        case STOP_AUTO_REFRESH:
            return Object.assign({}, state, { isAutoRefreshing: false });
        case REFRESH_ELAPSED:
            return Object.assign({}, state, { lastRefresh: action.payload });    
        default: 
            return state;
    }
}