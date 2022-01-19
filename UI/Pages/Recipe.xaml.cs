using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UI.Pages
{
    /// <summary>
    /// Interaction logic for Recipe.xaml
    /// </summary>
    public partial class Recipe : Page
    {
        Appp app = new Appp();
        static List<string> products;
        static List<string> printProducts;
        static string selected_item;
        static string recipe_name;
        public Recipe()
        {
            InitializeComponent();
            List<string> _recipes = app.GetRecipes();
            recipes.ItemsSource = _recipes;
        }
        private void Grid_Click(object sender, RoutedEventArgs e)
        {
            var ClickedButton = e.OriginalSource as NavButton;
            NavigationService.Navigate(ClickedButton.NavUri);
        }

        private void Grid_Click2(object sender, RoutedEventArgs e)
        {
            recipe_name = recipes.SelectedItem.ToString();
            var ClickedButton = e.OriginalSource as AddButton;
            NavigationService.Navigate(ClickedButton.NavUri);
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        public string getRecipeName()
        {
            return recipe_name;
        }
        public List<string> getValues()
        {
            //app.UpdateShoppingList();
            printProducts = app.PrintShoppingList();
            return printProducts;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddButton_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string product;
            string q;
            selected_item = recipes.SelectedItem.ToString();
            products = app.PrintShoppingProducts(selected_item);
            if (products.Count == 0)
            {
                MessageBox.Show("You have consumed this recipe!");
                app.ConsumeRecipe(recipes.SelectedItem.ToString());
            }
            else if (products.Count > 0)
            {
                MessageBox.Show("You added products to shopping list!");
                foreach(string prod in products)
                {
                    product = prod.Split()[0];
                    q = prod.Split()[1];
                    q = q.Remove(q.Length - 1);
                    if (app.CheckProductsInShoppingList(product) == false)
                    {
                        app.AddShoppingList(q, product);
                    }
                    else if (app.CheckProductsInShoppingList(product) == true)
                    {
                        app.AddQuantityToShoppingList(product, q);
                    }
                }
            }
            printProducts = products;
        }
        public void RefreshShopList()
        {
            //app.UpdateShoppingList();
            printProducts = app.PrintShoppingList();
        }
        private void Grid_Click3(object sender, RoutedEventArgs e)
        {
            var ClickedButton = e.OriginalSource as AddButton;
            NavigationService.Navigate(ClickedButton.NavUri);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            List<string> x;
            if (app.IfRecipeExists(search.Text) == true)
            {
                x = app.SearchRecipe(search.Text);
                recipes.ItemsSource = x;
                search.Text = "";
            }
            else if (search.Text == "")
            {
                List<string> _recipes = app.GetRecipes();
                recipes.ItemsSource = _recipes;
            }
            else if (app.IfRecipeExists(search.Text) == false)
            {
                MessageBox.Show("Recipe not found!");
                search.Text = "";
            }
        }
    }
}
