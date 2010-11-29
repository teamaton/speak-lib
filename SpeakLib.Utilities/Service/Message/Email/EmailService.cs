using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using SpeakFriend.Utilities;

namespace SpeakFriend.Utilities
{
    public class EmailService
    {
        private readonly SettingService _settingService;

        public EmailService(SettingService settingService)
        {
            _settingService = settingService;
        }

        public void Send<TMessageParticipantType, TMessageType, TMessageStatus>
            (MessageBase<TMessageParticipantType, TMessageType, TMessageStatus> message)
        {
            if (message.DistributionType != DistributionType.Email)
                return;

            var settings = GetSettings();

#if DEBUG
			var mailMessageDebug = GetFrom(message, settings);
            Debug.WriteLine("Email:" + Environment.NewLine + mailMessageDebug.Subject + Environment.NewLine +
                            mailMessageDebug.Body);
#endif

            if (settings.EmailEnabled.Value == false)
                return;

            var mailMessage = GetFrom(message, settings);

        	SmtpClient smtpClient;
			if (settings.SmtpConfigOverDb.Value)
				smtpClient = new SmtpClient();
			else
			{
				smtpClient = new SmtpClient(settings.SmtpServer.Value, 25)
									 {
										 DeliveryMethod = SmtpDeliveryMethod.Network,
										 Credentials =
											new NetworkCredential(
												settings.SmtpUserName.Value,
												settings.SmtpPassword.Value
										)
									 };
			}

            smtpClient.Send(mailMessage);
        }

        public EmailSettings GetSettings()
        {
            EmailSettings emailSettings = GetEmailSettings();

            // Use values from web.config settings if not set yet.

            if (emailSettings.EmailEnabled.IsDefault())
                emailSettings.EmailEnabled.Value = SpeakLibSettings.EmailIsEnabled;

            if (emailSettings.EmailFromAddress.IsDefault())
                emailSettings.EmailFromAddress.Value = SpeakLibSettings.EmailFrom;

            if (emailSettings.EmailFromName.IsDefault())
                emailSettings.EmailFromName.Value = SpeakLibSettings.EmailFromName;

            if (emailSettings.EmailToAddress.IsDefault())
                emailSettings.EmailToAddress.Value = SpeakLibSettings.EmailTo;

            if (emailSettings.EmailToName.IsDefault())
                emailSettings.EmailToName.Value = SpeakLibSettings.EmailToName;

			if (emailSettings.SmtpConfigOverDb.IsDefault())
				emailSettings.SmtpConfigOverDb.Value = SpeakLibSettings.SmtpConfigOverDb;

            if (emailSettings.SmtpServer.IsDefault())
                emailSettings.SmtpServer.Value = SpeakLibSettings.SmtpServer;

            if (emailSettings.SmtpUserName.IsDefault())
                emailSettings.SmtpUserName.Value = SpeakLibSettings.SmtpUser;

            if (emailSettings.SmtpPassword.IsDefault())
                emailSettings.SmtpPassword.Value = SpeakLibSettings.SmtpPassword;

            _settingService.CreateOrUpdate(emailSettings.SettingList);

            return emailSettings;
        }

        private EmailSettings GetEmailSettings()
        {
            var settings = _settingService.GetBy(EmailSettings.Type, EmailSettings.TypeId);

            return new EmailSettings(settings);
        }

        /// <summary>
        /// Create a ready-to-sent email message. Use the From as ReplyTo and use a generic address as From.
        /// </summary>
        /// <param name="message">The internal message from which to build an email.</param>
        /// <param name="settings">The EmailSettings.</param>
        /// <returns>A new email message.</returns>
        private static MailMessage GetFrom<TMessageParticipantType, TMessageType, TMessageStatus>
            (MessageBase<TMessageParticipantType, TMessageType, TMessageStatus> message, EmailSettings settings)
        {
            var mailMessage = new MailMessage
                          {
                              From = new MessageAddress(settings.EmailFromAddress.Value, message.From.DisplayName).MailAddress,
                              ReplyTo = message.From.MailAddress,
                              BodyEncoding = Encoding.UTF8,
                              Body = message.Body,
                              SubjectEncoding = Encoding.UTF8,
                              Subject = message.Subject,
                          };

                mailMessage.To.Add(message.To.MailAddress);

            return mailMessage;
        }

    }
}
