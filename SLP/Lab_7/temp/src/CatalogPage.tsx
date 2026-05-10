import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { useSearch, Link } from '@tanstack/react-router';
import { ProductSchema, type IProduct } from './productSchema';
import { useAuth } from './AuthContext';
import { z } from 'zod';

export const CatalogPage = () => {
  const { logout, user } = useAuth();
  const queryClient = useQueryClient();
  
  // Получаем категорию, по умолчанию пустая строка
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
      
      if (!result.success) {
        throw new Error("Ошибка структуры данных");
      }
      return result.data;
    },
    staleTime: 60000,
    gcTime: 300000,
  });

  // 1. УДАЛЕНИЕ (Оптимистичное)
  const deleteMutation = useMutation({
    mutationFn: async (id: number) => {
      await fetch(`https://dummyjson.com/products/${id}`, { method: 'DELETE' });
    },
    onMutate: async (id) => {
      await queryClient.cancelQueries({ queryKey: currentKey });
      const previousProducts = queryClient.getQueryData(currentKey);

      queryClient.setQueryData(currentKey, (old: any) => 
        old?.filter((p: any) => p.id !== id)
      );
      return { previousProducts };
    },
    onError: (err, id, context) => {
      queryClient.setQueryData(currentKey, context?.previousProducts);
    },
    // УБРАЛИ invalidateQueries, чтобы сервер не "воскрешал" удаленные товары
  });

  // 2. ДОБАВЛЕНИЕ (Ручное обновление кэша)
  const addMutation = useMutation({
    mutationFn: async (newProd: any) => {
      const res = await fetch('https://dummyjson.com/products/add', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(newProd),
      });
      const data = await res.json();
      // DummyJSON возвращает одинаковый ID, заменяем его на уникальный для сессии
      return { ...data, id: Date.now() };
    },
    onSuccess: (newItem) => {
      queryClient.setQueryData(currentKey, (oldData: any) => {
        return oldData ? [newItem, ...oldData] : [newItem];
      });
      alert("Товар добавлен локально!");
    }
  });

  // 3. ИЗМЕНЕНИЕ (Ручное обновление кэша)
  const updateMutation = useMutation({
    mutationFn: async (prod: IProduct) => {
      // Имитируем запрос
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
      queryClient.setQueryData(currentKey, (oldData: any) => {
        return oldData?.map((p: any) => p.id === updatedItem.id ? updatedItem : p);
      });
      alert("Товар обновлен!");
    },
  });

  const handleEdit = (product: IProduct) => {
    const newTitle = prompt("Изменить название:", product.title);
    const newPrice = prompt("Изменить цену:", String(product.price));

    if (!newTitle || !newPrice) return;

    const result = ProductSchema.safeParse({ 
      id: product.id, 
      title: newTitle, 
      price: Number(newPrice) 
    });

    if (result.success) {
      updateMutation.mutate(result.data);
    } else {
      alert("Ошибка: " + JSON.stringify(result.error.flatten().fieldErrors));
    }
  };

  const handleAdd = () => {
    const title = prompt("Название нового товара:");
    const price = prompt("Цена:");
    if (!title || !price) return;

    const result = ProductSchema.safeParse({ 
      title, 
      price: Number(price), 
      id: 1 // Временный ID
    });

    if (result.success) {
      addMutation.mutate(result.data);
    } else {
      alert("Ошибка: " + JSON.stringify(result.error.flatten().fieldErrors));
    }
  };

  if (isLoading) return <h2>Загрузка...</h2>;
  if (isError) return <h2 style={{ color: 'red' }}>Ошибка структуры данных</h2>;

  const categories = [
    { name: 'Все', value: '' },
    { name: 'Смартфоны', value: 'smartphones' },
    { name: 'Ноутбуки', value: 'laptops' },
  ];

  return (
    <div style={{ padding: '20px' }}>
      <div style={{ display: 'flex', gap: '10px', marginBottom: '20px' }}>
        {categories.map((cat) => (
          <Link key={cat.name} from="/catalog" search={{ category: cat.value }}
            style={{ padding: '5px 15px', borderRadius: '15px', textDecoration: 'none', 
            background: category === cat.value ? 'var(--accent)' : '#eee', color: category === cat.value ? '#fff' : '#000' }}>
            {cat.name}
          </Link>
        ))}
      </div>

      <button className="btn-next" onClick={handleAdd} style={{ marginBottom: '20px' }}>+ Добавить товар</button>

      <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fill, minmax(250px, 1fr))', gap: '20px' }}>
        {products?.map((p: IProduct) => (
          <div key={p.id} className="registration-container" style={{ margin: 0, padding: '15px', textAlign: 'left' }}>
            <Link to="/product/$id" params={{ id: String(p.id) }} style={{ color: 'var(--accent)', textDecoration: 'none' }}>
              <h4>{p.title}</h4>
            </Link>
            <p>Цена: ${p.price}</p>
            <div style={{ display: 'flex', gap: '10px', marginTop: '10px' }}>
              <button style={{ background: '#ffc107', flex: 1 }} onClick={() => handleEdit(p)}>Изменить</button>
              <button style={{ background: '#dc3545', color: '#fff', flex: 1 }} onClick={() => deleteMutation.mutate(p.id)}>Удалить</button>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};