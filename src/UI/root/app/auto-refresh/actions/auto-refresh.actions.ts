import { Action } from '@ngrx/store';

export const START_AUTO_REFRESH = 'START_AUTO_REFRESH';
export const REFRESH_ELAPSED = 'REFRESH_ELAPSED';
export const STOP_AUTO_REFRESH = 'STOP_AUTO_REFRESH';

export const startAutoRefresh = () : Action => {
    return {
        type: START_AUTO_REFRESH
    };
};

export const stopAutoRefresh = () : Action => {
    return {
        type: STOP_AUTO_REFRESH
    };
};

export const refreshElapsed = () : Action => {
    return {
        type: REFRESH_ELAPSED,
        payload: new Date()
    };
};