using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace TrackApp
{
    public class AppViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Category> _categories = new ObservableCollection<Category>();
        private Category _selectedCategory;
        private ObservableCollection<Element> _currentElements;

        public ObservableCollection<Category> Categories
        {
            get { return _categories; }
        }

        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set { _selectedCategory = value; OnPropertyChanged(); OnPropertyChanged("CurrentElements");}
        }

        public ObservableCollection<Element> CurrentElements
        {
            get { return SelectedCategory ==null?null: SelectedCategory.Elements ; }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
