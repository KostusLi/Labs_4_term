import { createFileRoute, Link } from '@tanstack/react-router'
import { useQuery } from '@tanstack/react-query'
import { ProductSchema } from '../productSchema'
import { useRecoilValue } from 'recoil'
import { uiSettingsState } from '../store/atom'

export const Route = createFileRoute('/product/$id')({
  component: ProductDetail,
})

function ProductDetail() {
  const { id } = Route.useParams()
  const uiSettings = useRecoilValue(uiSettingsState);
  
  const { data: product, isLoading, isError } = useQuery({
    queryKey: ['product', id],
    queryFn: async () => {
      const res = await fetch(`https://dummyjson.com/products/${id}`);
      const json = await res.json();
      return ProductSchema.parse(json); 
    }
  })

  if (isLoading) return <div>Загрузка...</div>
  if (isError) return <div>Ошибка: Товар не найден или данные некорректны</div>

  return (
   <div 
      className="registration-container" 
      style={{ 
        textAlign: 'left',
        background: uiSettings.theme === 'dark' ? '#2d2d2d' : '#ffffff',
        color: uiSettings.theme === 'dark' ? '#eee' : '#333',
        border: uiSettings.theme === 'dark' ? '1px solid #444' : '1px solid #eee',
        transition: 'all 0.3s',
        marginTop: '30px'
      }}
    >
      <Link 
        to="/catalog" 
        className="btn-back" 
        style={{ 
          textDecoration: 'none', 
          display: 'inline-block', 
          marginBottom: '20px',
          color: uiSettings.theme === 'dark' ? 'var(--accent)' : 'inherit'
        }}
      >
        ← Назад в каталог
      </Link>

      <h1 style={{ color: uiSettings.theme === 'dark' ? '#fff' : 'inherit' }}>
        {product?.title}
      </h1>
      
      <p style={{ fontSize: '20px', marginBottom: '15px' }}>
        Цена: <b>${product?.price}</b>
      </p>
      
      <p style={{ lineHeight: '1.6', color: uiSettings.theme === 'dark' ? '#bbb' : '#666' }}>
        {product?.description}
      </p>
    </div>
  )
}