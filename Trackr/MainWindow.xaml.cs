﻿using Newtonsoft.Json;
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
using Trackr;

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
        public ObservableCollection<Category> CollectionCategories { get; set; }

        // Hold user token
        public string Token = "";

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = this;

            // Load categories to dictionary
            Dictionary<string, string> cats = ApiManager.GetCategories();
            DictCategories = cats;

            // Load categories to ObservableCollection
            ObservableCollection<Category> Occ = new ObservableCollection<Category>();

            foreach (var cat in DictCategories)
            {
                Category newCat = new Category();
                newCat.ID = Int32.Parse(cat.Key);
                newCat.Name = cat.Value;
                newCat.Description = "";

                Occ.Add(newCat);
            }

            CollectionCategories = Occ;
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
            WindowTitle.Text = btn.Name.TrimToSentence();
        }

        // Handle sidebar buttons' mouse leave
        private void SidebarBtn_MouseLeave(object sender, MouseEventArgs e)
        {
            WindowTitle.Text = "Trackr.";
        }

        // Handle login window close
        private void LoginWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            LoginWindow.Visibility = Visibility.Collapsed;
        }

        // Handle login
        private void LoginAccept_Click(object sender, RoutedEventArgs e)
        {
            string token = Credentials.SendCredentials(LoginLogin.Text, LoginPassword.Password);

            try
            {
                Token = JsonConvert.DeserializeObject<Dictionary<string, string>>(token)["token"];
            }
            catch { }

            if(Token != "")
                UsernameTxt.Text = LoginLogin.Text;
        }

        // Open login window
        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow.Visibility = Visibility.Visible;
        }

        // Handle send ticket
        private void SendTicketBtn_Click(object sender, RoutedEventArgs e)
        {
            if(Token != "" && TicketCategory.Text != "" && TicketTitle.Text !="" && TicketBody.Text != "")
            {
                string response = Issue.SendTicket(Token, TicketCategory.Text, TicketTitle.Text, TicketBody.Text);

                WindowTitle.Text = TicketCategory.Text;
            }
            else
            {
                TicketBody.Text = "Don't leave me empty!";
            }

        }

        // Handle register close
        private void RegisterWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            RegisterWindow.Visibility = Visibility.Collapsed;
        }

        // Handle register
        private void RegisterAccept_Click(object sender, RoutedEventArgs e)
        {
            string response = Registration.SendCredentialsAsync(RegisterLogin.Text, RegisterPassword.Password, RegisterEmail.Text);
        }

        // Open registration panel
        private void OpenRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow.Visibility = Visibility.Visible;
            LoginWindow.Visibility = Visibility.Collapsed;
        }
    }
   
}
