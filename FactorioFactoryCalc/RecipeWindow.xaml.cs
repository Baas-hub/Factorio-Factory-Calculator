using FactorioFactoryCalc.Models;
using FactorioFactoryCalc.Services;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

namespace FactorioFactoryCalc
{
    public partial class RecipeWindow : Window
    {
        private readonly RecipeManager _recipeManager;
        private ObservableCollection<RecipeComponent> _recipeComponents;

        public RecipeWindow(RecipeManager recipeManager)
        {
            InitializeComponent();
            _recipeManager = recipeManager;
            _recipeComponents = new ObservableCollection<RecipeComponent>();
            IngredientsListBox.ItemsSource = _recipeComponents;
            InitializeComboBoxes();
        }

        private void InitializeComboBoxes()
        {
            IngredientComboBox.ItemsSource = _recipeManager.Ingredients;
            IngredientComboBox.DisplayMemberPath = "Name";

            RequiredFactoryTypeComboBox.ItemsSource = _recipeManager.FactoryTypes;
            RequiredFactoryTypeComboBox.DisplayMemberPath = "Name";
        }

        private void AddIngredientToRecipe_Click(object sender, RoutedEventArgs e)
        {
            if (IngredientComboBox.SelectedItem is Ingredient selectedIngredient &&
                int.TryParse(IngredientQuantityTextBox.Text, out int quantity))
            {
                _recipeComponents.Add(new RecipeComponent { Ingredient = selectedIngredient, Quantity = quantity });
                IngredientComboBox.SelectedIndex = -1;
                IngredientQuantityTextBox.Clear();
            }
            else
            {
                MessageBox.Show("Please select an ingredient and enter a valid quantity.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RemoveIngredientFromRecipe_Click(object sender, RoutedEventArgs e)
        {
            if (IngredientsListBox.SelectedItem is RecipeComponent selectedComponent)
            {
                _recipeComponents.Remove(selectedComponent);
            }
        }

        private void AddRecipe_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(OutputQuantityTextBox.Text, out int outputQuantity) &&
                decimal.TryParse(CraftingTimeTextBox.Text, out decimal craftingTime))
            {
                var recipe = new Recipe
                {
                    Name = RecipeNameTextBox.Text,
                    OutputQuantity = outputQuantity,
                    CraftingTime = craftingTime,
                    Components = new List<RecipeComponent>(_recipeComponents),
                    RequiredFactoryType = RequiredFactoryTypeComboBox.SelectedItem as FactoryType
                };
                _recipeManager.AddRecipe(recipe);

                var recipeOutput = new Ingredient
                {
                    Name = recipe.Name,
                    IsRawMaterial = false,
                    Recipe = recipe
                };
                _recipeManager.AddIngredient(recipeOutput);

                ConfirmationTextBlock.Text = $"Recipe '{recipe.Name}' added successfully and its output registered as an ingredient!";
                ClearInputs();
            }
            else
            {
                ConfirmationTextBlock.Text = "Please fill in all fields with valid values.";
                ConfirmationTextBlock.Foreground = Brushes.Red;
            }
        }

        private void ClearInputs()
        {
            RecipeNameTextBox.Clear();
            OutputQuantityTextBox.Clear();
            CraftingTimeTextBox.Clear();
            _recipeComponents.Clear();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
