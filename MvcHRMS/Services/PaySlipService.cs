using MvcHRMS.Models;
using System.Net.Mail;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using MvcHRMS.Repository;
using System.IO;

namespace MvcHRMS.Services
{
    public class PaySlipService : IPaySlipService
    {
        private readonly IEmpRepository _empRepository;
        private readonly IConfiguration _configuration;

        public PaySlipService(IEmpRepository empRepository, IConfiguration configuration)
        {
            _empRepository = empRepository;
            _configuration = configuration;
        }

        public Emp GetEmployeeDetails(int empId)
        {
            return _empRepository.GetEmployeeById(empId);
        }

        public decimal CalculateNetSalary(decimal grossSalary, int workingDays, int leaveDays)
        {
            int workingDayss = 30;
            decimal perDaySalary = grossSalary / workingDayss;
            return grossSalary - (perDaySalary * leaveDays);
        }
        //public decimal CalculateNetSalary(decimal grossSalary, int workingDays, int leaveDays)
        //{
        //    if (workingDays == 0)
        //    {
        //        throw new ArgumentException("Working days cannot be zero.", nameof(workingDays));
        //    }

        //    decimal perDaySalary = grossSalary / workingDays;
        //    return grossSalary - (perDaySalary * leaveDays);
        //}

        public string GeneratePaySlipPDF(Emp employee, int workingDays, int leaveDays, string month)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "PaySlips", $"{employee.Name}_{month}.pdf");

            using (PdfWriter writer = new PdfWriter(filePath))
            {
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf, iText.Kernel.Geom.PageSize.A4);

