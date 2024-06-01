using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows;

namespace FeedbackApp
{
    public partial class MainWindow : Window
    {
        private List<Person> persons = new();
        private Person selectedPerson;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddPersonButton_Click(object sender, RoutedEventArgs e)
        {
            var person = new Person
            {
                FirstName = FirstNameTextBox.Text,
                LastName = LastNameTextBox.Text,
                NameAbbreviation = NameAbbreviationTextBox.Text,
                Position = PositionTextBox.Text,
                Team = DepartmentTextBox.Text
            };

            persons.Add(person);
            PersonsListBox.Items.Add(persons);

            FirstNameTextBox.Clear();
            LastNameTextBox.Clear();
            NameAbbreviationTextBox.Clear();
            PositionTextBox.Clear();
            DepartmentTextBox.Clear();
        }

        private void SavePersonsButton_Click(object sender, RoutedEventArgs e)
        {
            var json = JsonSerializer.Serialize(persons);
            File.WriteAllText("feedback.json", json);
            MessageBox.Show("Person saved successfully.");
        }

        private void PersonListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

            if (PersonsListBox.SelectedItem != null)
            {
                selectedPerson = (Person)PersonsListBox.SelectedItem;
                FeedbackTextBox.Text = selectedPerson.Feedback ?? string.Empty;
                MessageBox.Show($"Selected Colleague: {selectedPerson.FirstName}");
            }
            else
            {
                selectedPerson = null;
                FeedbackTextBox.Clear();
            }
        }

        private void SaveFeedbackButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedPerson != null)
            {
                selectedPerson.Feedback = FeedbackTextBox.Text;
                MessageBox.Show("Feedback saved successfully.");
            }
            else
            {
                MessageBox.Show("Please select a person to save feedback.");
            }
        }
        private void RemovePersonButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedPerson != null)
            {
                persons.Remove(selectedPerson);
                PersonsListBox.Items.Remove(selectedPerson);
                FeedbackTextBox.Clear();
                selectedPerson = null;
                MessageBox.Show("Person removed successfully.");
            }
            else
            {
                MessageBox.Show("Please select a person to remove.");
            }
        }
    }
}
