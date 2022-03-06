using Calculator.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Calculator.ViewModels
{
    class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        void OnProportyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        bool Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnProportyChanged(PropertyName);
            return true;
        }

        private Ariph calculation;

        private string lastOperation;
        private bool newDisplayRequired = false;
        private string display;


        public string FirstOperand
        {
            get => calculation.FirstOperand;
            set { calculation.FirstOperand = value; }
        }

        public string SecondOperand
        {
            get => calculation.SecondOperand;
            set { calculation.SecondOperand = value; }
        }

        public string Operation
        {
            get => calculation.Operation;
            set { calculation.Operation = value; }
        }

        public string LastOperation
        {
            get => lastOperation;
            set { lastOperation = value; }
        }

        public string Result
        {
            get => calculation.Result;
        }

        public string Display
        {
            get => display;
            set => Set(ref display, value);
        }



        public MainWindowViewModel()
        {
            calculation = new Ariph();

            display = "0";
            FirstOperand = string.Empty;
            SecondOperand = string.Empty;
            Operation = string.Empty;
            lastOperation = string.Empty;

            //Команды            
            OperationButtonPress = new RelayCommand(OnOperationButtonPressCommandExecute, CanOperationButtonPressCommandExecuted);
            SingularOperationButtonPress = new RelayCommand(OnSingularOperationButtonPressCommandExecute, CanSingularOperationButtonPressCommandExecuted);
            ButtonPress = new RelayCommand(OnDigitButtonPressCommandExecute, CanDigitButtonPressCommandExecuted);
            
        }

        private bool CanCopyCommandExecuted(object p) => true;

        /// <summary>Команда обработчик нажатия на кнопки оперций с двумя операндами</summary>
        public ICommand OperationButtonPress { get; }

        private void OnOperationButtonPressCommandExecute(object p)
        {
            if (FirstOperand == string.Empty || LastOperation == "=")
            {
                FirstOperand = display;
                LastOperation = p.ToString();
            }
            else
            {
                SecondOperand = display;
                Operation = lastOperation;
                calculation.CalculateResult();

                if (Operation == "/" && SecondOperand == "0")
                {
                    Display = "Ошибка";
                    newDisplayRequired = true;
                    return;
                }
                else
                {
                    LastOperation = p.ToString();
                    Display = Result;
                    FirstOperand = display;
                }
            }
            newDisplayRequired = true;
        }

        private bool CanOperationButtonPressCommandExecuted(object p) => true;

        /// <summary>Команда обработчик нажатия на кнопки оперций с одним операндом</summary>
        public ICommand SingularOperationButtonPress { get; }

        private void OnSingularOperationButtonPressCommandExecute(object p)
        {
            FirstOperand = Display;
            Operation = p.ToString();
            calculation.CalculateResult();

            if (Operation == "1/x" && FirstOperand == "0" || Operation == "√х" && Convert.ToDouble(FirstOperand) < 0)
            {
                Display = "Ошибка";
                newDisplayRequired = true;
                return;
            }
            else
            {
                LastOperation = "=";
                Display = Result;
                FirstOperand = display;
                newDisplayRequired = true;
            }
        }

        private bool CanSingularOperationButtonPressCommandExecuted(object p) => true;

        /// <summary>Команда обработчик нажатия на цифровые кнопки</summary>
        public ICommand ButtonPress{ get; }

        private void OnDigitButtonPressCommandExecute(object p)
        {             
            switch (p)
            {
                case "C":
                    Display = "0";
                    FirstOperand = string.Empty;
                    SecondOperand = string.Empty;
                    Operation = string.Empty;
                    LastOperation = string.Empty;                   
                    break;
                case "CE":
                    Display = "0";
                    SecondOperand = string.Empty;
                    break;
                case "←":
                    Display = display.Length > 1 ? display.Substring(0, display.Length - 1) : "0";
                    break;
                case "±":
                    Display = display.Contains("-") ? display.Remove(display.IndexOf("-"), 1) : "-" + display;
                    break;
                case ".":
                    if (newDisplayRequired)
                    {
                        Display = "0.";
                    }
                    else
                    {
                        if (!display.Contains("."))
                        {
                            Display = display + ".";
                        }
                    }
                    break;
                default:
                    Display = display == "0" || newDisplayRequired ? p.ToString() : display + p.ToString();
                    break;
            }
            newDisplayRequired = false;
        }
        private bool CanDigitButtonPressCommandExecuted(object p) => true;
    }
}