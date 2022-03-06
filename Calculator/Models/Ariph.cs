using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Models
{
    public class Ariph
    {
        private string result;

        public Ariph(string firstOperand, string secondOperand, string operation)
        {
            FirstOperand = firstOperand;
            SecondOperand = secondOperand;
            Operation = operation;
            result = string.Empty;
        }

        public Ariph(string firstOperand, string operation)
        {
            FirstOperand = firstOperand;
            SecondOperand = string.Empty;
            Operation = operation;
            result = string.Empty;
        }

        public Ariph()
        {
            FirstOperand = string.Empty;
            SecondOperand = string.Empty;
            Operation = string.Empty;
            result = string.Empty;
        }

        public string FirstOperand { get; set; }
        public string SecondOperand { get; set; }
        public string Operation { get; set; }
        public string Result { get { return result; } }

        public void CalculateResult()
        {
            switch (Operation)
            {
                case ("+"):
                    result = (Convert.ToDouble(FirstOperand) + Convert.ToDouble(SecondOperand)).ToString();
                    break;

                case ("-"):
                    result = (Convert.ToDouble(FirstOperand) - Convert.ToDouble(SecondOperand)).ToString();
                    break;

                case ("*"):
                    result = (Convert.ToDouble(FirstOperand) * Convert.ToDouble(SecondOperand)).ToString();
                    break;

                case ("/"):
                    result = (Convert.ToDouble(FirstOperand) / Convert.ToDouble(SecondOperand)).ToString();
                    break;

                case ("%"):
                    result = (Convert.ToDouble(FirstOperand) / 100).ToString();
                    break;

                case ("√x"):
                    result = Convert.ToDouble(FirstOperand) < 0 ? "Ошибка" : Math.Sqrt(Convert.ToDouble(FirstOperand)).ToString();
                    break;

                case ("x²"):
                    result = Math.Pow(Convert.ToDouble(FirstOperand), 2).ToString();
                    break;

                case ("1/x"):
                    result = Convert.ToDouble(FirstOperand) == 0 ? "Ошибка" : (1 / Convert.ToDouble(FirstOperand)).ToString();
                    break;
            }
        }
    }
}
