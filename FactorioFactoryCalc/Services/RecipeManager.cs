using FactorioFactoryCalc.Models;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace FactorioFactoryCalc.Services
{
    public class RecipeManager
    {
        public ObservableCollection<Ingredient> Ingredients { get; set; }
        public ObservableCollection<Recipe> Recipes { get; set; }
        public ObservableCollection<Factory> Factories { get; set; }
        public ObservableCollection<FactoryType> FactoryTypes { get; set; }

        public RecipeManager()
        {
            Ingredients = new ObservableCollection<Ingredient>();
            Recipes = new ObservableCollection<Recipe>();
            Factories = new ObservableCollection<Factory>();
            FactoryTypes = new ObservableCollection<FactoryType>();
        }

        public void AddIngredient(Ingredient ingredient)
        {
            Ingredients.Add(ingredient);
        }

        public void AddRecipe(Recipe recipe)
        {
            Recipes.Add(recipe);
        }

        public void AddFactory(Factory factory)
        {
            Factories.Add(factory);
        }

        public void AddFactoryType(FactoryType factoryType)
        {
            FactoryTypes.Add(factoryType);
        }

        public Dictionary<string, decimal> CalculateRequiredFactories(Recipe recipe, int desiredOutputPerMinute, Dictionary<string, Factory> factorySelections)
        {
            var result = new Dictionary<string, decimal>();
            CalculateRequiredFactoriesRecursive(recipe, desiredOutputPerMinute, factorySelections, result);
            return result;
        }

        private void CalculateRequiredFactoriesRecursive(Recipe recipe, decimal requiredOutputPerMinute, Dictionary<string, Factory> factorySelections, Dictionary<string, decimal> result)
        {
            if (!factorySelections.TryGetValue(recipe.RequiredFactoryType.Name, out var factory))
            {
                throw new InvalidOperationException($"No factory selected for type {recipe.RequiredFactoryType.Name} required by recipe {recipe.Name}");
            }

            decimal itemsPerCraftingCycle = recipe.OutputQuantity;
            decimal craftingCyclesPerMinute = 60 / recipe.CraftingTime;
            decimal itemsProducedPerMinute = itemsPerCraftingCycle * craftingCyclesPerMinute * factory.CraftingSpeedMultiplier;

            decimal requiredFactories = requiredOutputPerMinute / itemsProducedPerMinute;

            if (result.ContainsKey(factory.Name))
            {
                result[factory.Name] += requiredFactories;
            }
            else
            {
                result[factory.Name] = requiredFactories;
            }

            foreach (var component in recipe.Components)
            {
                if (component.Ingredient.Recipe != null)
                {
                    decimal requiredInputPerMinute = (requiredOutputPerMinute / recipe.OutputQuantity) * component.Quantity;
                    CalculateRequiredFactoriesRecursive(component.Ingredient.Recipe, requiredInputPerMinute, factorySelections, result);
                }
            }
        }
    }
}
