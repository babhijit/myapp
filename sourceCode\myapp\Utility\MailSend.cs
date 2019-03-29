using System;
using System.Collections.Generic;
using System.Text;
using System.Speech.Synthesis;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;
using Utility;


namespace Utility
{
    public sealed class MailSend
    {
        public static Boolean SetUpMailConfiguration()
        {
            if (AppConfig.Instance.EmailFlag)
            {
                if (String.IsNullOrEmpty(AppConfig.Instance.SmtpHost) || ((short)AppConfig.Instance.SmtpPort == 0) ||
                        String.IsNullOrEmpty(AppConfig.Instance.SmtpUser))
                {
                    MailSetupForm mailConfigForm = new MailSetupForm();

                    if (mailConfigForm.ShowDialog() != DialogResult.OK)
                        return false;
                }
            }

            return true;
        }

        public static Boolean SendMail(String from, String to, String subject, String body = "")
        {
            SpeechSynthesizer speechSynthesizer = new SpeechSynthesizer();
            speechSynthesizer.SelectVoiceByHints(VoiceGender.Female);
            speechSynthesizer.Speak("Sending eMail");

            MailMessage eMail = new MailMessage(from, to, subject, body);

            //MailAddress carbonCopy = new MailAddress("Notification_List@contoso.com");
            //eMail.CC.Add(carbonCopy);

            eMail.BodyEncoding = System.Text.Encoding.UTF8;
            eMail.SubjectEncoding = System.Text.Encoding.UTF8;

            Boolean keepTrying = true;
            short noPass = 0;
            while (keepTrying)
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    SmtpClient smtpClient = new SmtpClient(AppConfig.Instance.SmtpHost, AppConfig.Instance.SmtpPort);
                    smtpClient.Credentials = new NetworkCredential(AppConfig.Instance.SmtpUser, AppConfig.Instance.SmtpPwd);
                    smtpClient.Send(eMail);
                    Cursor.Current = Cursors.Default;
                    speechSynthesizer.Speak("eMail Sent");
                    return true;

                }
                catch (SmtpFailedRecipientsException e)
                {
                    keepTrying = false;
                    MessageBox.Show(e.Message, "Email Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (SmtpException)
                {
                    MailSetupForm mailSetupFrom = new MailSetupForm();
                    if (DialogResult.Cancel == mailSetupFrom.ShowDialog())
                        keepTrying = false;
                }

                noPass++;
                if (noPass == 3)
                    keepTrying = false;
            }

            return false;
        }

    }
}
