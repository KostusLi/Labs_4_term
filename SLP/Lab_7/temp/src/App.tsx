import { useEffect } from 'react';
import { useAuth } from './AuthContext';
import { RouterProvider, createRouter } from '@tanstack/react-router';
import { routeTree } from './routeTree.gen';

const router = createRouter({ 
  routeTree, 
  context: { auth: undefined! } 
});

declare module '@tanstack/react-router' {
  interface Register {
    router: typeof router
  }
}

export default function App() {
  const auth = useAuth();

  useEffect(() => {
    router.invalidate();
  }, [auth.isAuthenticated]);

  return (
    <RouterProvider 
      router={router} 
      context={{ auth }}
    />
  );
}