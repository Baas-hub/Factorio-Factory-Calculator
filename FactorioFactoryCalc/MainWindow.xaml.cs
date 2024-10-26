using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FactorioFactoryCalc.Models;
using FactorioFactoryCalc.Services;

namespace FactorioFactoryCalc
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public RecipeManager RecipeManager { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            RecipeManager = new RecipeManager();
        }

        private void OpenIngredientWindow_Click(object sender, RoutedEventArgs e)
        {
            var ingredientWindow = new IngredientWindow(RecipeManager);
            ingredientWindow.Show();
        }

        private void OpenRecipeWindow_Click(object sender, RoutedEventArgs e)
        {
            var recipeWindow = new RecipeWindow(RecipeManager);
            recipeWindow.Show();
        }

        private void OpenFactoryWindow_Click(object sender, RoutedEventArgs e)
        {
            var factoryWindow = new FactoryWindow(RecipeManager);
            factoryWindow.Show();
        }

        private void OpenCalculationWindow_Click(object sender, RoutedEventArgs e)
        {
            var calculationWindow = new CalculationWindow(RecipeManager);
            calculationWindow.Show();
        }

        private void OpenFactoryTypeWindow_Click(object sender, RoutedEventArgs e)
        {
            var factoryTypeWindow = new FactoryTypeWindow(RecipeManager);
            factoryTypeWindow.Show();
        }
    }
}