                // Adding the company logo
                iText.Layout.Element.Image logo = new iText.Layout.Element.Image(iText.IO.Image.ImageDataFactory.Create(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "logo.png")));
                logo.ScaleToFit(100, 100);
                logo.SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER);
                document.Add(logo);

                // Adding company name
                Paragraph companyName = new Paragraph("Masstech Business Solutions")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                    .SetFontSize(16)
                    .SetBold()
                    .SetFontColor(iText.Kernel.Colors.ColorConstants.BLUE);
                document.Add(companyName);

                // Adding Payslip title
                Paragraph payslipTitle = new Paragraph($"Payslip for the Month {month} of 2023")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                    .SetFontSize(14)
                    .SetBold()
                    .SetMarginBottom(20);
                document.Add(payslipTitle);

                // Adding Employee details
                Table employeeTable = new Table(2).UseAllAvailableWidth();

                AddCellToTable(employeeTable, "NAME OF EMPLOYEE:", employee.Name);
                AddCellToTable(employeeTable, "DESIGNATION:", employee.Designation);
                AddCellToTable(employeeTable, "BANK NAME:", "ICICI Bank");
                AddCellToTable(employeeTable, "EMPLOYEE EMAIL:", employee.Email);
                AddCellToTable(employeeTable, "IFSC CODE:", "ICIC00000XXXXX");
                AddCellToTable(employeeTable, "DATE OF JOINING:", employee.DateOfJoining.ToString("dd-MM-yyyy"));
                AddCellToTable(employeeTable, "BANK ACCOUNT NO:", "123456789");
                AddCellToTable(employeeTable, "CONTACT NO:", employee.Contact);
                AddCellToTable(employeeTable, "PAN:", "FGKPB0088L");
                AddCellToTable(employeeTable, "DAYS IN MONTH:", "30");
                AddCellToTable(employeeTable, "AADHAR:", "7902 8178 5003");
                AddCellToTable(employeeTable, "UAN:", "NA");
                AddCellToTable(employeeTable, "LEAVE TAKEN:", leaveDays.ToString());

                document.Add(employeeTable);

                // Adding Salary details
                Table salaryTable = new Table(3).UseAllAvailableWidth();
                salaryTable.SetMarginTop(20).SetMarginBottom(20);

                AddCellToTable(salaryTable, "GROSS SALARY", "AMOUNT", "DEDUCTION");
                AddCellToTable(salaryTable, "Basic", "30,000", "PF:00.00");
                AddCellToTable(salaryTable, "HRA", "00.00", "Professional Tax:00.00");
                AddCellToTable(salaryTable, "Travel Allowance", "00.00", "TDS:00.00");
                AddCellToTable(salaryTable, "Bonus", "00.00", "");
                AddCellToTable(salaryTable, "Special Allowance", "00.00", "");
                AddCellToTable(salaryTable, "Medical Re-imbursement", "00.00", "");

                decimal grossSalary = employee.Salary;
                decimal netSalary = CalculateNetSalary(grossSalary, workingDays, leaveDays);
                decimal deductions = grossSalary - netSalary;

                AddCellToTable(salaryTable, "GROSS SALARY", grossSalary.ToString("F2"), "TOTAL DEDUCTION");
                AddCellToTable(salaryTable, "NET SALARY PAID", netSalary.ToString("F2"), deductions.ToString("F2"));

                document.Add(salaryTable);

                // Adding authorization
                Paragraph authorization = new Paragraph("Signature & Authorization")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                    .SetFontSize(12)
                    .SetBold()
                    .SetMarginTop(20);
                document.Add(authorization);

                document.Close();
            }

            return filePath;
        }
        public void SendPaySlipEmail(Emp employee, string filePath)
        {
            // Create a new MailMessage object
            MailMessage mail = new MailMessage();

            // Create and configure the SmtpClient object with hardcoded SMTP settings
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com"); // Replace with your SMTP server
            smtpClient.Port = 587; // Replace with your SMTP port
            smtpClient.Credentials = new System.Net.NetworkCredential("", ""); // Replace with your email and password
            smtpClient.EnableSsl = true;

            // Set the sender's email address
            mail.From = new MailAddress(""); // Replace with your sender email address

            // Add the recipient's email address
            mail.To.Add(employee.Email);

            // Set the email subject
            mail.Subject = "Your Payslip for the month";

            // Set the email body
            mail.Body = "Please find attached your payslip.";

            // Add the attachment
            mail.Attachments.Add(new Attachment(filePath));

            // Send the email
            smtpClient.Send(mail);
        }

        //public void SendPaySlipEmail(Emp employee, string filePath)
        //{
        //    MailMessage mail = new MailMessage();
        //    SmtpClient smtpClient = new SmtpClient(_configuration["Smtp:Host"]);

        //    mail.From = new MailAddress(_configuration["Smtp:From"]);
        //    mail.To.Add(employee.Email);
        //    mail.Subject = "Your Payslip for the month";
        //    mail.Body = "Please find attached your payslip.";
        //    mail.Attachments.Add(new Attachment(filePath));

        //    smtpClient.Port = int.Parse(_configuration["Smtp:Port"]);
        //    smtpClient.Credentials = new System.Net.NetworkCredential(_configuration["Smtp:Username"], _configuration["Smtp:Password"]);
        //    smtpClient.EnableSsl = true;

        //    smtpClient.Send(mail);
        //}

        public void SavePaySlip(int empId, string month, string filePath)
        {
            PaySlip paySlip = new PaySlip
            {
                EmpNo = empId,
                Month = month,
                FilePath = filePath
            };

            _empRepository.SavePaySlip(paySlip);
        }

        private void AddCellToTable(Table table, string cell1, string cell2)
        {
            table.AddCell(new Cell().Add(new Paragraph(cell1).SetBold()));
            table.AddCell(new Cell().Add(new Paragraph(cell2)));
        }

        private void AddCellToTable(Table table, string cell1, string cell2, string cell3)
        {
            table.AddCell(new Cell().Add(new Paragraph(cell1).SetBold()));
            table.AddCell(new Cell().Add(new Paragraph(cell2)));
            table.AddCell(new Cell().Add(new Paragraph(cell3)));
        }
    }
}
