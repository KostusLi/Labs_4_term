import { createFileRoute, Link } from '@tanstack/react-router'
import { useQuery } from '@tanstack/react-query'
import { ProductSchema } from '../productSchema'

export const Route = createFileRoute('/product/$id')({
  component: ProductDetail,
})

function ProductDetail() {
  const { id } = Route.useParams()

  const { data: product, isLoading, isError } = useQuery({
    queryKey: ['product', id],
    queryFn: async () => {
      const res = await fetch(`https://dummyjson.com/products/${id}`);
      const json = await res.json();
      // Валидируем данные одного товара
      return ProductSchema.parse(json); 
    }
  })

  if (isLoading) return <div>Загрузка...</div>
  if (isError) return <div>Ошибка: Товар не найден или данные некорректны</div>

  return (
    <div className="registration-container" style={{ textAlign: 'left' }}>
      <Link to="/catalog" className="btn-back" style={{ textDecoration: 'none', display: 'inline-block', marginBottom: '20px' }}>
        ← Назад в каталог
      </Link>
      <h1>{product?.title}</h1>
      <p style={{ fontSize: '20px' }}>Цена: <b>${product?.price}</b></p>
      <p>{product?.description}</p>
    </div>
  )
}