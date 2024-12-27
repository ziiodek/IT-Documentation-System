using NPOI.POIFS.FileSystem;
using System;
using System.Net;
using System.Net.Mail;

namespace ITDocumentation.Classes
{
    public class Email
    {

        string client;
        int port;
        string username;
        string password;
        string message;
        string subject;
        string managerEmail;
        string authorEmail;

        public Email() {
            client = "smtp.office365.com";
            username = "noreply@firstlightfcu.org";
            password = "a=6lK3u>IO6^J4~MF608686$";
            port = 587;
            
        }

        public void waitingForApprovalMessage(string documentAuthor, string managerName, string authorEmail, string managerEmail, string documentName) {
            managerName = managerName.ToLower();
            authorEmail = authorEmail.ToLower();
            managerEmail = managerEmail.ToLower();
            documentAuthor = documentAuthor.ToLower();
            subject = "Document ready for approval";
            message = "A new document is ready for approval:<br>Author: " + documentAuthor + "<br>Document: " + documentName+"<br><a href='https://flfcu_innovations.fbfcu.org/itdocs/'>https://flfcu_innovations.fbfcu.org/itdocs/</a>";
            this.managerEmail = managerEmail;
            this.authorEmail = authorEmail;
            sendEmail();
        }

        public void documentApprovedMessage(string documentAuthor, string managerName, string authorEmail, string managerEmail, string documentName) {
            managerName = managerName.ToLower();
            authorEmail = authorEmail.ToLower();
            managerEmail = managerEmail.ToLower();
            documentAuthor = documentAuthor.ToLower();
            subject = "Your document was approved";
            message = "The document "+ documentName+" was approved by "+managerName+ "<br><a href='https://flfcu_innovations.fbfcu.org/itdocs/'>https://flfcu_innovations.fbfcu.org/itdocs/</a>";
            this.managerEmail= managerEmail;
            this.authorEmail = authorEmail;
            sendEmail();
        }

        public void documentNotApprovedMessage(string documentAuthor, string managerName, string authorEmail, string managerEmail, string documentName)
        {
            managerName = managerName.ToLower();
            authorEmail = authorEmail.ToLower();
            managerEmail = managerEmail.ToLower();
            documentAuthor = documentAuthor.ToLower();
            subject = "Your document was not approved";
            message = "The document " + documentName + " was not approved by " + managerName + "<br><a href='https://flfcu_innovations.fbfcu.org/itdocs/'>https://flfcu_innovations.fbfcu.org/itdocs/</a>";
            this.managerEmail = managerEmail;
            this.authorEmail = authorEmail;
            sendEmail();
        }


        void sendEmail() {
            var smtpClient = new SmtpClient(client) { 
                Port = port,
                Credentials = new NetworkCredential(username, password),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage { 
                From = new MailAddress(username),
                Subject = subject,
                Body = message,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(new MailAddress(managerEmail));
            mailMessage.CC.Add(new MailAddress(authorEmail));

            try { 
               smtpClient.Send(mailMessage);

            }
            catch (Exception ex) { 
                Console.WriteLine(ex.ToString());


            }
        }

    }
}
