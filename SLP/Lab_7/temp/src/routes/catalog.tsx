import { createFileRoute, redirect } from '@tanstack/react-router'
import { CatalogPage } from '../CatalogPage'
import { z } from 'zod'

// 1. Схема для валидации параметров поиска
const productSearchSchema = z.object({
  category: z.string().optional().catch(''), // Если в URL чепуха — вернет пустую строку
})

export const Route = createFileRoute('/catalog')({
  // 2. Внедряем валидацию Search Params
  validateSearch: (search) => productSearchSchema.parse(search),
  
  beforeLoad: ({ context }) => {
    if (!context.auth.isAuthenticated) {
      throw redirect({ to: '/login' })
    }
  },
  component: CatalogPage,
})