import { createFileRoute, redirect } from '@tanstack/react-router'
import { CatalogPage } from '../CatalogPage'
import { z } from 'zod'

const productSearchSchema = z.object({
  category: z.string().optional().catch(''),
})

export const Route = createFileRoute('/catalog')({
  validateSearch: (search) => productSearchSchema.parse(search),
  
  beforeLoad: ({ context }) => {
    if (!context.auth.isAuthenticated) {
      throw redirect({ to: '/login' })
    }
  },
  component: CatalogPage,
})