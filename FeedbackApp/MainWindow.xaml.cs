using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace FeedbackApp
{
    public partial class MainWindow : Window
    {
        private ObservableCollection<Person> persons;
        private Person selectedPerson;
        private const string FilePath = "persons.json";

        public MainWindow()
        {
            InitializeComponent();
            LoadPersons();
            PersonsListBox.ItemsSource = persons;
        }

        private void LoadPersons()
        {
            if (File.Exists(FilePath))
            {
                var json = File.ReadAllText(FilePath);
                persons = JsonConvert.DeserializeObject<ObservableCollection<Person>>(json);
            }
            else
            {
                persons = new ObservableCollection<Person>();
            }
        }

        private void SavePersons()
        {
            var json = JsonConvert.SerializeObject(persons, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(FilePath, json);
        }

        private void PersonsListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (PersonsListBox.SelectedItem is Person person)
            {
                selectedPerson = person;
                NameAbbreviationTextBox.Text = person.NameAbbreviation;
                FirstNameTextBox.Text = person.FirstName;
                LastNameTextBox.Text = person.LastName;
                PositionTextBox.Text = person.Position;
                TeamTextBox.Text = person.Team;
                FeedbackTextBox.Text = person.Feedback;
            }
        }

        private void AddPersonButton_Click(object sender, RoutedEventArgs e)
        {
                var newPerson = new Person { NameAbbreviation = NameAbbreviationTextBox.Text, FirstName = FirstNameTextBox.Text, LastName = LastNameTextBox.Text, Position = PositionTextBox.Text, Team = TeamTextBox.Text, Feedback = FeedbackTextBox.Text };
                persons.Add(newPerson);
                SavePersons();
                ClearForm();
        }

        private void EditPersonButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedPerson != null)
            {
                selectedPerson.NameAbbreviation = NameAbbreviationTextBox.Text;
                selectedPerson.FirstName = FirstNameTextBox.Text;
                selectedPerson.LastName = LastNameTextBox.Text;
                selectedPerson.Position = PositionTextBox.Text;
                selectedPerson.Team = TeamTextBox.Text;
                selectedPerson.Feedback = FeedbackTextBox.Text;
                PersonsListBox.Items.Refresh();
                SavePersons();
                ClearForm();
            }
            else
            {
                MessageBox.Show("Please select a person");
            }
        }

        private void RemovePersonButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedPerson != null)
            {
                persons.Remove(selectedPerson);
                SavePersons();
                ClearForm();
            }
            else
            {
                MessageBox.Show("Please select a person to remove.");
            }
        }

        private void ClearForm()
        {
            NameAbbreviationTextBox.Clear();
            FirstNameTextBox.Clear();
            LastNameTextBox.Clear();
            PositionTextBox.Clear();
            TeamTextBox.Clear();
            FeedbackTextBox.Clear();
            selectedPerson = null;
            PersonsListBox.SelectedItem = null;
        }
    }

    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NameAbbreviation { get; set; }
        public string Position { get; set; }
        public string Team { get; set; }
        public string Feedback { get; set; }
    }
}

