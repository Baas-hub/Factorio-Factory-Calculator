using FactorioFactoryCalc.Models;
using FactorioFactoryCalc.Services;
using System.Windows;

namespace FactorioFactoryCalc
{
    public partial class IngredientWindow : Window
    {
        private readonly RecipeManager _recipeManager;

        public IngredientWindow(RecipeManager recipeManager)
        {
            InitializeComponent();
            _recipeManager = recipeManager;
        }

        private void AddIngredient_Click(object sender, RoutedEventArgs e)
        {
            var ingredient = new Ingredient
            {
                Name = IngredientNameTextBox.Text,
                IsRawMaterial = IsRawMaterialCheckBox.IsChecked ?? false
            };
            _recipeManager.AddIngredient(ingredient);
            ConfirmationTextBlock.Text = $"Ingredient '{ingredient.Name}' added successfully!";
            IngredientNameTextBox.Clear();
            IsRawMaterialCheckBox.IsChecked = false;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
