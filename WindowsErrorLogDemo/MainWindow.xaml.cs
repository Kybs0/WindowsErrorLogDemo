using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WindowsErrorLogDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            ErrorTextBlock.Text = string.Empty;
            EventLog eventlog = new EventLog();
            //"Application"应用程序, "Security"安全, "System"系统
            eventlog.Log = "Application";
            List<EventLogEntry> eventLogEntryList = eventlog.Entries.Cast<EventLogEntry>().ToList();
            var recentErrorLogs = eventLogEntryList.Where(i => i.EntryType == EventLogEntryType.Error
                                                               &&i.TimeWritten>DateTime.Today).ToList();
            foreach (EventLogEntry entry in recentErrorLogs)
            {
                string info = string.Empty;
                info += "类型：" + entry.EntryType.ToString() + ";\r\n";
                info += "描述：" + entry.Message.ToString() + ";\r\n";
                info += "时间：" + entry.TimeWritten.ToString("yyyy-MM-dd HH:mm:ss") + ";\r\n";
                info += "来源：" + entry.Source.ToString() + ";\r\n";
                info += "\r\n***************************************************************************\r\n\r\n";
                ErrorTextBlock.Text += info;
            }
        }
    }
}
