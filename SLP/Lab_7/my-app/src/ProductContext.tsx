import React, { createContext, useContext, useReducer, useEffect, ReactNode } from 'react';
import { IProduct } from './productShema';

interface IProductState {
  products: IProduct[];
  loading: boolean;
}

type Action = 
  | { type: 'SET_PRODUCTS'; payload: IProduct[] }
  | { type: 'ADD_PRODUCT'; payload: IProduct }
  | { type: 'DELETE_PRODUCT'; payload: number }
  | { type: 'UPDATE_PRODUCT'; payload: IProduct };

const productReducer = (state: IProductState, action: Action): IProductState => {
  switch (action.type) {
    case 'SET_PRODUCTS': return { ...state, products: action.payload, loading: false };
    case 'ADD_PRODUCT': return { ...state, products: [action.payload, ...state.products] };
    case 'DELETE_PRODUCT': return { ...state, products: state.products.filter(p => p.id !== action.payload) };
    case 'UPDATE_PRODUCT': return { ...state, products: state.products.map(p => p.id === action.payload.id ? action.payload : p) };
    default: return state;
  }
};

const ProductContext = createContext<any>(null);

export const ProductProvider = ({ children }: { children: ReactNode }) => {
  const [state, dispatch] = useReducer(productReducer, { products: [], loading: true });

  // 1. READ (GET запрос)
  useEffect(() => {
    fetch('https://dummyjson.com/products?limit=10')
      .then(res => res.json())
      .then(data => dispatch({ type: 'SET_PRODUCTS', payload: data.products }));
  }, []);

  // 2. CREATE (POST запрос)
  const addProduct = async (newProd: Omit<IProduct, 'id'>) => {
    const res = await fetch('https://dummyjson.com/products/add', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(newProd),
    });
    const data = await res.json();
    dispatch({ type: 'ADD_PRODUCT', payload: { ...data, id: Date.now() } }); // Даем временный ID
  };

  // 3. DELETE
  const deleteProduct = async (id: number) => {
    await fetch(`https://dummyjson.com/products/${id}`, { method: 'DELETE' });
    dispatch({ type: 'DELETE_PRODUCT', payload: id });
  };

  // 4. UPDATE (PUT запрос)
  const updateProduct = async (prod: IProduct) => {
    await fetch(`https://dummyjson.com/products/${prod.id}`, {
      method: 'PUT',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(prod),
    });
    dispatch({ type: 'UPDATE_PRODUCT', payload: prod });
  };

  return (
    <ProductContext.Provider value={{ ...state, addProduct, deleteProduct, updateProduct }}>
      {children}
    </ProductContext.Provider>
  );
};

export const useProducts = () => useContext(ProductContext);