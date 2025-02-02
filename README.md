
# Калькулятор квадратных уравнений

![Скриншот приложения](https://avatars.mds.yandex.net/get-images-cbir/4823862/Y1zk0dGZbGSdtQqwFZC4-g2589/preview) <!-- Замените ссылку на реальный скриншот -->

Простое WPF-приложение для решения квадратных уравнений с подробным отображением этапов вычисления. Реализовано с использованием паттерна MVVM и модульным тестированием.

## 🚀 Особенности

- 📐 Решение квадратных и линейных уравнений
- 📊 Подробный пошаговый расчет дискриминанта
- 🛠️ Обработка всех особых случаев:
  - Нулевые коэффициенты
  - Вырожденные уравнения
  - Комплексные корни
- ✅ Валидация ввода
- 📝 Логирование вычислений
- 🧪 Полный набор xUnit тестов

## 💻 Технологии

- .NET 6
- WPF
- MVVM Pattern
- xUnit
- LiveCharts (для будущих расширений)

## 📦 Установка

1. Клонировать репозиторий:
```bash
git clone https://github.com/yourusername/discriminant-calculator.git
```

2. Собрать решение в Visual Studio 2022:
```bash
msbuild DiscriminantCalculator.sln /p:Configuration=Release
```

## 🎮 Использование

1. Введите коэффициенты:
```
a = 2
b = -11
c = 5
```

2. Нажмите "Рассчитать"

3. Получите результат:
```
Два действительных корня: x₁ = 5.00, x₂ = 0.50

Этапы решения:
D = b² - 4ac = (-11.00)² - 4*2.00*5.00 = 81.00
√D = 9.00
x₁ = (-b + √D)/(2a) = (11.00 + 9.00)/4.00 = 5.00
x₂ = (-b - √D)/(2a) = (11.00 - 9.00)/4.00 = 0.50
```

## 🧪 Тестирование
Запуск всех тестов:
```bash
dotnet test --filter "Category=Unit"
```

Пример теста:
```csharp
[Fact]
[Category("Unit")]
public void TestTwoRealRoots()
{
    var vm = new MainViewModel {
        A = "1",
        B = "-5",
        C = "6"
    };
    
    vm.CalculateCommand.Execute(null);
    
    Assert.Contains("x₁ = 3.00", vm.ResultMessage);
    Assert.Contains("x₂ = 2.00", vm.ResultMessage);
}
```

## 📄 Лицензия
MIT License. Подробнее в файле [LICENSE](LICENSE).

## 🤝 Как помочь проекту
1. Форкните репозиторий
2. Создайте ветку для новой фичи (`git checkout -b feature/amazing-feature`)
3. Сделайте коммит изменений (`git commit -m 'Add some amazing feature'`)
4. Запушьте ветку (`git push origin feature/amazing-feature`)
5. Откройте Pull Request

---

*Разработано с использованием лучших практик WPF разработки. Для вопросов и предложений: (mailto:frostastoru22998@mail.ru)*
```
