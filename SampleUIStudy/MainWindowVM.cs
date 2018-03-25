using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;

namespace SampleUIStudy
{
    #region NotifyProperty Changed Implement
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class NotifyPropertyChangedInvocatorAttribute : System.Attribute
    {
        public NotifyPropertyChangedInvocatorAttribute() { }
        public NotifyPropertyChangedInvocatorAttribute(string parameterName)
        {
            ParameterName = parameterName;
        }

        public string ParameterName { get; private set; }
    }
    #endregion
    public class MainWindowVM : INotifyPropertyChanged
    {
        #region NotifyProperty chaned
        public event PropertyChangedEventHandler PropertyChanged;
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public MainWindowVM()
        {
            // Bind Command with Control
            BtnCommand = new RelayCommand(ShowPopupListBox);

            // Initialize List Item
            InitListItem();
        }

        public void AddListItem(LinkListItem _item)
        {
            try
            {
                LinkListItem.Add(_item);
            }
            catch (Exception)
            {
                System.Console.WriteLine("Exception In AddListItem( )...");
            }
        }

        public void InitListItem()
        {
            try
            {
                LinkListItem item = new LinkListItem();
                item.ItemName = "Test11111111";
                item.UserName = "rock";
                item.UserPwd = "qwerty";

                LinkListItem item2 = new LinkListItem();
                item2.ItemName = "Test2222222";
                item2.UserName = "iqos";
                item2.UserPwd = "1234";

                LinkListItem item3 = new LinkListItem();
                item3.ItemName = "Test3333333";
                item3.UserName = "Holis";
                item3.UserPwd = "coffee";

                List<LinkListItem> iList = new List<LinkListItem>();

                iList.Add(item);
                iList.Add(item2);
                iList.Add(item3);
                iList.Add(item2);
                iList.Add(item);
                iList.Add(item2);
                //iList.Add(item3);
                //iList.Add(item2);
                //iList.Add(item);

                LinkListItem = new ObservableCollection<LinkListItem>(iList);
            }
            catch (Exception)
            {

            }
        }

        public string BtnContent { get { return "..."; } }

        private ICommand btnCommand;
        public ICommand BtnCommand
        {
            get
            {
                return btnCommand;
            }
            set
            {
                btnCommand = value;
            }
        }

        /// <summary>
        /// If List Item need Extend Data then use this class object. 
        /// </summary>
        public ObservableCollection<LinkListItem> _linkListItem { get; set; }
        public ObservableCollection<LinkListItem> LinkListItem
        {
            get { return _linkListItem; }
            set
            {
                _linkListItem = value;
                OnPropertyChanged("LinkListItem");
            }
        }

        private RelayCommand<object> _onSelectedItemReport;
        public ICommand OnSelectedItemReport
        {
            get
            {
                return _onSelectedItemReport ?? (_onSelectedItemReport = new RelayCommand<object>(_ =>
                {
                    if (null != _ as LinkListItem)
                    {
                        var item = _ as LinkListItem;
                        SelectedItem = item;

                        OnVisibleReportServerList(false);
                    }
                }));
            }
        }

        private RelayCommand<object> _onSelectedItemRest;
        public ICommand OnSelectedItemRest
        {
            get
            {
                return _onSelectedItemRest ?? (_onSelectedItemRest = new RelayCommand<object>(_ =>
                {
                    if (null != _ as LinkListItem)
                    {
                        var item = _ as LinkListItem;
                        SelectedItemRest = item;

                        OnVisibleRestServiceList(false);
                    }
                }));
            }
        }

        public LinkListItem _selectedItems;
        public LinkListItem SelectedItem
        {
            get
            {
                return _selectedItems;
            }
            set
            {
                _selectedItems = value;
                OnPropertyChanged("SelectedItem");
            }
        }

        public LinkListItem _selectedItemsRest;
        public LinkListItem SelectedItemRest
        {
            get
            {
                return _selectedItemsRest;
            }
            set
            {
                _selectedItemsRest = value;
                OnPropertyChanged("SelectedItemRest");
            }
        }

        public void ShowPopupListBox(object obj)
        {
            try
            {
                var gd = obj as Grid;

                if( gd.Name == "ListContainerRest" )
                {
                    if (RestServiceVisible == Visibility.Hidden)
                        RestServiceVisible = Visibility.Visible;
                    else
                        RestServiceVisible = Visibility.Hidden;
                }
                else
                {
                    if (ReportServerListVisible == Visibility.Hidden)
                        ReportServerListVisible = Visibility.Visible;
                    else
                        ReportServerListVisible = Visibility.Hidden;
                }
            }
            catch(Exception)
            {

            }
        }

        private Visibility _reportServerListVisible = Visibility.Hidden;
        public Visibility ReportServerListVisible
        {
            get
            {
                return _reportServerListVisible;
            }
            set
            {
                _reportServerListVisible = value;
                OnPropertyChanged("ReportServerListVisible");
            }
        }

        private Visibility _restServiceVisible = Visibility.Hidden;
        public Visibility RestServiceVisible
        {
            get
            {
                return _restServiceVisible;
            }
            set
            {
                _restServiceVisible = value;
                OnPropertyChanged("RestServiceVisible");
            }
        }

        private Color _listItemBackgroundColor;
        public Color ListItemBackgroundColor
        {
            get
            {
                return _listItemBackgroundColor;
            }
            set
            {
                _listItemBackgroundColor = value;
                OnPropertyChanged("ListItemBackgroundColor");
            }
        }

        public ListVisivilityAction OnVisibleReportServerList { get; set; }
        public ListVisivilityAction OnVisibleRestServiceList { get; set; }

        public CheckedChangedRadioBtn OnCheckedChangedRBtn { get; set; }

        public ListItemColorAction OnMouseHover { get; set; }
    }

    public class LinkListItem
    {
        public string ItemName { get; set; }
        public string UserName { get; set; }
        public string UserPwd { get; set; }
    }

    public delegate void ListVisivilityAction(bool bOpen);
    public delegate void CheckedChangedRadioBtn(int index);
    public delegate void ListItemColorAction();
}
