﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab5
{
    [Serializable]
    enum Update
    {
        Add,
        Replace,
        Property
    }
    [Serializable]
    internal class Edition: INotifyPropertyChanged
    {
        protected string title;
        protected DateTime release;
        protected int circulation;

        public event PropertyChangedEventHandler PropertyChanged;

        public Edition(string title, DateTime release, int circulation)
        {
            this.title = title;
            this.release = release;
            this.circulation = circulation;
        }
        public Edition() : this("Anonymous", new DateTime(1, 1, 1), 0) { }
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        public DateTime Release
        {
            get { return release; }
            set { release = value; OnPropertyChanged("Изменили поле release"); }
        }
        public int Circulation
        {
            get { return circulation; }
            set
            {
                if (value < 0) throw new FormatException("Тираж должен быть положительным числом!");
                circulation = value;
                OnPropertyChanged("Изменили поле circulation");
            }
        }
        public virtual object DeepCopy()
        {
            return new Edition(title, release, circulation);
        }
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            return title == ((Edition)obj).title && release == ((Edition)obj).release && circulation == ((Edition)obj).circulation;
        }
        public static bool operator ==(Edition lhs, Edition rhs) { return lhs.Equals(rhs); }
        public static bool operator !=(Edition lhs, Edition rhs) { return !lhs.Equals(rhs); }
        public override int GetHashCode()
        {
            return (title + release + circulation).GetHashCode();
        }
        public override string ToString()
        {
            return title + " " + release + " " + circulation;
        }

        // Реализация Интерфейса INotifyPropertyChanged
        /*При использовании атрибута CallerMemberName в вызовах метода NotifyPropertyChanged 
          нет необходимости указывать имя свойства в качестве строкового аргумента.*/
        private void OnPropertyChanged([CallerMemberName] String propertyName = "")
        {
            //Суть метода Invoke: он принимает делегат и выполняет его в том потоке, в котором был создан элемент управления, у которого вызывается Invoke.
             
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}