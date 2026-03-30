import React, { useReducer } from 'react';
import { registrationReducer, initialState } from './reducer';
import './RegistrationForm.css';
import { step1Schema, step2Schema, step3Schema, IFormData } from './schema';

export const RegistrationForm = () => {
  const [state, dispatch] = useReducer(registrationReducer, initialState);

  const handleChange = (field: keyof IFormData, value: string | boolean) => {
    dispatch({ type: 'UPDATE_FIELD', payload: { field, value } });
  };

  const handleNext = () => {
    let currentSchema;
    if (state.currentStep === 1) currentSchema = step1Schema;
    if (state.currentStep === 2) currentSchema = step2Schema;

    if (currentSchema) {
      const result = currentSchema.safeParse(state.formData);
      if (result.success) {
        dispatch({ type: 'NEXT_STEP' });
      } else {
        const fieldErrors = result.error.flatten().fieldErrors;
        Object.entries(fieldErrors).forEach(([field, messages]) => {
          if (messages) {
            dispatch({ type: 'SET_ERROR', payload: { field: field as keyof IFormData, message: messages[0] } });
          }
        });
      }
    }
  };

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    const result = step3Schema.safeParse(state.formData);
    
    if (!result.success) {
      const fieldErrors = result.error.flatten().fieldErrors;
      Object.entries(fieldErrors).forEach(([field, messages]) => {
        if (messages) {
          dispatch({ type: 'SET_ERROR', payload: { field: field as keyof IFormData, message: messages[0] } });
        }
      });
      return;
    }

    dispatch({ type: 'SUBMIT_START' });
    setTimeout(() => {
      console.log('Финальные данные:', state.formData);
      dispatch({ type: 'SUBMIT_SUCCESS' });
      alert('Регистрация прошла успешно! Проверьте консоль.');
    }, 2000);
  };

const renderStep = () => {
    switch (state.currentStep) {
      case 1:
        return (
          <div className="form-step">
            <h3>Шаг 1: Аккаунт</h3>
            <input 
              className="form-input" 
              placeholder="Email" 
              value={state.formData.email} 
              onChange={(e) => handleChange('email', e.target.value)} 
            />
            {state.errors.email && <span className="error-message">{state.errors.email}</span>}
            
            <input 
              className="form-input"
              type="password" 
              placeholder="Пароль" 
              value={state.formData.password} 
              onChange={(e) => handleChange('password', e.target.value)} 
            />
            {state.errors.password && <span className="error-message">{state.errors.password}</span>}
          </div>
        );
      case 2:
        return (
          <div className="form-step">
            <h3>Шаг 2: Профиль</h3>
            <input 
              className="form-input"
              placeholder="Имя пользователя" 
              value={state.formData.username} 
              onChange={(e) => handleChange('username', e.target.value)} 
            />
            {state.errors.username && <span className="error-message">{state.errors.username}</span>}
            
            <input 
              className="form-input"
              placeholder="Город" 
              value={state.formData.city} 
              onChange={(e) => handleChange('city', e.target.value)} 
            />
            {state.errors.city && <span className="error-message">{state.errors.city}</span>}
          </div>
        );
      case 3:
        return (
          <div className="form-step">
            <h3>Шаг 3: О себе</h3>
            <select 
              className="form-select"
              value={state.formData.occupation} 
              onChange={(e) => handleChange('occupation', e.target.value)}
            >
              <option value="">Выберите профессию...</option>
              <option value="Dev">Разработчик</option>
              <option value="QA">Тестировщик</option>
            </select>
            {state.errors.occupation && <span className="error-message">{state.errors.occupation}</span>}
            
            <label className="checkbox-group">
              <input 
                type="checkbox" 
                checked={!!state.formData.acceptedTerms} 
                onChange={(e) => handleChange('acceptedTerms', e.target.checked)} 
              />
              Согласен с правилами
            </label>
            {state.errors.acceptedTerms && <span className="error-message">{state.errors.acceptedTerms}</span>}
          </div>
        );
      default: return null;
    }
  };

  return (
    <div className="registration-container">
      <form onSubmit={handleSubmit}>
        {renderStep()}
        
        <div className="button-group">
          {state.currentStep > 1 && (
            <button type="button" className="btn-back" onClick={() => dispatch({ type: 'PREV_STEP' })}>
              Назад
            </button>
          )}

          {state.currentStep < 3 ? (
            <button type="button" className="btn-next" onClick={handleNext}>
              Далее
            </button>
          ) : (
            <button type="submit" className="btn-submit" disabled={state.isSubmitting}>
              {state.isSubmitting ? 'Загрузка...' : 'Зарегистрироваться'}
            </button>
          )}
        </div>
      </form>
    </div>
  );
};