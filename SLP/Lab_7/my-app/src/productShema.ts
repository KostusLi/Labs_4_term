import { z } from 'zod';

export const ProductSchema = z.object({
  id: z.number(),
  title: z.string().min(3, "Название должно быть минимум 3 символа"),
  price: z.coerce.number({ message: "Цена должна быть числом" }).positive("Цена > 0"),
});

export type IProduct = z.infer<typeof ProductSchema>;