using System;
using System.Windows.Forms;

namespace QueryTalk.Mapper
{

    static class Program
    {
        internal static License License { get; private set; }

        internal static Connect Connect { get; private set; }

        internal static Start Start { get; private set; }

        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                License = new License();
                Connect = new Connect();
                Start = new Start();
                Start.Initialize();

                var mainForm = new MainForm();
                mainForm.Initialize();
                Application.Run(mainForm);
            }
            catch (Exception ex)
            {
                Common.ShowApplicationCrashedException(ex);
            }
            finally
            {
                // Log.LogUse(999);
                System.Threading.Thread.Sleep(200);
            }
        }
    }
}
