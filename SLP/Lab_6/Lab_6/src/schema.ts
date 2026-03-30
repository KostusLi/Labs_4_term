import {z} from 'zod';

export const RegistrationSchema = z.object(
    {
        email: z
            .string()
            .email("Неверный формат email")
            .min(1, "Email обязателен"),

        password: z 
            .string()
            .min(8, "Пароль должен быть минимум 8 символов"),

        username: z
            .string()
            .min(1, "Введите имя пользователя"),

        city: z
            .string()
            .min(1, "Выберите или введите город"),

        occupation: z
            .string()
            .min(1, "Выберите род деятельности"),

        acceptedTerms: z
            .boolean()
            .refine((val) => val === true, {
            message: "Вы должны согласиться с правилами",
            }),
    }
);


export type IFormData = z.infer<typeof RegistrationSchema>;


export const step1Schema = RegistrationSchema.pick({
    email: true,
    password: true,
});

export const step2Schema = RegistrationSchema.pick({
    username: true,
    city: true,
});

export const step3Schema = RegistrationSchema.pick({
    occupation: true,
    acceptedTerms: true,
});
