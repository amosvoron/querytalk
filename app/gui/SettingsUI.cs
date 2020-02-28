using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace QueryTalk.Mapper
{
    internal partial class SettingsUI : UserControl, ILateInitializer
    {
        private MappingUI _mappingUI;
        private KeyFile _keyFile = new KeyFile();
        private bool _isSigned
        {
            get
            {
                return _checkSignAssembly.Checked;
            }
        }

        #region ILateInitializer

        void ILateInitializer.Initialize()
        {
            // Log.LogUse(9);
        }

        #endregion

        internal SettingsUI(MappingUI mappingUI)
        {
            _mappingUI = mappingUI;
            InitializeComponent();
            Init();
            AddEventHandlers();
        }

        private void Init()
        {
            // assembly version
            var assemblyVersion = _mappingUI.AssemblyVersion;
            if (assemblyVersion == null)
            {
                assemblyVersion = Tuple.Create(0, 0, 0, 0);
            }
            _ctrVersion1.Text = assemblyVersion.Item1.ToString();
            _ctrVersion2.Text = assemblyVersion.Item2.ToString();
            _ctrVersion3.Text = assemblyVersion.Item3.ToString();
            _ctrVersion4.Text = assemblyVersion.Item4.ToString();

            // file version
            var fileVersion = _mappingUI.FileVersion;
            if (fileVersion == null)
            {
                fileVersion = Tuple.Create(0, 0, 0, 0);
            }
            _ctrFileVersion1.Text = fileVersion.Item1.ToString();
            _ctrFileVersion2.Text = fileVersion.Item2.ToString();
            _ctrFileVersion3.Text = fileVersion.Item3.ToString();
            _ctrFileVersion4.Text = fileVersion.Item4.ToString();

            // key file
            if (_mappingUI.KeyFile != null)
            {
                _keyFile = _mappingUI.KeyFile;
                _checkSignAssembly.Checked = true;
                _ctrKeyFile.Text = System.IO.Path.GetFileName(_mappingUI.KeyFile.FilePath);
                _ctrKeyFilePassword.Text = _keyFile.PfxPassword;
                _checkDelaySigning.Checked = _keyFile.IsDelaySigning;
                HandleSignAssemblyControl();
            }

            // tooltips
            _reset.SetToolTip("Reset", "Reset the assembly settings");
            _ctrKeyFile.SetToolTip("Strong name key file", "Choose a string name key file (.snk, .pfx) to sign the mapper assembly", 10);
            _btnAutoAssemblyVersion.SetToolTip("Auto", "Auto set the assembly version number based on the date and time\r\n" +
                "passed from 1 Jan, 2000.", 10);
            _btnAutoFileVersion.SetToolTip("Auto", "Auto set the file version number based on the date and time\r\n" +
                "passed from 1 Jan, 2000.", 10);
        }

        private void AddEventHandlers()
        {
            Tuple<int, int, int, int> version;
            _ctrVersion1.KeyUp += (o, e) => TryGetAssemblyVersion(out version);
            _ctrVersion2.KeyUp += (o, e) => TryGetAssemblyVersion(out version);
            _ctrVersion3.KeyUp += (o, e) => TryGetAssemblyVersion(out version);
            _ctrVersion4.KeyUp += (o, e) => TryGetAssemblyVersion(out version);
            _ctrFileVersion1.KeyUp += (o, e) => TryGetFileVersion(out version);
            _ctrFileVersion2.KeyUp += (o, e) => TryGetFileVersion(out version);
            _ctrFileVersion3.KeyUp += (o, e) => TryGetFileVersion(out version);
            _ctrFileVersion4.KeyUp += (o, e) => TryGetFileVersion(out version);

            // Auto:Click
            _btnAutoAssemblyVersion.Click += (o, e) =>
            {
                try
                {
                    // Log.LogUse(52);
                    SetAutoAssemblyVersion();
                }
                catch (System.Exception ex)
                {
                    Common.ShowUnknownException(ex);
                }
            };

            // Auto:Click
            _btnAutoFileVersion.Click += (o, e) =>
            {
                try
                {
                    // Log.LogUse(53);
                    SetAutoFileVersion();
                }
                catch (System.Exception ex)
                {
                    Common.ShowUnknownException(ex);
                }
            };

            // sign the assembly
            _checkSignAssembly.CheckedChanged += (o, e) =>
                {
                    try
                    {
                        // Log.LogUse(54);
                        HandleSignAssemblyControl();
                    }
                    catch (System.Exception ex)
                    {
                        Common.ShowUnknownException(ex);
                    }
                };

            _ctrKeyFile.Click += (o, e) =>
                {
                    try
                    {
                        if (BrowseKeyFile())
                        {
                            _ctrKeyFile.LinkColor = Color.FromName("HotTrack");

                            if (_keyFile.IsPfx)
                            {
                                _ctrKeyFilePasswordLabel.Visible = true;
                                _ctrKeyFilePassword.Visible = true;
                            }
                            else
                            {
                                _ctrKeyFilePasswordLabel.Visible = false;
                                _ctrKeyFilePassword.Visible = false;
                            }
                        }
                    }
                    catch (System.Exception ex)
                    {
                        Common.ShowUnknownException(ex);
                    }
                };

            _reset.Click += (o, e) =>
                {
                    Reset();
                };
        }

        private void Reset()
        {
            try
            {
                _ctrVersion1.Text = "0";
                _ctrVersion2.Text = "0";
                _ctrVersion3.Text = "0";
                _ctrVersion4.Text = "0";
                _ctrFileVersion1.Text = "0";
                _ctrFileVersion2.Text = "0";
                _ctrFileVersion3.Text = "0";
                _ctrFileVersion4.Text = "0";
                _checkSignAssembly.Checked = false;
                _checkDelaySigning.Checked = false;
                _ctrKeyFile.Text = "No key file selected";
                _ctrKeyFilePassword.Text = null;
                _keyFile = new KeyFile();
                _ctrAssemblyVersionMessage.Visible = false;
                _ctrFileVersionMessage.Visible = false;
                HandleSignAssemblyControl();
            }
            catch (System.Exception ex)
            {
                Common.ShowUnknownException(ex);
            }
        }

        #region Version

        // check if version value is valid (between 0 and 65534)
        private bool CheckVersionValue(string value, out int result)
        {
            result = 0;   // default value

            if (String.IsNullOrWhiteSpace(value))
            {
                return true;
            }

            if (!int.TryParse(value, out result))
            {
                return false;
            }

            if (result < 0 || result > 65534)
            {
                return false;
            }

            return true;
        }

        // get assembly version
        private bool TryGetAssemblyVersion(out Tuple<int, int, int, int> version)
        {
            version = null;

            try
            {
                int part1, part2, part3, part4;

                _ctrAssemblyVersionMessage.Visible = false;

                // part1
                if (!CheckVersionValue(_ctrVersion1.Text, out part1))
                {
                    _ctrAssemblyVersionMessage.Visible = true;
                    return false;
                }

                // part2
                if (!CheckVersionValue(_ctrVersion2.Text, out part2))
                {
                    _ctrAssemblyVersionMessage.Visible = true;
                    return false;
                }

                // part3
                if (!CheckVersionValue(_ctrVersion3.Text, out part3))
                {
                    _ctrAssemblyVersionMessage.Visible = true;
                    return false;
                }

                // part4
                if (!CheckVersionValue(_ctrVersion4.Text, out part4))
                {
                    _ctrAssemblyVersionMessage.Visible = true;
                    return false;
                }

                // set version
                version = Tuple.Create(part1, part2, part3, part4);
                return true;
            }
            catch (System.Exception ex)
            {
                Common.ShowUnknownException(ex);
                return false;
            }
        }

        // get assembly version
        private bool TryGetFileVersion(out Tuple<int, int, int, int> version)
        {
            version = null;

            try
            {
                int part1, part2, part3, part4;

                _ctrFileVersionMessage.Visible = false;

                // part1
                if (!CheckVersionValue(_ctrFileVersion1.Text, out part1))
                {
                    _ctrFileVersionMessage.Visible = true;
                    return false;
                }

                // part2
                if (!CheckVersionValue(_ctrFileVersion2.Text, out part2))
                {
                    _ctrFileVersionMessage.Visible = true;
                    return false;
                }

                // part3
                if (!CheckVersionValue(_ctrFileVersion3.Text, out part3))
                {
                    _ctrFileVersionMessage.Visible = true;
                    return false;
                }

                // part4
                if (!CheckVersionValue(_ctrFileVersion4.Text, out part4))
                {
                    _ctrFileVersionMessage.Visible = true;
                    return false;
                }

                // set version
                version = Tuple.Create(part1, part2, part3, part4);
                return true;
            }
            catch (System.Exception ex)
            {
                Common.ShowUnknownException(ex);
                return false;
            }
        }

        // set auto assembly version
        private void SetAutoAssemblyVersion()
        {
            var buildNumber = (int)DateTime.Now.Subtract(DateTime.Parse("2000-01-01")).TotalDays;
            var revisionNumber = (int)(DateTime.Now.TimeOfDay.TotalSeconds / 2);
            _ctrVersion3.Text = buildNumber.ToString();
            _ctrVersion4.Text = revisionNumber.ToString();
        }

        // set auto file version
        private void SetAutoFileVersion()
        {
            var buildNumber = (int)DateTime.Now.Subtract(DateTime.Parse("2000-01-01")).TotalDays;
            var revisionNumber = (int)(DateTime.Now.TimeOfDay.TotalSeconds / 2);
            _ctrFileVersion3.Text = buildNumber.ToString();
            _ctrFileVersion4.Text = revisionNumber.ToString();
        }

        #endregion

        #region Assembly Signing

        // init
        private void HandleSignAssemblyControl()
        {
            if (_isSigned)
            {
                _ctrKeyFile.Visible = true;
                _ctrKeyFileLabel.Visible = true;
                _checkDelaySigning.Visible = true;

                // pfx => password
                if (_keyFile.IsPfx)
                {
                    _ctrKeyFilePasswordLabel.Visible = true;
                    _ctrKeyFilePassword.Visible = true;
                }
            }
            else
            {
                _ctrKeyFile.Visible = false;
                _ctrKeyFileLabel.Visible = false;
                _ctrKeyFilePasswordLabel.Visible = false;
                _ctrKeyFilePassword.Visible = false;
                _checkDelaySigning.Visible = false;
                _ctrPfxException.Visible = false;
            }
        }

        // browse key file
        private bool BrowseKeyFile()
        {
            var browser = new OpenFileDialog();
            browser.Title = "Browse key files";
            browser.Filter = "Key Files (*.snk, *.pfx)|*.snk;*.pfx";

            // show mapper directory
            browser.InitialDirectory = System.IO.Directory.GetCurrentDirectory();

            if (browser.ShowDialog() == DialogResult.OK)
            {
                _keyFile.FilePath = browser.FileName;
                _ctrKeyFile.Text = System.IO.Path.GetFileName(_keyFile.FilePath);
                _ctrKeyFilePassword.Text = null;
                return true;
            }

            return false;
        }

        // check key file
        private bool CheckKeyFile(StringBuilder message)
        {
            // reset key file in parent control
            _mappingUI.KeyFile = null;

            // defaults
            _ctrKeyFile.LinkColor = Color.FromName("HotTrack");
            _ctrPfxException.Visible = false;
            _ctrPfxException.Text = "";

            if (_isSigned)
            {
                _keyFile.IsDelaySigning = _checkDelaySigning.Checked;

                // any file
                if (String.IsNullOrWhiteSpace(_keyFile.FilePath))
                {
                    _ctrKeyFile.LinkColor = Color.Red;
                    _ctrKeyFile.Text = "No key file selected";
                    return false;
                }

                // has password (if pfx)
                _keyFile.PfxPassword = _ctrKeyFilePassword.Text;
                if (_keyFile.IsPfx && String.IsNullOrEmpty(_keyFile.PfxPassword))
                {
                    _ctrPfxException.Text = "The .pfx password is not given.";
                    _ctrPfxException.Visible = true;
                    return false; 
                }

                // check .pfx
                if (_keyFile.IsPfx)
                {
                    System.Exception ex;
                    var cert = Common.TryCreateCertificate(_keyFile, out ex);
                    if (ex != null)
                    {
                        _ctrPfxException.Text = ex.Message;
                        _ctrPfxException.Visible = true;
                        return false;
                    }
                }

                _mappingUI.KeyFile = _keyFile;
            }

            return true;
        }

        #endregion

        private void _linkBack_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                StringBuilder message = new StringBuilder();

                // priority: key file
                if (!CheckKeyFile(message))
                {
                    return;
                }

                // assembly version
                Tuple<int, int, int, int> assemblyVersion;
                if (!TryGetAssemblyVersion(out assemblyVersion))
                {
                    return;
                }

                // file version
                Tuple<int, int, int, int> fileVersion;
                if (!TryGetFileVersion(out fileVersion))
                {
                    return;
                }

                // ok
                _mappingUI.AssemblyVersion = assemblyVersion;
                _mappingUI.FileVersion = fileVersion;
                _mappingUI.MainUI.ShowMainPage();

                // refresh settings icon
                _mappingUI.RefreshLinks();
            }
            catch (System.Exception ex)
            {
                Common.ShowUnknownException(ex);
            }
        }

        private void _imageVideo_Click(object sender, EventArgs e)
        {
            // Log.LogUse(72);
            Common.OpenBrowser(Globals.GlobalResources.AppMapperSettingsVideo);
        }
    }
}
