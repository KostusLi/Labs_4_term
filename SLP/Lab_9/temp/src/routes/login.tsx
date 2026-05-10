import { createFileRoute, redirect } from '@tanstack/react-router'
import { RegistrationForm } from '../RegistrationForm'

export const Route = createFileRoute('/login')({
  beforeLoad: ({ context }) => {
    if (context.auth.isAuthenticated) {
      throw redirect({ to: '/catalog' })
    }
  },
  component: () => (
    <div style={{ padding: '20px' }}>
      <h1>Добро пожаловать!</h1>
      <RegistrationForm />
    </div>
  ),
})