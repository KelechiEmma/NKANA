using System.Threading.Tasks;

namespace NKANA.Services
{
    public interface IEmailSender
    {
        /// <summary>
        /// Uses the default email configuration stored in app settings to send an email.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="userDisplayName">An optional name to display at inbox as the sender of the message.</param>
        /// <returns></returns>
        /// <remarks>Use this method when you want to send automated emails that do not require human interactions. Emails
        /// such as user registration emails, password recovery. These emails area usually from noreply@abc.com</remarks>
        DeliveryResponse SendEmail(EmailMessage message, string userDisplayName = null);

        /// <summary>
        /// Uses the default email configuration stored in app settings to send an email.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="userDisplayName">An optional name to display at inbox as the sender of the message.</param>
        /// <returns></returns>
        /// <remarks>Use this method when you want to send automated emails that do not require human interactions. Emails
        /// such as user registration emails, password recovery. These emails area usually from noreply@abc.com</remarks>
        Task<DeliveryResponse> SendEmailAsync(EmailMessage message, string userDisplayName = null);

        /// <summary>
        /// Sends an email to the designated email address.
        /// </summary>
        /// <param name="message">The message to be sent.</param>
        /// <param name="from">The email address of the sender</param>
        /// <param name="username">The username of the sender used to login to the email server.</param>
        /// <param name="password">The password of the sender used to login to the email server.</param>
        /// <param name="userDisplayName">An optional name to display at inbox as the sender of the message.</param>
        /// <returns>Returns a flag indicating if the message was sent successfully.</returns>
        DeliveryResponse SendEmail(EmailMessage message, string[] from, string username = null, string password = null, string userDisplayName = null);

        /// <summary>
        /// Sends an email to the designated email address.
        /// </summary>
        /// <param name="message">The message to be sent.</param>
        /// <param name="from">The email address of the sender</param>
        /// <param name="username">The username of the sender used to login to the email server.</param>
        /// <param name="password">The password of the sender used to login to the email server.</param>
        /// <param name="userDisplayName">An optional name to display at inbox as the sender of the message.</param>
        /// <returns>Returns a flag indicating if the message was sent successfully.</returns>
        Task<DeliveryResponse> SendEmailAsync(EmailMessage message, string[] from, string username = null, string password = null, string userDisplayName = null);
    }
}
