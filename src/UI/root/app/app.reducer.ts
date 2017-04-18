import { ActionReducer, combineReducers } from '@ngrx/store';

import { 
    autoRefreshReducer,
    IState as IAutoRefreshState
} from './auto-refresh/auto-refresh.reducer';

export interface IState {
    autoRefresh: IAutoRefreshState
}

export const appReducer: ActionReducer<IState> = combineReducers({
    autoRefresh: autoRefreshReducer
});