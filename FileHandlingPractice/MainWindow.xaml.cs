using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace FileHandlingConcepts
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

            private readonly string folderPath =
                System.IO.Path.Combine(
    Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
    "FileHandlingConcepts");
        private string FilePath =>
             System.IO.Path.Combine(folderPath, "Hema.txt");
        private string StudentFolderPath =>
                System.IO.Path.Combine(folderPath, "Student");


            // 1. Create Files
            private void Createfile_Click(object sender, RoutedEventArgs e)
            {
                try
                {
                    Directory.CreateDirectory(folderPath);

                    string[] files =
                    {
                    "Hema.txt",
                    "Steve.txt",
                    "Sayisha.txt"
                };

                    foreach (string file in files)
                    {
                        string path = System.IO.Path.Combine(folderPath, file);

                        if (!File.Exists(path))
                        {
                            using (File.Create(path))
                            {
                            }
                        }
                    }

                    MessageBox.Show(
                        "Files created successfully!",
                        "Success");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }

            // 2. Write File
            private void writefile_Click(object sender, RoutedEventArgs e)
            {
                try
                {
                    Directory.CreateDirectory(folderPath);

                    File.WriteAllText(
                        FilePath,
                        "Hi My Name is Hema");

                    MessageBox.Show(
                        "Text written successfully!",
                        "Success");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }

            // 3. Read File
            private void readfile_Click(object sender, RoutedEventArgs e)
            {
                try
                {
                    if (File.Exists(FilePath))
                    {
                        string value = File.ReadAllText(FilePath);

                        MessageBox.Show(value, "File Content");
                    }
                    else
                    {
                        MessageBox.Show(
                            "File not found. Please create or write the file first.",
                            "Information");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }

            // 4. Delete File
            private void deletefile_Click(object sender, RoutedEventArgs e)
            {
                try
                {
                    if (File.Exists(FilePath))
                    {
                        File.Delete(FilePath);

                        MessageBox.Show(
                            "Hema.txt deleted successfully!",
                            "Success");
                    }
                    else
                    {
                        MessageBox.Show(
                            "File does not exist.",
                            "Information");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }

            // 5. Create Folder
            private void btnCreatefolder_Click(
                object sender, RoutedEventArgs e)
            {
                try
                {
                    if (Directory.Exists(StudentFolderPath))
                    {
                        MessageBox.Show("Folder already exists.");
                    }
                    else
                    {
                        Directory.CreateDirectory(StudentFolderPath);

                        MessageBox.Show(
                            "Student folder created successfully!",
                            "Success");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }

            // 6. Delete Folder
            private void btndeletefolder_Click(
                object sender, RoutedEventArgs e)
            {
                try
                {
                    if (Directory.Exists(StudentFolderPath))
                    {
                        if (Directory.GetFileSystemEntries(
                            StudentFolderPath).Length == 0)
                        {
                            Directory.Delete(StudentFolderPath);

                            MessageBox.Show(
                                "Student folder deleted successfully!",
                                "Success");
                        }
                        else
                        {
                            MessageBox.Show(
                                "Folder is not empty. Delete its contents first.");
                        }
                    }
                    else
                    {
                        MessageBox.Show(
                            "Student folder does not exist.",
                            "Information");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }

            // 7. Get Files
            private void btngetfiles_Click(
                object sender, RoutedEventArgs e)
            {
                try
                {
                    if (Directory.Exists(folderPath))
                    {
                        string[] files = Directory.GetFiles(
                            folderPath,
                            "*",
                            SearchOption.TopDirectoryOnly);

                        if (files.Length == 0)
                        {
                            MessageBox.Show("No files found.");
                        }
                        else
                        {
                            MessageBox.Show(
                                string.Join(Environment.NewLine, files),
                                "Files List");
                        }
                    }
                    else
                    {
                        MessageBox.Show(
                            "Folder not found. Please create files first.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error");
                }
            }
    }

}


