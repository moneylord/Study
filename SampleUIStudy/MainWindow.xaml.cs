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
            ViewModel.OnCheckedChangedRBtn = CheckedChangedRBtn;

            // Default Disable setting.
            CheckedChangedRBtn(1);
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

        public void OnCheckedChanged(object sender, RoutedEventArgs args)
        {
            try
            {
                var rb = sender as RadioButton;

                if (rb.Name == "rBtn1")
                {
                    CheckedChangedRBtn(1);
                    VisibleListBoxReport(false);
                    VisibleListBoxRest(false);
                }
                else if (rb.Name == "rBtn2")
                {
                    CheckedChangedRBtn(2);
                    VisibleListBoxRest(false);
                }
                else
                {
                    CheckedChangedRBtn(3);
                    VisibleListBoxReport(false);
                }
            }
            catch (Exception)
            {

            }
        }

        public void CheckedChangedRBtn(int idx)
        {
            switch(idx)
            {
                case 1:
                    {
                        gridReportServer.IsEnabled = false;
                        gridRestService.IsEnabled = false;
                    }
                    break;
                case 2:
                    {
                        gridReportServer.IsEnabled = true;
                        gridRestService.IsEnabled = false;
                    }
                    break;
                case 3:
                    {
                        gridReportServer.IsEnabled = false;
                        gridRestService.IsEnabled = true;
                    }
                    break;
                default:
                    break;
            }
        }

        private void btnGroupItem_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                var test = sender as Button;

                SolidColorBrush b = new SolidColorBrush(Colors.Pink);

                test.Background = b;
            }
            catch(Exception)
            {

            }
        }

        private void btnGroupItem_MouseLeave(object sender, MouseEventArgs e)
        {
            try
            {
                var test = sender as Button;

                SolidColorBrush b = new SolidColorBrush(Colors.Green);

                test.Background = b;
            }
            catch (Exception)
            {

            }
        }
    }
}
