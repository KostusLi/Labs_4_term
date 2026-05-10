import { type IFormData } from './schema';

export type IFormErrors = Partial<Record<keyof IFormData, string>>;

export interface IFormState {
    currentStep: number,
    formData: IFormData,
    errors: IFormErrors,
    isSubmitting: boolean
}

export type TFormAction = 
| {type: 'UPDATE_FIELD'; payload: {field: string; value: string | boolean}}
| {type: 'SET_ERROR'; payload: {field: string; message: string}}
| {type: 'NEXT_STEP'}
| {type: 'PREV_STEP'}
| {type: 'SUBMIT_START'}
| {type: 'SUBMIT_SUCCESS'}