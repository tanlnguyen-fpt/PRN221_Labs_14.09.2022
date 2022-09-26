using System.Windows;

namespace ManageCategoriesApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ManageCategories categories = new();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCategory();
        }

        private void LoadCategory()
        {
            lvCategories.ItemsSource = categories.GetCategories();
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            Category category = new() { CategoryName = txtCategoryName.Text };
            categories.InsertCategory(category);
            LoadCategory();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Category category = new() { CategoryName = txtCategoryName.Text, CategoryID = int.Parse(txtCategoryID.Text) };
            categories.UpdateCategory(category);
            LoadCategory();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            Category category = new() { CategoryID = int.Parse(txtCategoryID.Text) };
            categories.DeleteCategory(category);
            LoadCategory();
        }
    }
}
