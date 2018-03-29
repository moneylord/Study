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
using System.Windows.Controls.Primitives;

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

        public void AddListItem(cListItem _item)
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
				cListItem citem = new cListItem();
				citem.CImageName = "crystal.png";
				citem.Value = 10;
				citem.CImagePath = @"C:\Users\Partner\Desktop\Sample Attach Data\crystal.png";

				cListItem citem2 = new cListItem();
				citem2.CImageName = "Down.png";
				citem2.Value = -10;
				citem2.CImagePath = @"C:\Users\Partner\Desktop\Sample Attach Data\Down.png";

				cListItem citem3 = new cListItem();
				citem3.CImageName = "star.png";
				citem3.Value = 0;
				citem3.CImagePath = @"C:\Users\Partner\Desktop\Sample Attach Data\star.png";

				List<cListItem> ciList = new List<cListItem>();

				ciList.Add(citem);
				ciList.Add(citem2);
				ciList.Add(citem3);
				ciList.Add(citem);
				ciList.Add(citem3);
				ciList.Add(citem2);

				CListItem = new ObservableCollection<cListItem>(ciList);				
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
        public ObservableCollection<cListItem> _linkListItem { get; set; }
        public ObservableCollection<cListItem> LinkListItem
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
                    if (null != _ as cListItem)
                    {
                        var item = _ as cListItem;
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
                    if (null != _ as cListItem)
                    {
                        var item = _ as cListItem;
                        SelectedItemRest = item;

                        OnVisibleRestServiceList(false);
                    }
                }));
            }
        }

        public cListItem _selectedItems;
        public cListItem SelectedItem
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

        public cListItem _selectedItemsRest;
        public cListItem SelectedItemRest
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
                var gd = obj as Popup;

                if( gd.Name == "ListContainerRest" )
                {
                }
                else
                {
					if( gd.IsOpen == false )
						gd.IsOpen = true;
					else
						gd.IsOpen = false;
				}
            }
            catch(Exception)
            {

            }
        }
    

		public string ImagePath
		{
			get { return @"C:\Users\Partner\Desktop\Sample Attach Data\crystal.png"; }
		}

		public ObservableCollection<cListItem> _cListItem { get; set; }
		public ObservableCollection<cListItem> CListItem
		{
			get { return _cListItem; }
			set
			{
				_cListItem = value;
				OnPropertyChanged("CListItem");
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

	public class cListItem : INotifyPropertyChanged
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

		private int _value;
		public int Value
		{
			get
			{
				return _value;
			}
				
			set
			{
				_value = value;
				if (_value < 0)
				{
					ListItemVisible = Visibility.Hidden;
					BackColor = new SolidColorBrush(Colors.Tomato);
				}
				else
				{
					ListItemVisible = Visibility.Visible;
					if (_value == 0)
						BackColor = new SolidColorBrush(Colors.Transparent);
					else
						BackColor = new SolidColorBrush(Colors.Turquoise);
				}
			}
		}

		private Visibility _listItemVisible = Visibility.Visible;
		public Visibility ListItemVisible
		{
			get
			{
				return _listItemVisible;
			}
			set
			{
				_listItemVisible = value;
				OnPropertyChanged("ListItemVisible");
			}
		}

		private SolidColorBrush _backColor = new SolidColorBrush(Colors.Transparent);
		public SolidColorBrush BackColor
		{
			get
			{
				return _backColor;
			}
			set
			{
				_backColor = value;
				OnPropertyChanged("BackColor");
			}
		}

		public string CImageName { get; set; }
		public string CImagePath { get; set; }		
	}

    public delegate void ListVisivilityAction(bool bOpen);
    public delegate void CheckedChangedRadioBtn(int index);
    public delegate void ListItemColorAction();
}
