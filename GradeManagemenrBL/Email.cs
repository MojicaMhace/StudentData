using MimeKit;
using MailKit.Net.Smtp;
using System;
using System.Runtime.CompilerServices;

namespace GradeManagemenrBL
{
    public class Email
    {
        public static void SendEmail(string studentName, string sendingEmail, double average)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Grade Management System", "mazeacijom@gmail.com"));
            message.To.Add(new MailboxAddress("Student", "mhacemojica04@gmail.com"));
            message.Subject = "JOB WELL DONE!!!!!";

            message.Body = new TextPart("html")
            {
                Text = $@"<h1>Hi, Students!</h1>
                <p>You did your best.</p>
                <ul>
                <li> This is your General Weighted Average: {average}</li>
                </ul>
                <p><strong>JOB WELL DONE!!!!!</strong></p>"
            };

            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect("sandbox.smtp.mailtrap.io", 2525, MailKit.Security.SecureSocketOptions.StartTls);

                    client.Authenticate("f0a7682123e394", "70d79b626a64e3");

                    client.Send(message);
                    Console.WriteLine("Email sent successfully through Mailtrap.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error sending email: {ex.Message}");
                }
                finally
                {
                    client.Disconnect(true);
                }
            }
        }
        public static void UpdateEmail(string studentName, string courseSection, double updateAverage)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Grade Management System", "mazeacijom@gmail.com"));
            message.To.Add(new MailboxAddress("Student", "mhacemojica04@gmail.com")); 
            message.Subject = "Your General Weighted Average has been Updated";

            message.Body = new TextPart("html")
            {
                Text = $@"<h1>Hi, {studentName}!</h1>
                    <p>Your General Weighted Average has been successfully updated.</p>
                    <ul>
                    <li><strong>Course and Section:</strong> {courseSection}</li>
                    <li><strong>New General Weighted Average:</strong> {updateAverage}</li>
                    </ul>
                    <p><strong>Keep up the good work and continue improving!!!:)</strong></p>"
            };

            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect("sandbox.smtp.mailtrap.io", 2525, MailKit.Security.SecureSocketOptions.StartTls);
                    client.Authenticate("f0a7682123e394", "70d79b626a64e3");
                    client.Send(message);
                    Console.WriteLine("Update email sent successfully through Mailtrap.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error sending update email: {ex.Message}");
                }
                finally
                {
                    client.Disconnect(true);
                }
            }
        }

        public static void AddEmail(string studentName, string courseSection, string sendingEmail, double average)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Grade Management System", "mazeacijom@gmail.com"));
            message.To.Add(new MailboxAddress("Student", "mhacemojica04@gmail.com"));
            message.Subject = "JOB WELL DONE!!!!!";

            message.Body = new TextPart("html")
            {
                Text = $@"<h1>Hi, {studentName}!</h1>
                <p>Congratulations!!!!</p>
                <ul>
                <li><strong>Course and Section:</strong> {courseSection}</li>
                <li><strong>General Weighted Average:</strong> {average}</li>
                </ul>
                <p><strong>Keep up the good work!!!:)</strong></p>"
            };

            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect("sandbox.smtp.mailtrap.io", 2525, MailKit.Security.SecureSocketOptions.StartTls);
                    client.Authenticate("f0a7682123e394", "70d79b626a64e3");
                    client.Send(message);
                    Console.WriteLine("Email sent successfully through Mailtrap.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error sending email: {ex.Message}");
                }
                finally
                {
                    client.Disconnect(true);
                }
            }
        }

        public static void deleteEmail(string studentName, string sendingEmail)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Grade Management System", "mazeacijom@gmail.com"));
            message.To.Add(new MailboxAddress("Student", "mhacemojica04@gmail.com"));
            message.Subject = "Record Deleted Successfully";

            message.Body = new TextPart("html")
            {
                Text = $@"<h1>Hi!!, {studentName}!</h1>
                <p><strong>Student Record Successfully deleted.</strong></p>"
            };

            using (var client = new SmtpClient())
            {
                try
                {
                    client.Connect("sandbox.smtp.mailtrap.io", 2525, MailKit.Security.SecureSocketOptions.StartTls);
                    client.Authenticate("f0a7682123e394", "70d79b626a64e3");
                    client.Send(message);
                    Console.WriteLine("Delete email sent successfully through Mailtrap.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error sending deletr email: {ex.Message}");
                }
                finally
                {
                    client.Disconnect(true);
                }
            }
        }

    }
}
