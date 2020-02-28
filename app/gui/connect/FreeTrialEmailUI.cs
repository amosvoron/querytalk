using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Windows.Forms;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using QueryTalk.Security;

namespace QueryTalk.Mapper
{
    internal partial class FreeTrialEmailUI : UserControl, ILateInitializer
    {
        private MainForm _mainForm;
        private Exception _exception;
        private string _code = null;

        #region ILateInitializer

        void ILateInitializer.Initialize()
        {
            // Log.LogUse(34);
        }

        #endregion

        internal FreeTrialEmailUI(MainForm mainForm)
        {
            _mainForm = mainForm;
            InitializeComponent();
            AddEventHandlers();

            Load += (o, e) =>
            {
                this.ActiveControl = _ctrEmail;
                _ctrEmail.Select(0, 0);
            };
        }

        private void AddEventHandlers()
        {
            _ctrEmail.TextChanged += (o, e) =>
            {
                if (string.IsNullOrWhiteSpace(_ctrEmail.Text))
                {
                    SetEmailLabelRed();
                }
                else
                {
                    SetEmailLabelRed(false);
                }
            };

            _ctrCode.TextChanged += (o, e) =>
            {
                if (string.IsNullOrWhiteSpace(_ctrCode.Text))
                {
                    SetCodeLabelRed();
                }
                else
                {
                    SetCodeLabelRed(false);
                }
            };

            _ctrEmail.KeyDown += (o, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    _SendEmail();

                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
            };

            _ctrCode.KeyDown += (o, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    _VerifyCode();

                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
            };

            _imageVideo.MouseEnter += (o, e) =>
            {
                Cursor = Cursors.Hand;
            };
            _imageVideo.MouseLeave += (o, e) =>
            {
                Cursor = Cursors.Default;
            };

        }

        #region Actions

