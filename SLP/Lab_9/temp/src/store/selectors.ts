import { selector } from 'recoil';
import { uiSettingsState, cartState } from './atom';

export const gridStylesSelector = selector({
  key: 'gridStylesSelector',
  get: ({ get }) => {
    const settings = get(uiSettingsState);
    if (settings.viewMode === 'list') {
      return {
        gridTemplateColumns: '1fr', 
        displayType: 'flex' as const,
      };
    }
    return {
      gridTemplateColumns: 'repeat(auto-fill, minmax(250px, 1fr))',
      displayType: 'block' as const,
    };
  },
});

export const cartCountState = selector({
  key: 'cartCountState',
  get: ({ get }) => {
    const cart = get(cartState);
    return cart.reduce((total, item) => total + item.quantity, 0);
  },
});

export const cartProductsSelector = selector({
  key: 'cartProductsSelector',
  get: ({ get }) => {
    const cart = get(cartState);
    return cart;
  },
});