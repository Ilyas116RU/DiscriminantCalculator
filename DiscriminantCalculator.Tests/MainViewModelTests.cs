using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Globalization;


namespace DiscriminantCalculator
{
    public class MainViewModelTests
    {
        private readonly MainViewModel _viewModel;

        public MainViewModelTests()
        {
            _viewModel = new MainViewModel();
        }

        [Fact]
        public void SolveQuadraticEquation_TwoRealRoots()
        {
            // Arrange
            _viewModel.A = "1";
            _viewModel.B = "-5";
            _viewModel.C = "6";

            // Act
            _viewModel.CalculateCommand.Execute(null);

            // Assert
            Assert.Contains("Два действительных корня", _viewModel.ResultMessage);
            Assert.Contains("x₁ = 3,00", _viewModel.ResultMessage);
            Assert.Contains("x₂ = 2,00", _viewModel.ResultMessage);
            Assert.Contains("D = b² - 4ac = -5,00² - 4*1,00*6,00 = 1,00", _viewModel.CalculationDetails);
        }

        [Fact]
        public void SolveQuadraticEquation_OneRealRoot()
        {
            // Arrange
            _viewModel.A = "1";
            _viewModel.B = "2";
            _viewModel.C = "1";

            // Act
            _viewModel.CalculateCommand.Execute(null);

            // Assert
            Assert.Contains("Один действительный корень: x = -1,00", _viewModel.ResultMessage);
            Assert.Contains("D = b² - 4ac = 2,00² - 4*1,00*1,00 = 0,00", _viewModel.CalculationDetails);
        }

        [Fact]
        public void SolveQuadraticEquation_NoRealRoots()
        {
            // Arrange
            _viewModel.A = "1";
            _viewModel.B = "1";
            _viewModel.C = "1";

            // Act
            _viewModel.CalculateCommand.Execute(null);

            // Assert
            Assert.Equal("Действительных корней нет", _viewModel.ResultMessage);
            Assert.Contains("D = b² - 4ac = 1,00² - 4*1,00*1,00 = -3,00", _viewModel.CalculationDetails);
        }

        [Fact]
        public void SolveLinearEquation_Valid()
        {
            // Arrange
            _viewModel.A = "0";
            _viewModel.B = "2";
            _viewModel.C = "-4";

            // Act
            _viewModel.CalculateCommand.Execute(null);

            // Assert
            Assert.Contains("Линейное уравнение. Корень: 2,00", _viewModel.ResultMessage);
            Assert.Contains("2,00x + -4,00 = 0 ⇒ x = 4,00/2,00 = 2,00", _viewModel.CalculationDetails);
        }

        [Fact]
        public void SolveLinearEquation_NoSolutions()
        {
            // Arrange
            _viewModel.A = "0";
            _viewModel.B = "0";
            _viewModel.C = "5";

            // Act
            _viewModel.CalculateCommand.Execute(null);

            // Assert
            Assert.Equal("Нет решений", _viewModel.ResultMessage);
        }

        [Fact]
        public void SolveLinearEquation_InfiniteSolutions()
        {
            // Arrange
            _viewModel.A = "0";
            _viewModel.B = "0";
            _viewModel.C = "0";

            // Act
            _viewModel.CalculateCommand.Execute(null);

            // Assert
            Assert.Equal("Бесконечное количество решений", _viewModel.ResultMessage);
        }

        [Fact]
        public void InvalidInput_ShowsErrorMessage()
        {
            // Arrange
            _viewModel.A = "abc";
            _viewModel.B = "2";
            _viewModel.C = "3";

            // Act
            _viewModel.CalculateCommand.Execute(null);

            // Assert
            Assert.Equal("Ошибка ввода! Введите числа", _viewModel.ResultMessage);
        }

        [Fact]
        public void CultureInvariantParsing_WorksWithDot()
        {
            // Arrange
            _viewModel.A = "1.5";
            _viewModel.B = "2,5"; // Оба разделителя должны работать
            _viewModel.C = "3";

            // Act
            _viewModel.CalculateCommand.Execute(null);

            // Assert
            Assert.DoesNotContain("Ошибка ввода", _viewModel.ResultMessage);
        }
    }

}
