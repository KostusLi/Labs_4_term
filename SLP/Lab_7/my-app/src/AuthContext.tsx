import { createContext, useContext, useState, useEffect, type ReactNode } from 'react';

// Используем тип данных из твоей 6-й лабы
interface IUserData {
  email: string;
  username: string;
}

interface IAuthContext {
  user: IUserData | null;
  isAuthenticated: boolean;
  login: (userData: any) => void;
  logout: () => void;
}

const AuthContext = createContext<IAuthContext | undefined>(undefined);

export const AuthProvider = ({ children }: { children: ReactNode }) => {
  const [user, setUser] = useState<IUserData | null>(null);

  // Проверка при загрузке: есть ли юзер в памяти браузера?
  useEffect(() => {
    const saved = localStorage.getItem('user');
    if (saved) setUser(JSON.parse(saved));
  }, []);

  const login = (userData: any) => {
    setUser(userData);
    localStorage.setItem('user', JSON.stringify(userData));
  };

  const logout = () => {
    setUser(null);
    localStorage.removeItem('user');
  };

  return (
    <AuthContext.Provider value={{ user, isAuthenticated: !!user, login, logout }}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => {
  const context = useContext(AuthContext);
  if (!context) throw new Error('useAuth error');
  return context;
};