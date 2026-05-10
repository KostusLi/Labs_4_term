import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { useSearch, Link, useNavigate } from '@tanstack/react-router';
import { ProductSchema, type IProduct } from './productSchema';
import { useAuth } from './AuthContext';
import { z } from 'zod';

import { useRecoilState, useRecoilValue, useSetRecoilState } from 'recoil';
import { uiSettingsState, favoritesState, cartState } from './store/atom';
import { gridStylesSelector } from './store/selectors';

export const CatalogPage = () => {
  const { logout, user } = useAuth();
  const queryClient = useQueryClient();
  const navigate = useNavigate();
  
  const [uiSettings, setUiSettings] = useRecoilState(uiSettingsState);
  const gridStyles = useRecoilValue(gridStylesSelector);

  const setFavorites = useSetRecoilState(favoritesState);
  const favorites = useRecoilValue(favoritesState);
  const setCart = useSetRecoilState(cartState);

  const { category = '' } = useSearch({ from: '/catalog' });
  const currentKey = ['products', category];

  const { data: products, isLoading, isError } = useQuery({
    queryKey: currentKey,
    queryFn: async () => {
      const url = category 
        ? `https://dummyjson.com/products/category/${category}`
        : 'https://dummyjson.com/products?limit=10';
      const res = await fetch(url);
      const json = await res.json();
      const ListSchema = z.array(ProductSchema);
      const result = ListSchema.safeParse(json.products);
      if (!result.success) throw new Error("Ошибка структуры данных");
      return result.data;
    },
    staleTime: 60000,
    retry: false,
  });

  const toggleFavorite = (id: number) => {
    setFavorites((prev) => 
      prev.includes(id) ? prev.filter(favId => favId !== id) : [...prev, id]
    );
  };

  const addToCart = (id: number) => {
    setCart((prev) => {
      const existing = prev.find(item => item.id === id);
      if (existing) {
        return prev.map(item => item.id === id ? { ...item, quantity: item.quantity + 1 } : item);
      }
      return [...prev, { id, quantity: 1 }];
    });
  };

  const deleteMutation = useMutation({
    mutationFn: async (id: number) => {
      await fetch(`https://dummyjson.com/products/${id}`, { method: 'DELETE' });
    },
    onMutate: async (id) => {
      await queryClient.cancelQueries({ queryKey: currentKey });
      const previousProducts = queryClient.getQueryData(currentKey);
      queryClient.setQueryData(currentKey, (old: any) => old?.filter((p: any) => p.id !== id));
      return { previousProducts };
    },
    onError: (err, id, context) => {
      queryClient.setQueryData(currentKey, context?.previousProducts);
    }
  });

  const addMutation = useMutation({
    mutationFn: async (newProd: any) => {
      const res = await fetch('https://dummyjson.com/products/add', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(newProd),
      });
      const data = await res.json();
      return { ...data, id: Date.now() };
    },
    onSuccess: (newItem) => {
      queryClient.setQueryData(currentKey, (oldData: any) => oldData ? [newItem, ...oldData] : [newItem]);
    }
  });

  const updateMutation = useMutation({
    mutationFn: async (prod: IProduct) => {
      if (prod.id < 200) {
        await fetch(`https://dummyjson.com/products/${prod.id}`, {
          method: 'PUT',
          headers: { 'Content-Type': 'application/json' },
          body: JSON.stringify(prod),
        });
      }
      return prod;
    },
    onSuccess: (updatedItem) => {
      queryClient.setQueryData(currentKey, (oldData: any) => 
        oldData?.map((p: any) => p.id === updatedItem.id ? updatedItem : p)
      );
    },
  });

  const handleLogout = () => {
    logout();
    navigate({ to: '/login' });
  };

  const handleEdit = (product: IProduct) => {
    const newTitle = prompt("Изменить название:", product.title);
    const newPrice = prompt("Изменить цену:", String(product.price));
    if (!newTitle || !newPrice) return;
    const result = ProductSchema.safeParse({ id: product.id, title: newTitle, price: Number(newPrice) });
    if (result.success) updateMutation.mutate(result.data);
  };

  const handleAdd = () => {
    const title = prompt("Название нового товара:");
    const price = prompt("Цена:");
    if (!title || !price) return;
    const result = ProductSchema.safeParse({ title, price: Number(price), id: 1 });
    if (result.success) addMutation.mutate(result.data);
  };

  const categories = [
    { label: 'Все', value: '' },
    { label: 'Смартфоны', value: 'smartphones' },
    { label: 'Ноутбуки', value: 'laptops' },
    { label: 'Парфюмерия', value: 'fragrances' },
    { label: 'Бакалея', value: 'groceries' }
  ];

  if (isLoading) return <h2>Загрузка...</h2>;
  if (isError) return <h2 style={{ color: 'red' }}>Ошибка структуры данных</h2>;

  return (
    <div style={{ 
      padding: '20px',
      background: uiSettings.theme === 'dark' ? '#1a1a1a' : '#fff',
      color: uiSettings.theme === 'dark' ? '#eee' : '#000',
      minHeight: '100vh',
      transition: 'all 0.3s'
    }}>

      <header style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '20px' }}>
        <h2 style={{ color: uiSettings.theme === 'dark' ? '#fff' : '#08060d', margin: 0 }}>
          Каталог для {user?.username}
        </h2>
        
        <div style={{ display: 'flex', gap: '10px' }}>
          <button onClick={() => setUiSettings(s => ({ ...s, viewMode: s.viewMode === 'grid' ? 'list' : 'grid' }))}>
            {uiSettings.viewMode === 'grid' ? 'Список' : 'Сетка'}
          </button>
          <button onClick={() => setUiSettings(s => ({ ...s, theme: s.theme === 'light' ? 'dark' : 'light' }))}>
            {uiSettings.theme === 'light' ? 'Тёмная' : 'Светлая'}
          </button>
          <button className="btn-back" onClick={handleLogout}>Выйти</button>
        </div>
      </header>

      <div style={{ display: 'flex', gap: '10px', marginBottom: '20px', flexWrap: 'wrap' }}>
  {categories.map((cat) => (
    <Link 
      key={cat.value} 
      from="/catalog" 
      search={{ category: cat.value }}
      style={{ 
        padding: '5px 15px', 
        borderRadius: '15px', 
        textDecoration: 'none', 
        fontSize: '14px',
        transition: 'all 0.2s',
        background: category === cat.value 
          ? 'var(--accent)' 
          : (uiSettings.theme === 'dark' ? '#333' : '#eee'),
        color: category === cat.value 
          ? '#fff' 
          : (uiSettings.theme === 'dark' ? '#bbb' : '#000')
      }}
    >
      {cat.label}
    </Link>
  ))}
