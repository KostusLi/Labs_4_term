import { z } from 'zod'

export const ProductSchema = z.object({
  id: z.number(),
  title: z.string().min(3, "Название должно быть минимум 3 символа"),
  price: z.coerce.number().positive("Цена должна быть больше 0"),
  category: z.string().optional(),
  description: z.string().optional(),
  thumbnail: z.string().optional(),
})

export type IProduct = z.infer<typeof ProductSchema>

export const fetchProducts = async (category?: string): Promise<IProduct[]> => {
  const url = category 
    ? `https://dummyjson.com/products/category/${category}`
    : 'https://dummyjson.com/products'

  const res = await fetch(url)
  if (!res.ok) throw new Error('Ошибка загрузки списка товаров')
  const data = await res.json()

  return z.array(ProductSchema).parse(data.products)
}

export const fetchProductById = async (id: string): Promise<IProduct> => {
  const res = await fetch(`https://dummyjson.com/products/${id}`)
  if (!res.ok) throw new Error('Товар не найден')
  const data = await res.json()

  return ProductSchema.parse(data)
}

export const addProductApi = async (newProduct: Partial<IProduct>): Promise<IProduct> => {
  const res = await fetch('https://dummyjson.com/products/add', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(newProduct),
  })
  if (!res.ok) throw new Error('Ошибка при добавлении товара')
  const data = await res.json()
  
  return ProductSchema.parse(data)
}

export const updateProductApi = async (product: IProduct): Promise<IProduct> => {
  const res = await fetch(`https://dummyjson.com/products/${product.id}`, {
    method: 'PUT',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(product),
  })
  if (!res.ok) throw new Error('Ошибка при обновлении товара')
  const data = await res.json()

  return ProductSchema.parse(data)
}

export const deleteProductApi = async (id: number) => {
  const res = await fetch(`https://dummyjson.com/products/${id}`, {
    method: 'DELETE',
  })
  if (!res.ok) throw new Error('Ошибка при удалении на сервере')
  return res.json()
}