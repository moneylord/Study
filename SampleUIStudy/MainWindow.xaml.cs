using System;
using System.Collections.Generic;
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

namespace SampleUIStudy
{
	/// <summary>
	/// MainWindow.xaml에 대한 상호 작용 논리
	/// </summary>
	public partial class MainWindow : Window
	{
        public MainWindowVM ViewModel
        {
            get { return DataContext as MainWindowVM; }
        }

        public MainWindow()
		{
			InitializeComponent();

            ViewModel.OnVisibleReportServerList = VisibleListBoxReport;
            ViewModel.OnVisibleRestServiceList = VisibleListBoxRest;
		}

        public void VisibleListBoxReport(bool _bVisible)
        {
            if (_bVisible)
                ViewModel.ReportServerListVisible = Visibility.Visible;
            else
                ViewModel.ReportServerListVisible = Visibility.Hidden;
        }

        public void VisibleListBoxRest(bool _bVisible)
        {
            if (_bVisible)
                ViewModel.RestServiceVisible = Visibility.Visible;
            else
                ViewModel.RestServiceVisible = Visibility.Hidden;
        }
	}
}
