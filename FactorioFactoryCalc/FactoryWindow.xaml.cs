using FactorioFactoryCalc.Models;
using FactorioFactoryCalc.Services;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;

namespace FactorioFactoryCalc
{
    public partial class FactoryWindow : Window
    {
        private readonly RecipeManager _recipeManager;
        private ObservableCollection<string> _supportedRecipeTypes;

        public FactoryWindow(RecipeManager recipeManager)
        {
            InitializeComponent();
            _recipeManager = recipeManager;
            _supportedRecipeTypes = new ObservableCollection<string>();
            SupportedRecipeTypesListBox.ItemsSource = _supportedRecipeTypes;

            FactoryTypeComboBox.ItemsSource = _recipeManager.FactoryTypes;
            FactoryTypeComboBox.DisplayMemberPath = "Name";
        }

        private void AddRecipeType_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(RecipeTypeTextBox.Text))
            {
                _supportedRecipeTypes.Add(RecipeTypeTextBox.Text);
                RecipeTypeTextBox.Clear();
            }
        }

        private void RemoveRecipeType_Click(object sender, RoutedEventArgs e)
        {
            if (SupportedRecipeTypesListBox.SelectedItem is string selectedType)
            {
                _supportedRecipeTypes.Remove(selectedType);
            }
        }

        private void AddFactory_Click(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(CraftingSpeedMultiplierTextBox.Text, out decimal craftingSpeedMultiplier))
            {
                var factory = new Factory
                {
                    Name = FactoryNameTextBox.Text,
                    CraftingSpeedMultiplier = craftingSpeedMultiplier,
                    FactoryType = FactoryTypeComboBox.SelectedItem as FactoryType
                };

                _recipeManager.AddFactory(factory);
                ConfirmationTextBlock.Text = $"Factory '{factory.Name}' added successfully!";
                ClearInputs();
            }
            else
            {
                ConfirmationTextBlock.Text = "Please enter a valid crafting speed multiplier.";
                ConfirmationTextBlock.Foreground = Brushes.Red;
            }
        }

        private void ClearInputs()
        {
            FactoryNameTextBox.Clear();
            CraftingSpeedMultiplierTextBox.Clear();
            _supportedRecipeTypes.Clear();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
