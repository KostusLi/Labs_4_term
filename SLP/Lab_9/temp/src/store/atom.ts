import { atom, type AtomEffect } from 'recoil';

const localStorageEffect = <T>(key: string): AtomEffect<T> => ({ setSelf, onSet }) => {
  const savedValue = localStorage.getItem(key);
  if (savedValue != null) {
    setSelf(JSON.parse(savedValue));
  }

  onSet((newValue, _, isReset) => {
    isReset
      ? localStorage.removeItem(key)
      : localStorage.setItem(key, JSON.stringify(newValue));
  });
};

export const uiSettingsState = atom({
  key: 'uiSettingsState',
  default: {
    viewMode: 'grid' as 'grid' | 'list',
    theme: 'light' as 'light' | 'dark',
  },
  effects: [localStorageEffect('ui_settings')],
});

export const favoritesState = atom<number[]>({
  key: 'favoritesState',
  default: [],
  effects: [localStorageEffect('favorites')],
});

export interface ICartItem {
  id: number;
  quantity: number;
}

export const cartState = atom<ICartItem[]>({
  key: 'cartState',
  default: [],
  effects: [localStorageEffect('cart')],
});