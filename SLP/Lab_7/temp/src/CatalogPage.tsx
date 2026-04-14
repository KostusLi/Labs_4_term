import { useAuth } from './AuthContext';
import { useProducts } from './ProductContext';
import { ProductSchema, type IProduct } from './productSchema';

export const CatalogPage = () => {
  const { user, logout } = useAuth();
  const { products, addProduct, deleteProduct, updateProduct } = useProducts();

  const handleEdit = (product: IProduct) => {
    const newTitle = prompt("Изменить название:", product.title);
    const newPrice = prompt("Изменить цену:", String(product.price));

    if (newTitle === null || newPrice === null) return;

    const result = ProductSchema.safeParse({ 
      id: product.id, 
      title: newTitle, 
      price: newPrice 
    });

    if (result.success) {
      updateProduct({
        id: product.id,
        title: result.data.title,
        price: result.data.price
      });
    } else {
      const errors = result.error.flatten().fieldErrors;
      alert("Ошибка валидации: " + JSON.stringify(errors));
    }
  };

  const handleAdd = () => {
    const title = prompt("Название нового товара:");
    const price = prompt("Цена:");
    if (title === null || price === null) return;

    const result = ProductSchema.safeParse({ title, price, id: 1 });
    if (result.success) {
      addProduct({ title: result.data.title, price: result.data.price });
    } else {
      const errors = result.error.flatten().fieldErrors;
      alert("Ошибка: " + JSON.stringify(errors));
    }
  };

  return (
    <div style={{ padding: '20px' }}>
      <header style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
        <h2>Каталог для {user?.username}</h2>
        <button className="btn-back" onClick={logout}>Выйти</button>
      </header>

      <button className="btn-next" onClick={handleAdd} style={{ margin: '20px 0' }}>
        + Добавить товар
      </button>

      <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fill, minmax(250px, 1fr))', gap: '20px' }}>
        {products.map((p: IProduct) => (
          <div key={p.id} className="registration-container" style={{ margin: 0, padding: '15px', textAlign: 'left' }}>
            <h4>{p.title}</h4>
            <p>Цена: <b>${p.price}</b></p>
            
            <div style={{ display: 'flex', gap: '10px', marginTop: '10px' }}>
              <button 
                style={{ background: '#ffc107', color: '#000', fontSize: '14px', padding: '5px 10px' }} 
                onClick={() => handleEdit(p)}
              >
                Изменить
              </button>

              <button 
                style={{ background: '#dc3545', color: '#fff', fontSize: '14px', padding: '5px 10px' }} 
                onClick={() => deleteProduct(p.id)}
              >
                Удалить
              </button>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
};