using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
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

namespace Trackr
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Hold categories dictionary
        public Dictionary<string, string> DictCategories;

        // Hold categories collection
        public ObservableCollection<Category> CollectionCategories = new ObservableCollection<Category>();

        public MainWindow()
        {
            InitializeComponent();

            // Load categories to dictionary
            Dictionary<string, string> cats = ApiManager.GetCategories();
            DictCategories = cats;

            // Load categories to ObservableCollection
            foreach(var cat in DictCategories)
            {
                Category newCat = new Category();
                newCat.ID = Int32.Parse(cat.Key);
                newCat.Name = cat.Value;
                newCat.Description = "";

                CollectionCategories.Add(newCat);
            }
        }

        // Handle app close
        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        
        // Handle window drag
        private void Titlebar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        // Handle Home button
        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            MainTabs.SelectedIndex = 0;
        }

        // Handle Issues button
        private void IssuesBtn_Click(object sender, RoutedEventArgs e)
        {
            MainTabs.SelectedIndex = 1;

            string txt = "";

            foreach (var cat in CollectionCategories)
            {
                txt += cat.ID + ":" + cat.Name + Environment.NewLine;
            }

            dbg.Text = txt;
        }

        // Handle New Issues button
        private void NewIssueBtn_Click(object sender, RoutedEventArgs e)
        {
            MainTabs.SelectedIndex = 2;
        }

        // Handle sidebar buttons' mouse enter
        private void SidebarBtn_MouseEnter(object sender, MouseEventArgs e)
        {
            Button btn = sender as Button;
            Title.Text = btn.Name.TrimToSentence();
        }

        // Handle sidebar buttons' mouse leave
        private void SidebarBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            Title.Text = "Trackr.";
        }
    }
   
}
