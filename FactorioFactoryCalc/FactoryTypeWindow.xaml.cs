using FactorioFactoryCalc.Models;
using FactorioFactoryCalc.Services;
using System.Windows;

namespace FactorioFactoryCalc
{
    public partial class FactoryTypeWindow : Window
    {
        private readonly RecipeManager _recipeManager;

        public FactoryTypeWindow(RecipeManager recipeManager)
        {
            InitializeComponent();
            _recipeManager = recipeManager;
            FactoryTypesListBox.ItemsSource = _recipeManager.FactoryTypes;
            FactoryTypesListBox.DisplayMemberPath = "Name";
        }

        private void AddFactoryType_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(FactoryTypeNameTextBox.Text))
            {
                var factoryType = new FactoryType { Name = FactoryTypeNameTextBox.Text };
                _recipeManager.AddFactoryType(factoryType);
                ConfirmationTextBlock.Text = $"Factory Type '{factoryType.Name}' added successfully!";
                FactoryTypeNameTextBox.Clear();
            }
            else
            {
                ConfirmationTextBlock.Text = "Please enter a valid Factory Type name.";
                ConfirmationTextBlock.Foreground = System.Windows.Media.Brushes.Red;
            }
        }

        private void RemoveFactoryType_Click(object sender, RoutedEventArgs e)
        {
            if (FactoryTypesListBox.SelectedItem is FactoryType selectedFactoryType)
            {
                _recipeManager.FactoryTypes.Remove(selectedFactoryType);
                ConfirmationTextBlock.Text = $"Factory Type '{selectedFactoryType.Name}' removed successfully!";
            }
            else
            {
                ConfirmationTextBlock.Text = "Please select a Factory Type to remove.";
                ConfirmationTextBlock.Foreground = System.Windows.Media.Brushes.Red;
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
