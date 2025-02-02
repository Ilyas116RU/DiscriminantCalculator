using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Input;

public class MainViewModel : INotifyPropertyChanged
{
    private string _a = "1";
    private string _b = "2";
    private string _c = "1";
    private string _resultMessage = "Введите коэффициенты...";
    private string _calculationDetails = "";

    public MainViewModel()
    {
        PropertyChanged = delegate { };
    }

    public string A
    {
        get => _a;
        set { _a = value; OnPropertyChanged(nameof(A)); }
    }

    public string B
    {
        get => _b;
        set { _b = value; OnPropertyChanged(nameof(B)); }
    }

    public string C
    {
        get => _c;
        set { _c = value; OnPropertyChanged(nameof(C)); }
    }

    public string ResultMessage
    {
        get => _resultMessage;
        set { _resultMessage = value; OnPropertyChanged(nameof(ResultMessage)); }
    }

    public string CalculationDetails
    {
        get => _calculationDetails;
        set { _calculationDetails = value; OnPropertyChanged(nameof(CalculationDetails)); }
    }

    public ICommand CalculateCommand => new RelayCommand(Calculate);

    private void Calculate(object? parameter)
    {
        try
        {
            if (!TryParseCoefficients(out double a, out double b, out double c)) return;

            if (a == 0)
            {
                SolveLinearEquation(b, c);
                return;
            }

            SolveQuadraticEquation(a, b, c);
        }
        catch (Exception ex)
        {
            ResultMessage = $"Ошибка: {ex.Message}";
            CalculationDetails = string.Empty;
        }
    }

    private bool TryParseCoefficients(out double a, out double b, out double c)
    {
        a = 0;
        b = 0;
        c = 0;

        if (!double.TryParse(A, NumberStyles.Any, CultureInfo.InvariantCulture, out a) ||
            !double.TryParse(B, NumberStyles.Any, CultureInfo.InvariantCulture, out b) ||
            !double.TryParse(C, NumberStyles.Any, CultureInfo.InvariantCulture, out c))
        {
            ResultMessage = "Ошибка ввода! Введите числа";
            CalculationDetails = string.Empty;
            return false;
        }
        return true;
    }

    private void SolveLinearEquation(double b, double c)
    {
        if (b == 0)
        {
            ResultMessage = c == 0 ?
                "Бесконечное количество решений" :
                "Нет решений";
            CalculationDetails = "Уравнение вырождено в линейное: 0x + 0 = 0";
            return;
        }

        double root = -c / b;
        ResultMessage = $"Линейное уравнение. Корень: {root:F2}";
        CalculationDetails = $"Решение линейного уравнения:\n{b:F2}x + {c:F2} = 0 ⇒ x = {-c:F2}/{b:F2} = {root:F2}";
    }

    private void SolveQuadraticEquation(double a, double b, double c)
    {
        double discriminant = b * b - 4 * a * c;
        string details = $"D = b² - 4ac = {b:F2}² - 4*{a:F2}*{c:F2} = {discriminant:F2}\n";

        if (discriminant > 0)
        {
            double sqrtD = Math.Sqrt(discriminant);
            double x1 = (-b + sqrtD) / (2 * a);
            double x2 = (-b - sqrtD) / (2 * a);
            ResultMessage = $"Два действительных корня: x₁ = {x1:F2}, x₂ = {x2:F2}";
            details += $"x₁ = (-b + √D)/(2a) = ({-b:F2} + {sqrtD:F2})/{2 * a:F2} = {x1:F2}\n" +
                       $"x₂ = (-b - √D)/(2a) = ({-b:F2} - {sqrtD:F2})/{2 * a:F2} = {x2:F2}";
        }
        else if (discriminant == 0)
        {
            double x = -b / (2 * a);
            ResultMessage = $"Один действительный корень: x = {x:F2}";
            details += $"x = -b/(2a) = {-b:F2}/{(2 * a):F2} = {x:F2}";
        }
        else
        {
            ResultMessage = "Действительных корней нет";
            details += "Так как D < 0, уравнение имеет комплексные корни";
        }

        CalculationDetails = details;
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged(string propertyName) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}

public class RelayCommand : ICommand
{
    private readonly Action<object?> _execute;

    public RelayCommand(Action<object?> execute) => _execute = execute;

    public bool CanExecute(object? parameter) => true;

    public void Execute(object? parameter) => _execute(parameter);

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }
}