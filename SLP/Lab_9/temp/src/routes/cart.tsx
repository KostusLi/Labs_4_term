import { createFileRoute, Link } from '@tanstack/react-router';
import { useRecoilState, useRecoilValue } from 'recoil';
import { cartState, uiSettingsState } from '../store/atom';
import { useQuery } from '@tanstack/react-query';
import { ProductSchema } from '../productSchema';
import { z } from 'zod';

export const Route = createFileRoute('/cart')({
  component: CartPage,
});

function CartPage() {
  const [cart, setCart] = useRecoilState(cartState);
  const uiSettings = useRecoilValue(uiSettingsState);

  const { data: allProducts } = useQuery({
    queryKey: ['products', ''],
    queryFn: async () => {
      const res = await fetch('https://dummyjson.com/products?limit=100');
      const json = await res.json();
      return z.array(ProductSchema).parse(json.products);
    },
  });

  const cartItems = cart.map(item => {
    const product = allProducts?.find(p => p.id === item.id);
    return { ...item, details: product };
  }).filter(item => item.details);

  const updateQuantity = (id: number, delta: number) => {
    setCart(prev => prev.map(item => {
      if (item.id === id) {
        const newQty = item.quantity + delta;
        return { ...item, quantity: newQty > 0 ? newQty : 1 };
      }
      return item;
    }));
  };

  const removeItem = (id: number) => {
    setCart(prev => prev.filter(item => item.id !== id));
  };

  const totalPrice = cartItems.reduce((sum, item) => 
    sum + (item.details?.price || 0) * item.quantity, 0
  );

  if (cartItems.length === 0) {
    return (
      <div style={{ textAlign: 'center', padding: '50px' }}>
        <h2 style={{color: uiSettings.theme === 'dark' ? '#fff' : '#08060d'}}>Ваша корзина пуста</h2>
        <Link to="/catalog" style={{ color: 'var(--accent)' }}>Вернуться в каталог</Link>
      </div>
    );
  }

  return (
    <div 
      className="registration-container" 
      style={{ 
        maxWidth: '800px', 
        textAlign: 'left',
        // ПРИМЕНЯЕМ ТЕМУ К КОНТЕЙНЕРУ
        background: uiSettings.theme === 'dark' ? '#2d2d2d' : '#ffffff',
        color: uiSettings.theme === 'dark' ? '#eee' : '#333',
        border: uiSettings.theme === 'dark' ? '1px solid #444' : '1px solid #eee',
        transition: 'all 0.3s'
      }}
    >
      <h3 style={{ borderBottom: uiSettings.theme === 'dark' ? '2px solid #444' : '2px solid #f0f0f0' }}>Ваша корзина</h3>
      
      <div style={{ display: 'flex', flexDirection: 'column', gap: '15px' }}>
        {cartItems.map(item => (
          <div key={item.id} style={{ 
            display: 'flex', 
            justifyContent: 'space-between', 
            alignItems: 'center',
            padding: '10px',
            borderBottom: uiSettings.theme === 'dark' ? '1px solid #444' : '1px solid #eee'
          }}>
            <div>
              <h4 style={{ margin: 0 }}>{item.details?.title}</h4>
              <p style={{ margin: 0, fontSize: '14px', color: uiSettings.theme === 'dark' ? '#aaa' : '#666' }}>
                Цена: ${item.details?.price} x {item.quantity} = <b>${(item.details?.price || 0) * item.quantity}</b>
              </p>
            </div>
            
            <div style={{ display: 'flex', alignItems: 'center', gap: '10px' }}>
              <button onClick={() => updateQuantity(item.id, -1)} style={{ padding: '2px 8px' }}>-</button>
              <span style={{ minWidth: '20px', textAlign: 'center' }}>{item.quantity}</span>
              <button onClick={() => updateQuantity(item.id, 1)} style={{ padding: '2px 8px' }}>+</button>
              <button 
                onClick={() => removeItem(item.id)} 
                style={{ marginLeft: '10px', background: '#dc3545', color: 'white', fontSize: '12px' }}
              >
                Удалить
              </button>
            </div>
          </div>
        ))}
      </div>

      <div style={{ 
        marginTop: '30px', 
        textAlign: 'right', 
        borderTop: uiSettings.theme === 'dark' ? '2px solid #444' : '2px solid #eee', 
        paddingTop: '15px' 
      }}>
        <h4>Итого: <span style={{ color: 'var(--accent)', fontSize: '24px' }}>${totalPrice}</span></h4>
        <button style={{ 
          background: 'var(--accent)', 
          color: 'white', 
          padding: '12px 20px', 
          width: '100%',
          border: 'none',
          borderRadius: '8px',
          cursor: 'pointer',
          fontWeight: 'bold'
        }}>
          Оформить заказ
        </button>
      </div>
    </div>
  );
}