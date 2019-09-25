using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FromPractice.ViewModels
{
    public class PersonCollectionViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        bool isEditing;
        PersonViewModel personEdit;
        public bool IsEditing
        {
            private set { SetProperty(ref isEditing, value); }
            get { return isEditing; }
        }

        public PersonViewModel PersonEdit
        {
            set { SetProperty(ref personEdit, value); }
            get { return personEdit; }
        }
        bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (Object.Equals(storage, value))
            {
                return false;
            }
            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //对 ICommand 类型的三个属性以及 Persons 属性的更改不会触发 PropertyChanged 事件。 这些属性都是在类首次创建时设置的，此后不会更改。

        //“新建”、“提交”和“取消”按钮的所有逻辑,,构造函数中将这三个属性设置为 Command 类型的对象
        public ICommand NewCommand { private set; get; }

        public ICommand SubmitCommand { private set; get; }

        public ICommand CancelCommand { private set; get; }

        /// <summary>
        /// 用于保存填写的数据，并且展示在ListView中
        /// </summary>
        public IList<PersonViewModel> Persons { get; } = new ObservableCollection<PersonViewModel>();

        public PersonCollectionViewModel()
        {
            //执行初始化
            NewCommand = new Command(
                execute: () =>{    //按钮按下时执行excute方法，是个Action（无参数无返回的方法）
                    PersonEdit = new PersonViewModel();
                    PersonEdit.PropertyChanged += PersonEdit_PropertyChanged;  //注册PersonViewModel的属性更新事件
                    IsEditing = true;
                    RefreshCanExecutes();
                },
                canExecute:()=>{ return !IsEditing; });
            //初始IsEditing=false，canExecute返回true，按钮可用； 单击按钮，执行execute后，IsEditing=true，则返回false，Button禁用其自身（即单击后，则立即禁用）

            SubmitCommand = new Command(
                execute: () =>
                {
                    PersonEdit.PropertyChanged -= PersonEdit_PropertyChanged;
                    Persons.Add(PersonEdit);
                    PersonEdit = null;   //清空 填写栏
                    IsEditing = false;
                    RefreshCanExecutes();
                },
                canExecute: () =>   /*提交按钮可用的条件*/
                {
                    return PersonEdit != null &&
                           PersonEdit.Name != null &&
                           PersonEdit.Name.Length > 1 &&
                           PersonEdit.Age > 0;
                });

            CancelCommand = new Command(
                execute: () =>
                {
                    PersonEdit.PropertyChanged -= PersonEdit_PropertyChanged;
                    PersonEdit = null;
                    IsEditing = false;
                    RefreshCanExecutes();
                },
                canExecute: () => /*初始IsEditing=false，按钮不可用*/
                {
                    return IsEditing;
                });
        }

        private void PersonEdit_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            //调用 ChangeCanExecute 将导致 Command 类触发 CanExecuteChanged 方法。 
            //Button 已为该事件附加了一个处理程序，并通过再次调用 CanExecute 进行响应，然后根据该方法的返回值启用自身。
            (SubmitCommand as Command).ChangeCanExecute();
        }
        void RefreshCanExecutes()
        {
            (NewCommand as Command).ChangeCanExecute();
            (SubmitCommand as Command).ChangeCanExecute();
            (CancelCommand as Command).ChangeCanExecute();
        }
    }
}