</div>

      <button className="btn-next" onClick={handleAdd} style={{ marginBottom: '20px' }}>+ Добавить товар</button>

      <div style={{ display: 'grid', gridTemplateColumns: gridStyles.gridTemplateColumns, gap: '20px' }}>
        {products?.map((p: IProduct) => (
          <div key={p.id} className="registration-container" style={{ 
            margin: 0, padding: '15px', textAlign: 'left',
            display: gridStyles.displayType === 'flex' ? 'flex' : 'block',
            justifyContent: 'space-between', alignItems: 'center',
            background: uiSettings.theme === 'dark' ? '#333' : '#fff',
            border: uiSettings.theme === 'dark' ? '1px solid #444' : '1px solid #eee'
          }}>
            <div>
              <Link to="/product/$id" params={{ id: String(p.id) }} style={{ color: 'var(--accent)', textDecoration: 'none' }}>
                <h4 style={{ margin: '0 0 5px 0' }}>{p.title}</h4>
              </Link>
              <p>Цена: ${p.price}</p>
              
              <div style={{ display: 'flex', gap: '10px', marginTop: '10px' }}>
                <button 
                  onClick={() => toggleFavorite(p.id)}
                  style={{ background: 'none', border: 'none', cursor: 'pointer', fontSize: '20px' }}
                >
                  {favorites.includes(p.id) ? '❤️' : '🤍'}
                </button>
                <button 
                  onClick={() => addToCart(p.id)}
                  style={{ background: 'var(--accent)', color: '#fff', fontSize: '12px', padding: '5px 10px' }}
                >
                  🛒 В корзину
                </button>
              </div>
            </div>
            
            <div style={{ display: 'flex', gap: '10px', marginTop: gridStyles.displayType === 'flex' ? '0' : '15px' }}>
              <button style={{ background: '#ffc107', fontSize: '12px', padding: '5px' }} onClick={() => handleEdit(p)}>Изменить</button>
              <button style={{ background: '#dc3545', color: '#fff', fontSize: '12px', padding: '5px' }} onClick={() => deleteMutation.mutate(p.id)}>Удалить</button>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};