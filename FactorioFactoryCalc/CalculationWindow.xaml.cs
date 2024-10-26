using FactorioFactoryCalc.Models;
using FactorioFactoryCalc.Services;
using System.Text;
using System.Windows;
using System.Collections.ObjectModel;

namespace FactorioFactoryCalc
{
    public partial class CalculationWindow : Window
    {
        private readonly RecipeManager _recipeManager;
        private ObservableCollection<FactoryTypeSelection> _factoryTypeSelections;

        public CalculationWindow(RecipeManager recipeManager)
        {
            InitializeComponent();
            _recipeManager = recipeManager;
            RecipeComboBox.ItemsSource = _recipeManager.Recipes;
            RecipeComboBox.DisplayMemberPath = "Name";

            _factoryTypeSelections = new ObservableCollection<FactoryTypeSelection>();
            foreach (var factoryType in _recipeManager.FactoryTypes)
            {
                _factoryTypeSelections.Add(new FactoryTypeSelection
                {
                    Name = factoryType.Name,
                    Factories = new ObservableCollection<Factory>(_recipeManager.Factories.Where(f => f.FactoryType == factoryType)),
                    SelectedFactory = null
                });
            }
            FactoryTypeSelectionItemsControl.ItemsSource = _factoryTypeSelections;
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            if (RecipeComboBox.SelectedItem is Recipe selectedRecipe &&
                int.TryParse(DesiredOutputTextBox.Text, out int desiredOutput))
            {
                var factorySelections = _factoryTypeSelections.ToDictionary(fts => fts.Name, fts => fts.SelectedFactory);
                
                try
                {
                    var requiredFactories = _recipeManager.CalculateRequiredFactories(selectedRecipe, desiredOutput, factorySelections);
                    StringBuilder resultText = new StringBuilder();
                    resultText.AppendLine($"Required factories for {desiredOutput} {selectedRecipe.Name} per minute:");
                    foreach (var kvp in requiredFactories)
                    {
                        resultText.AppendLine($"- {kvp.Key}: {kvp.Value:F2}");
                    }
                    ResultTextBlock.Text = resultText.ToString();
                }
                catch (InvalidOperationException ex)
                {
                    ResultTextBlock.Text = $"Error: {ex.Message}";
                }
            }
            else
            {
                ResultTextBlock.Text = "Please select a recipe, enter a valid desired output, and select factories for all types.";
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }

    public class FactoryTypeSelection
    {
        public string Name { get; set; }
        public ObservableCollection<Factory> Factories { get; set; }
        public Factory SelectedFactory { get; set; }
    }
}
