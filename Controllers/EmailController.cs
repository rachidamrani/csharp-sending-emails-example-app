using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace SendingEmails.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmailController : ControllerBase
{
    [HttpPost]
    public IActionResult SendEmail()
    {
        // create a new mime message object which we are going to use to fill the message data
        MimeMessage message = new MimeMessage();
        
        // add the sender info that will appear in the email message
        message.From.Add(new MailboxAddress("Ecomove Admin", "ecomove.enterprise@gmail.com"));

        // add the receiver email address
        message.To.Add(MailboxAddress.Parse("rachid3alaoui@gmail.com"));
        
        // add the message subject
        message.Subject = "Message subject";
        
        // add the message body
        message.Body = new TextPart("plain") { Text = @"Yes, Hello! this is a message sent from C# web api!" };
        
        // create a new SMTP client
        SmtpClient smtpClient = new SmtpClient();

        try
        {
            // connect to the gmail smtp server using port 465 with SSL enabled
            smtpClient.Connect("smtp.gmail.com", 465, true);
            
            // authenticate to the client
            smtpClient.Authenticate("ecomove.enterprise@gmail.com", "qwcq qvvf ksqf ibuf");
            
            // send the message
            smtpClient.Send(message);

            return Ok("Email sent!");


        }
        catch (Exception e)
        {
            // in case of an error display the message
            Console.WriteLine(e.Message);
            
            return Problem(title: "Error", detail: e.Message, statusCode: 500);
        }
        finally
        {
            // at any case always disconnect from the client
            smtpClient.Disconnect(true);
            smtpClient.Dispose();
        }
    }
}