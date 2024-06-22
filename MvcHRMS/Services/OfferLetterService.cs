using MvcHRMS.Models;
using MvcHRMS.Repository;
using System.Net.Mail;
using System.Net;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout;






namespace MvcHRMS.Services
{
    public class OfferLetterService
    {
        private readonly IOfferLetterRepository _offerLetterRepository;

        public OfferLetterService(IOfferLetterRepository offerLetterRepository)
        {
            _offerLetterRepository = offerLetterRepository;
        }

        public void GenerateOfferLetter(string empId, string name, string email, string dateOfJoining, string salary)
        {
            // Generate offer letter content
            string offerLetterContent = GenerateOfferLetterContent(name, dateOfJoining, salary);

            // Save offer letter metadata to database
            string fileName = $"{name}_{DateTime.Now:yyyyMMddHHmmss}";
            string filePath = SavePdfToFileSystem(offerLetterContent, fileName);
            _offerLetterRepository.Insert(new OfferLetter
            {
                EmpId = empId,
                Name = name,
                Email = email,
                Salary="30000",
                GeneratedDate = DateTime.Now,
                FilePath = filePath
                
            });

            // Send email with attachment
            SendEmailWithAttachment(email, offerLetterContent, fileName, filePath);

            // Clear form fields
            ClearFormFields();
        }

        private string GenerateOfferLetterContent(string name, string dateOfJoining, string salary)
        {
            return $" Offer Letter\nDear {name},\n\nWe are pleased to offer you the position. Your date of joining will be {dateOfJoining} and your salary will be {salary}.\n We are thrilled to extend to you an offer for the Software Developer position at MASSTECH. After careful consideration of your qualifications, experience, and interview performance, we are confident that you will make a valuable addition to our team.\nYour enthusiasm for Software Industry and your impressive achievements, particularly accomplishments and skills, stood out during the selection process. We believe your expertise will contribute significantly to our ongoing success.\nSincerely,\nMasstech";
        }
        private string SavePdfToFileSystem(string content, string fileName)
        {
            string directoryPath = Path.Combine("OfferLetters");
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string filePath = Path.Combine(directoryPath, $"{fileName}.pdf");

            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                using (PdfWriter writer = new PdfWriter(stream))
                {
                    using (PdfDocument pdf = new PdfDocument(writer))
                    {
                        Document document = new Document(pdf);
                        document.Add(new Paragraph(content));
                        document.Close();
                    }
                }
            }

            return filePath;
        }

        private void SendEmailWithAttachment(string toEmail, string content, string fileName, string filePath)
        {
            // Email sender credentials
            string fromEmail = "your@mail"; // Replace with your email
            string fromPassword = "yourpassword"; // Replace with your email password

            // Create mail message
            using (var message = new MailMessage())
            {
                message.From = new MailAddress(fromEmail);
                message.To.Add(toEmail);
                message.Subject = "Your Offer Letter";
                message.Body = "Please find attached your offer letter.";

                // Attach PDF
                Attachment attachment = new Attachment(filePath, System.Net.Mime.MediaTypeNames.Application.Pdf);
                attachment.Name = $"{fileName}.pdf";
                message.Attachments.Add(attachment);

                // Configure SMTP client
                using (var smtpClient = new SmtpClient("smtp.gmail.com"))
                {
                    smtpClient.Port = 587;
                    smtpClient.Credentials = new NetworkCredential(fromEmail, fromPassword);
                    smtpClient.EnableSsl = true;

                    // Send email
                    smtpClient.Send(message);
                }
            }
        }

        private void ClearFormFields()
        {
            // Implement form field clearing logic similar to your previous method
        }

    }
}
