import { createContext, useContext, useReducer, useEffect, type ReactNode} from 'react';
import { type IProduct } from './productSchema';

interface IProductState {
  products: IProduct[];
  loading: boolean;
}

type Action = 
  | { type: 'SET_PRODUCTS'; payload: IProduct[] }
  | { type: 'ADD_PRODUCT'; payload: IProduct }
  | { type: 'DELETE_PRODUCT'; payload: number }
  | { type: 'UPDATE_PRODUCT'; payload: IProduct };

interface IProductContext extends IProductState {
  addProduct: (newProd: Omit<IProduct, 'id'>) => Promise<void>;
  deleteProduct: (id: number) => Promise<void>;
  updateProduct: (prod: IProduct) => Promise<void>;
}

const productReducer = (state: IProductState, action: Action): IProductState => {
  switch (action.type) {
    case 'SET_PRODUCTS': return { ...state, products: action.payload, loading: false };
    case 'ADD_PRODUCT': return { ...state, products: [action.payload, ...state.products] };
    case 'DELETE_PRODUCT': return { ...state, products: state.products.filter(p => p.id !== action.payload) };
    case 'UPDATE_PRODUCT': return { ...state, products: state.products.map(p => p.id === action.payload.id ? action.payload : p) };
    default: return state;
  }
};

const ProductContext = createContext<IProductContext | undefined>(undefined);

export const ProductProvider = ({ children }: { children: ReactNode }) => {
  const [state, dispatch] = useReducer(productReducer, { products: [], loading: true });

  useEffect(() => {
    fetch('https://dummyjson.com/products?limit=10')
      .then(res => res.json())
      .then(data => dispatch({ type: 'SET_PRODUCTS', payload: data.products }));
  }, []);

  const addProduct = async (newProd: Omit<IProduct, 'id'>) => {
    const res = await fetch('https://dummyjson.com/products/add', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(newProd),
    });
    const data = await res.json();
    dispatch({ type: 'ADD_PRODUCT', payload: { ...data, id: Date.now() } });
  };

  const deleteProduct = async (id: number) => {
    await fetch(`https://dummyjson.com/products/${id}`, { method: 'DELETE' });
    dispatch({ type: 'DELETE_PRODUCT', payload: id });
  };

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

export const useProducts = () => {
  const context = useContext(ProductContext);
  if (!context) throw new Error("useProducts must be used within ProductProvider");
  return context;
};