using System.Globalization;
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
using System.Xml.Linq;

namespace DataSave
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
            private void GenerateOfferLetter_Click(
                object sender, RoutedEventArgs e)
            {
                string name = txtName.Text.Trim();
                string position = txtPosition.Text.Trim();
                string company = txtCompany.Text.Trim();
                string salaryText = txtSalary.Text.Trim();
                string joiningDateText = txtJoiningDate.Text.Trim();
                string location = txtLocation.Text.Trim();

                if (string.IsNullOrWhiteSpace(name) ||
                    string.IsNullOrWhiteSpace(position) ||
                    string.IsNullOrWhiteSpace(company) ||
                    string.IsNullOrWhiteSpace(salaryText) ||
                    string.IsNullOrWhiteSpace(joiningDateText) ||
                    string.IsNullOrWhiteSpace(location))
                {
                    MessageBox.Show(
                        "Please fill in all fields.",
                        "Missing Details",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);

                    return;
                }

                if (!decimal.TryParse(
                        salaryText,
                        NumberStyles.Number,
                        CultureInfo.InvariantCulture,
                        out decimal salary) || salary < 0)
                {
                    MessageBox.Show(
                        "Please enter a valid annual salary.",
                        "Invalid Salary",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);

                    return;
                }

                if (!DateTime.TryParseExact(
                        joiningDateText,
                        "dd-MM-yyyy",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out DateTime joiningDate))
                {
                    MessageBox.Show(
                        "Enter the date in dd-MM-yyyy format.",
                        "Invalid Date",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);

                    return;
                }

                string today = DateTime.Today.ToString(
                    "dd MMMM yyyy",
                    CultureInfo.InvariantCulture);

                string joining = joiningDate.ToString(
                    "dd MMMM yyyy",
                    CultureInfo.InvariantCulture);

                txtOfferLetter.Text =
                    company.ToUpperInvariant() + "\n" +
                    "========================================\n\n" +
                    "                 OFFER LETTER\n\n" +
                    "Date: " + today + "\n\n" +
                    "Dear " + name + ",\n\n" +
                    "We are pleased to offer you the position of " +
                    position + " at " + company + ".\n\n" +
                    "Your annual salary will be Rs. " +
                    salary.ToString("N2",
                        CultureInfo.GetCultureInfo("en-IN")) +
                    " per annum.\n\n" +
                    "Your joining date is " + joining +
                    ", and your work location will be " +
                    location + ".\n\n" +
                    "Your employment will be subject to the company's " +
                    "policies, terms and conditions. Further details " +
                    "will be provided by the HR department.\n\n" +
                    "We look forward to welcoming you to our team " +
                    "and wish you success in your new role.\n\n" +
                    "Sincerely,\n\n" +
                    "HR Department\n" +
                    company + "\n\n" +
                    "========================================\n" +
                    "Sample offer letter for demonstration purposes.";

            

                MessageBox.Show(
                    "Offer letter generated successfully!",
                    "Success",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }

            private void Clear_Click(object sender, RoutedEventArgs e)
            {
                txtName.Clear();
                txtPosition.Clear();
                txtCompany.Clear();
                txtSalary.Clear();
                txtJoiningDate.Clear();
                txtLocation.Clear();

                txtOfferLetter.Text =
                    "Your offer letter will appear here...";

               
            }

            private void Print_Click(object sender, RoutedEventArgs e)
            {
                
            if (string.IsNullOrWhiteSpace(txtOfferLetter.Text) ||
                txtOfferLetter.Text == "Your offer letter will appear here...")
            {
                MessageBox.Show(
                    "Please generate the offer letter first.",
                    "Information",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                return;
            }

            PrintDialog printDialog = new PrintDialog();

            if (printDialog.ShowDialog() == true)
            {
                FlowDocument document = new FlowDocument(
                    new Paragraph(new Run(txtOfferLetter.Text)));

                document.PagePadding = new Thickness(50);
                document.FontSize = 12;

                IDocumentPaginatorSource paginator = document;

                printDialog.PrintDocument(
                    paginator.DocumentPaginator,

                    "Employee Offer Letter");
            }
    }

}

       

}