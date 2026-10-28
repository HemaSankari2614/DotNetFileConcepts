using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JSONDeserializauionFileHandling
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
  
    partial class MainWindow : Window
    {
        private readonly string filePath =
            System.IO.Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "Users.json");

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string name = txtname.Text.Trim();
            string username = txtusername.Text.Trim();
            string password = txtpassword.Password;
            string confirmPassword = txtcnmpass.Password;

            if (string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show(
                    "Please fill in all fields.",
                    "Validation",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show(
                    "Password and Confirm Password do not match.",
                    "Validation",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);

                return;
            }

            try
            {
                List<User> users = new List<User>();

                if (File.Exists(filePath))
                {
                    string existingJson = File.ReadAllText(filePath);

                    if (!string.IsNullOrWhiteSpace(existingJson))
                    {
                        users = JsonSerializer.Deserialize<List<User>>(
                            existingJson) ?? new List<User>();
                    }
                }

                if (users.Any(u =>
                    u.Username.Equals(
                        username,
                        StringComparison.OrdinalIgnoreCase)))
                {
                    MessageBox.Show(
                        "Username already exists.",
                        "Registration",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);

                    return;
                }

                User newUser = new User
                {
                    Name = name,
                    Username = username,
                    Password = password
                };

                users.Add(newUser);

                string json = JsonSerializer.Serialize(
                    users,
                    new JsonSerializerOptions
                    {
                        WriteIndented = true
                    });

                File.WriteAllText(filePath, json);

                MessageBox.Show(
                    "Registration successful!",
                    "Success",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                txtname.Clear();
                txtusername.Clear();
                txtpassword.Clear();
                txtcnmpass.Clear();
            }
            catch (JsonException)
            {
                MessageBox.Show(
                    "The JSON file contains invalid data.",
                    "File Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            catch (IOException ex)
            {
                MessageBox.Show(
                    "File error: " + ex.Message,
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "An error occurred: " + ex.Message,
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }

    public class User
    {
        public string Name { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}