// src/App.tsx
import { RegistrationForm } from './RegistrationForm'; // Проверь путь к файлу!

function App() {
  return (
    <div style={{ padding: '20px', fontFamily: 'sans-serif' }}>
      <h1>Лабораторная работа №6</h1>
      {/* Вызываем твой компонент */}
      <RegistrationForm />
    </div>
  );
}

export default App;