        private void _SendEmail()
        {
            try
            {
                // Log.LogUse(45);

                Reset();

                if (!ValidateEmail())
                {
                    return;
                }

                _btnSend.Enabled = false;
                _iconAjaxSendCode.Visible = true;
                GenerateCode();

                Task.Factory.StartNew(() =>
                {
                    try
                    {
                        SendEmail();
                        SetEmailSent();
                    }
                    catch (Exception ex)
                    {
                        try
                        {
                            SaveEmail();
                            SetEmailSent(true, true);
                        }
                        catch
                        {
                            _exception = ex;
                            SetEmailSent(false);
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                _exception = ex;
                SetEmailSent(false);
            }
        }

        private void _VerifyCode()
        {
            try
            {
                // Log.LogUse(46);

                ResetCode();

                if (!ValidateCode())
                {
                    return;
                }

                _iconAjaxVerify.Visible = true;
                _linkTryAgain.Enabled = false;
                _btnVerify.Enabled = false;
                _ctrCode.Enabled = false;

                Task.Factory.StartNew(() =>
                {
                    SaveEmail();
                });
            }
            catch (Exception ex)
            {
                _exception = ex;
                SetFailed();
            }
        }

        private void SaveEmail()
        {
            try
            {
                var response = CommunicationManager.ExecuteCommand(
                    Program.License.CommunicationID, Commands.REGISTEREMAIL, string.Empty, string.Empty, _ctrEmail.Text.Trim());

                if (CommunicationManager.IsDenied(response))
                {
                    SetFailed();
                }
                else
                {
                    SetDone();
                }
            }
            catch (Exception ex)
            {
                _exception = ex;
                SetFailed();
            }
        }

        #endregion

        #region Setters

        private void SetEmailLabelRed(bool set = true)
        {
            _ctrEmailLabel.ForeColor = set ? Color.Red : Color.Black;
        }

        private void SetCodeLabelRed(bool set = true)
        {
            _ctrCodeLabel.ForeColor = set ? Color.Red : Color.Black;
        }

        private void Reset(bool empty = true)
        {
            _ctrFailure.ThreadSafeInvoke(() =>
            {
                _exception = null;
                _emailSuccessfullySentPanel.Visible = false;
                _ctrDone.Visible = false;
                _ctrCode.Visible = false;
                _btnVerify.Visible = false;
                _iconOK.Visible = false;
                _linkContinue.Visible = false;
                _ctrEmailLabel.ForeColor = Color.Black;
                _ctrEmailError.Visible = false;
                _ctrCodeLabel.ForeColor = Color.Black;
                _ctrCodeError.Visible = false;
                _ctrCodeLabel.Visible = false;
                _linkTryAgain.Visible = false;
                _iconAjaxSendCode.Visible = false;
                _ctrFailure.Visible = false;

                if (empty)
                {
                    _ctrCode.Text = string.Empty;
                }
            });
        }

        private void ResetCode()
        {
            _iconOK.ThreadSafeInvoke(() =>
            {
                _iconOK.Visible = false;
                _ctrDone.Visible = false;
                _ctrCodeLabel.ForeColor = Color.Black;
                _ctrCodeError.Visible = false;
                _linkContinue.Visible = false;
                _ctrFailure.Visible = false;
            });
        }

        private void SetEmailSent(bool success = true, bool skipVerification = false)
        {
            _ctrCode.ThreadSafeInvoke(() =>
            {
                if (success)
                {
                    if (skipVerification)
                    {
                        _ctrEmail.Enabled = false;
                        _ctrCodeLabel.ForeColor = Color.Black;
                        _linkTryAgain.Visible = true;
                        _iconAjaxSendCode.Visible = false;
                        _linkTryAgain.Visible = true;
                        _linkContinue.Visible = true;
                        _ctrDone.Visible = false;
                    }
                    else
                    {
                        _ctrCode.Visible = true;
                        _ctrCodeLabel.Visible = true;
                        _btnVerify.Visible = true;
                        _ctrEmail.Enabled = false;
                        _ctrEmail.ForeColor = Color.Black;
                        _ctrCodeLabel.ForeColor = Color.Black;
                        _linkTryAgain.Visible = true;
                        _iconAjaxSendCode.Visible = false;
                        _emailSuccessfullySentPanel.Visible = true;
                        _ctrEmailSuccessfullySent.Text = _ctrEmail.Text;
                    }
                }
                else
                {
                    _ctrEmail.Enabled = false;
                    _ctrEmail.ForeColor = Color.Black;
                    _linkTryAgain.Visible = true;
                    _iconAjaxSendCode.Visible = false;
                    _ctrFailure.Visible = true;
                    _linkContinue.Visible = true;
                }
            });
        }

        private void SetDone()
        {
            // Log.LogUse(57);

            _ctrDone.ThreadSafeInvoke(() =>
            {
                _ctrDone.Visible = true;
                _iconOK.Visible = true;
                _linkContinue.Visible = true;
                _iconAjaxVerify.Visible = false;
                _linkTryAgain.Enabled = true;
                _btnVerify.Enabled = true;
                _ctrCode.Enabled = true;
            });
        }

        private void SetFailed()
        {
            // Log.LogUse(62);

            _ctrDone.ThreadSafeInvoke(() =>
            {
                _ctrFailure.Visible = true;
                _linkContinue.Visible = true;
                _iconAjaxVerify.Visible = false;
                _linkTryAgain.Enabled = true;
                _btnVerify.Enabled = true;
                _ctrCode.Enabled = true;
            });
        }

        #endregion

        #region Generate code & send email

        private void GenerateCode()
        {
            var rand = new Random();
            _code = rand.Next(1000, 9999).ToString();

            //_ctrCode.Text = _code;
        }

        private void SendEmail()
        {
            var mailClient = new SmtpClient(host: "querytalk.com", port: 25);
            mailClient.EnableSsl = false;
            mailClient.UseDefaultCredentials = false;
            mailClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            mailClient.Credentials = new System.Net.NetworkCredential("support@querytalk.com", "support!1");

            var bodyBuilder = new StringBuilder();
            bodyBuilder.Append("<p>Thank you for using QueryTalk!</p>");
            bodyBuilder.Append("<p>Please enter the following verification code in the application:&nbsp;");
            bodyBuilder.Append("<strong>" + _code + "</strong></p>");
            bodyBuilder.Append("<p>QueryTalk Team<br />");
            bodyBuilder.Append("<a href=\"mailto:support@querytalk.com\">support@querytalk.com</a></p>");

            MailMessage email = new MailMessage(new MailAddress("no-reply@querytalk.com", "QueryTalk"), 
                new MailAddress(_ctrEmail.Text.Trim()));
            email.Subject = "Email Verification Code";
            email.IsBodyHtml = true;
            email.Body = bodyBuilder.ToString();
            email.IsBodyHtml = true;
            email.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            mailClient.Send(email);
        }

        #endregion

        #region Validate

        private bool ValidateEmail()
        {
            var email = _ctrEmail.Text;
            if (string.IsNullOrWhiteSpace(email))
            {
                SetEmailLabelRed();
                return false;
            }

            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(email))
            {
                return true;
            }

            _ctrEmailError.Visible = true;
            return false;
        }

        private bool ValidateCode()
        {
            var code = _ctrCode.Text;

            if (string.IsNullOrWhiteSpace(code))
            {
                SetCodeLabelRed();
                return false;
            }

            if (code != _code)
            {
                _ctrCodeError.Visible = true;
                return false;
            }

            return true;
        }

        #endregion

        #region Link event handlers

        private void _linkResend_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _emailSuccessfullySentPanel.Visible = false;
            _btnSend.Enabled = true;
            _linkTryAgain.Visible = false;
            _ctrEmail.Enabled = true;
            Reset();
        }

        private void _btnSend_Click(object sender, EventArgs e)
        {
            _SendEmail();
        }

        private void _btnVerify_Click(object sender, EventArgs e)
        {
            _VerifyCode();
        }

        private void _linkContinue_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            _mainForm.OnAuthenticationSuccess();
        }

        #endregion

        private void _imageVideo_Click(object sender, EventArgs e)
        {
            // Log.LogUse(73);
            Common.OpenBrowser(Globals.GlobalResources.AppFreeTrialEmailVideo);
        }
    }
}
