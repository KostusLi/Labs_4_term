import { AuthProvider, useAuth } from './AuthContext';
import { ProductProvider } from './ProductContext';
import { RegistrationForm } from './RegistrationForm';
import { CatalogPage } from './CatalogPage';

const Root = () => {
  const { isAuthenticated } = useAuth();

  return (
    <>
      {isAuthenticated ? (
        <ProductProvider>
          <CatalogPage />
        </ProductProvider>
      ) : (
        <RegistrationForm />
      )}
    </>
  );
};

export default function App() {
  return (
    <AuthProvider>
      <Root />
    </AuthProvider>
  );
}