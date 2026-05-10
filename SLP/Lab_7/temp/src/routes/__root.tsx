import { createRootRouteWithContext, Link, Outlet, useNavigate } from '@tanstack/react-router'
import { useAuth } from '../AuthContext'

interface MyRouterContext {
  auth: { 
    isAuthenticated: boolean,
    user: { username: string } | null,
    logout: () => void 
  }
}

export const Route = createRootRouteWithContext<MyRouterContext>()({
  component: RootComponent,
})

function RootComponent() {
  const { auth } = Route.useRouteContext();
  const navigate = useNavigate();

  const handleLogout = () => {
    auth.logout();
    navigate({ to: '/login' });
  };

  return (
    <>
      <nav style={{ 
        padding: '15px 30px', 
        background: 'var(--bg)', 
        borderBottom: '1px solid var(--border)',
        display: 'flex',
        justifyContent: 'space-between',
        alignItems: 'center',
        position: 'sticky',
        top: 0,
        zIndex: 100
      }}>
        <div style={{ display: 'flex', gap: '20px', fontWeight: 500 }}>
          <Link to="/" className="[&.active]:color-var(--accent)">Главная</Link>
          <Link to="/catalog" className="[&.active]:color-var(--accent)">Каталог</Link>
        </div>

        {auth.isAuthenticated ? (
          <div style={{ display: 'flex', alignItems: 'center', gap: '15px' }}>
            <span style={{ fontSize: '14px', color: 'var(--text)' }}>
            <b>{auth.user?.username}</b>
            </span>
          </div>
        ) : (
          <Link to="/login">Войти</Link>
        )}
      </nav>
      
      <main style={{ maxWidth: '1200px', margin: '0 auto', width: '100%' }}>
        <Outlet />
      </main>
    </>
  )
}