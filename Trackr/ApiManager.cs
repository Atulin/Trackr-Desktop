using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Trackr
{
    public static class ApiManager
    {
        public static string categories = "http://erronisgames.com/tracker/api/categories.php";
        public static string news       = "http://erronisgames.com/tracker/api/news.php";
        public static string issues     = "http://erronisgames.com/tracker/api/issues.php";
        public static string newTicket  = "http://erronisgames.com/tracker/api/ticket.php";
        public static string register   = "http://erronisgames.com/tracker/api/register.php";
        public static string login      = "http://erronisgames.com/tracker/api/login.php";

        // Get categories
        public static Dictionary<string, string> GetCategories()
        {
            var json = new WebClient().DownloadString(categories);
            var cats = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            return cats;
        }
    }

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
    }

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
