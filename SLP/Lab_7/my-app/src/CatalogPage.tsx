import React from 'react';
import { useAuth } from './AuthContext';
import { useProducts } from './ProductContext';
import { ProductSchema } from './productShema';

export const CatalogPage = () => {
  const { user, logout } = useAuth();
  const { products, addProduct, deleteProduct } = useProducts();

  const handleAdd = () => {
    const title = prompt("Название товара:");
    const price = Number(prompt("Цена:"));
    
    // ВАЛИДАЦИЯ ZOD
    const result = ProductSchema.safeParse({ title, price, id: 1 });
    
    if (result.success) {
      addProduct({ title, price });
    } else {
      // Использование .flatten() как в ТЗ
      const errors = result.error.flatten().fieldErrors;
      alert("Ошибка: " + JSON.stringify(errors));
    }
  };

  return (
    <div style={{ padding: '20px' }}>
      <header style={{ display: 'flex', justifyContent: 'space-between' }}>
        <h2>Каталог для {user?.username}</h2>
        <button onClick={logout}>Выйти</button>
      </header>

      <button onClick={handleAdd} style={{ margin: '20px 0' }}>+ Добавить товар</button>

      <div style={{ display: 'grid', gridTemplateColumns: 'repeat(3, 1fr)', gap: '10px' }}>
        {products.map((p: any) => (
          <div key={p.id} style={{ border: '1px solid #ccc', padding: '10px' }}>
            <h4>{p.title}</h4>
            <p>Цена: ${p.price}</p>
            <button onClick={() => deleteProduct(p.id)}>Удалить</button>
          </div>
        ))}
      </div>
    </div>
  );
};