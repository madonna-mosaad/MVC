using DataAccessLayer.Models;
using System;
using System.Net;
using System.Net.Mail;
namespace MVC.Helpers
{
	public class EmailSetting
	{
		public static void SendEmail(Email email)
		{
			using (var client = new SmtpClient("smtp.gmail.com", 587))
			{
				client.EnableSsl = true;
				client.Credentials = new NetworkCredential("madonnamosaad86@gmail.com", "dxpm cxpa kaiz vfct");

				var mail = new MailMessage("madonnamosaad86@gmail.com", email.Reciepent, email.Subject, email.Body);

				try
				{
					client.Send(mail);
				}
				catch (Exception ex)
				{
					// Handle any exceptions here
					Console.WriteLine(ex.ToString());
				}
			}
		}
	}
}