import { createRootRouteWithContext, Link, Outlet, useNavigate } from '@tanstack/react-router'

import { useRecoilValue, useRecoilCallback, useRecoilState } from 'recoil';
import {uiSettingsState, cartState, favoritesState } from '../store/atom';
import {cartCountState} from '../store/selectors'

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

  const cartCount = useRecoilValue(cartCountState);
  const [uiSettings, setUiSettings] = useRecoilState(uiSettingsState);

  const resetAll = useRecoilCallback(({ reset }) => () => {
    reset(uiSettingsState);
    reset(cartState);
    reset(favoritesState);
    alert('Все настройки и корзина очищены!');
  });

  const handleLogout = () => {
    auth.logout();
    navigate({ to: '/login' });
  };

  return (
    <div style={{ 
      minHeight: '100vh',
      background: uiSettings.theme === 'dark' ? '#1a1a1a' : '#fff',
      color: uiSettings.theme === 'dark' ? '#eee' : '#000',
      transition: 'all 0.3s'
    }}>
      <nav style={{ 
        padding: '15px 30px', 
        background: uiSettings.theme === 'dark' ? '#2d2d2d' : '#fff', 
        borderBottom: `1px solid ${uiSettings.theme === 'dark' ? '#444' : '#eee'}`,
        display: 'flex',
        justifyContent: 'space-between',
        alignItems: 'center',
        position: 'sticky',
        top: 0,
        zIndex: 100
      }}>
        <div style={{ display: 'flex', gap: '20px', fontWeight: 500 }}>
          <Link to="/" className="[&.active]:font-bold">Главная</Link>
          <Link to="/catalog" className="[&.active]:font-bold">Каталог</Link>
          <Link to="/cart" className="[&.active]:font-bold" style={{ position: 'relative' }}>
            Корзина
            {cartCount > 0 && (
              <span style={{
                position: 'absolute',
                top: '-8px',
                right: '-12px',
                background: '#ff4757',
                color: 'white',
                borderRadius: '50%',
                padding: '2px 6px',
                fontSize: '10px',
                fontWeight: 'bold'
              }}>
                {cartCount}
              </span>
            )}
          </Link>
        </div>

        <div style={{ display: 'flex', alignItems: 'center', gap: '15px' }}>
          <button 
            onClick={resetAll} 
            style={{ 
              fontSize: '12px', 
              background: 'none', 
              border: uiSettings.theme === 'dark' ? '1px solid #666' : '1px solid #999', 
              color: uiSettings.theme === 'dark' ? '#bbb' : '#555', // Цвет текста кнопки
              cursor: 'pointer', 
              padding: '2px 5px' 
            }}
          >
            Сбросить всё
          </button>

          {auth.isAuthenticated ? (
            <>
              <span style={{ fontSize: '14px', color: uiSettings.theme === 'dark' ? '#eee' : '#000' }}>
                User: <b>{auth.user?.username}</b>
              </span>
              <button onClick={handleLogout} style={{ padding: '5px 10px', fontSize: '12px' }}>Выйти</button>
            </>
          ) : (
            <Link to="/login" style={{ color: uiSettings.theme === 'dark' ? '#eee' : '#000' }}>Войти</Link>
          )}
        </div>
      </nav>
      
      <main style={{ maxWidth: '1200px', margin: '0 auto', width: '100%', padding: '20px' }}>
        <Outlet />
      </main>
    </div>
  )
}