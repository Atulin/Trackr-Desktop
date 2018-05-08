using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Trackr
{
    public static class ApiManager
    {
        public static string _categories = "http://erronisgames.com/tracker/api/categories.php";
        public static string _news       = "http://erronisgames.com/tracker/api/news.php";
        public static string _issues     = "http://erronisgames.com/tracker/api/issues.php";
        public static string _newTicket  = "http://erronisgames.com/tracker/api/ticket.php";
        public static string _register   = "http://erronisgames.com/tracker/api/register.php";
        public static string _login      = "http://erronisgames.com/tracker/api/login.php";

        // Get categories
        public static Dictionary<string, string> GetCategories()
        {
            var json = new WebClient().DownloadString(_categories);
            var cats = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            return cats;
        }
    }


    //==================
    // Issues
    //==================

    public class Issue
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime Date { get; set; }
        public int Score { get; set; }

        public string Category { get; set; }
        public string Severity { get; set; }
        public string Author { get; set; }
        public string Developer { get; set; }

        public Issue(int id, string title, string body, DateTime date, int score, string category, string severity, string author, string developer)
        {
            ID = id;
            Title = title;
            Body = body;
            Date = date;
            Score = score;
            Category = category;
            Severity = severity;
            Author = author;
            Developer = developer;
        }

        // Send credentials
        public static string SendTicket(string token, string category, string title, string body)
        {
            var client = new RestClient(ApiManager._newTicket);

            var request = new RestRequest("resource/{id}", Method.POST);
            request.AddParameter("token", token);
            request.AddParameter("cat", category);
            request.AddParameter("title", title);
            request.AddParameter("body", body);

            IRestResponse response = client.Execute(request);
            var content = response.Content; // raw content as string

            return content;
        }
    }


    //=====================
    // Credentials
    //=====================

    public class Credentials
    {
        [JsonProperty("login")]
        public string Login { get; set; }

        [JsonProperty("pass")]
        public string Password { get; set; }

        // Send credentials
        public static string SendCredentials(string login, string password)
        {
            var client = new RestClient(ApiManager._login);

            var request = new RestRequest("resource/{id}", Method.POST);
            request.AddParameter("login", login);
            request.AddParameter("password", password);

            IRestResponse response = client.Execute(request);
            var content = response.Content; // raw content as string

            return content;
        }
    }


    //=====================
    // Registration data
    //=====================
    public class Registration
    {
        // Send credentials
        public static string SendCredentialsAsync(string login, string password, string email)
        {
            var client = new RestClient(ApiManager._register);

            var request = new RestRequest("resource/{id}", Method.POST);
            request.AddParameter("login", login);
            request.AddParameter("password", password);
            request.AddParameter("email", email);

            IRestResponse response = client.Execute(request);
            var content = response.Content; // raw content as string

            return content;
        }

    }


    //===========
    // Categories
    //===========

    public class Category : INotifyPropertyChanged
    {
        // boiler-plate
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, string propertyName)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        // ID
        private int id;
        public int ID
        {
            get => id;
            set => SetField(ref id, value, "ID");
        }

        // Name
        private string name;
        public string Name
        {
            get => name;
            set => SetField(ref name, value, "Name");
        }

        // Description
        private string description;
        public string Description
        {
            get => description;
            set => SetField(ref description, value, "Description");
        }        
    }

}
