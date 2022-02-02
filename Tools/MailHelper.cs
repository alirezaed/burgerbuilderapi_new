using System.Net.Mail;

namespace BurgerBuilderApi.Tools
{
	public class MailHelper
	{
		MailMessage mail;
		SmtpClient SmtpServer;
		string AdminsEmail = "";
		public MailHelper()
		{
			mail = new MailMessage();
			mail.From = new MailAddress("website.joybarhouse@gmail.com", "Burger Builder");
			SmtpServer = new SmtpClient("smtp.gmail.com");
			SmtpServer.Port = 587;
			SmtpServer.Credentials = new System.Net.NetworkCredential("website.joybarhouse@gmail.com", "#Ed12345!!");
			SmtpServer.EnableSsl = true;
		}
		public bool SendMail(string subject, string body, string to)
		{
			try
			{
				mail.To.Add(to);
				mail.Subject = subject;
				mail.Body = body;
				SmtpServer.Send(mail);
				return true;
			}
			catch (Exception ex)
			{
				/*log*/
				return false;
			}
		}
		public bool SendMailToAdmin(string subject, string body)
		{
			return SendMail(subject, body, AdminsEmail);
		}


	}
}
