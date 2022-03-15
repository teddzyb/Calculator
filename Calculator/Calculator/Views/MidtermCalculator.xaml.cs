using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

using Calculator;

namespace Calculator.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MidtermCalculator : ContentView
    {
        private string equation = "";
        private string current = "0";
        private bool isOperator = false;

        public MidtermCalculator()
        {
            InitializeComponent();
        }

        private void Key_Pressed(object sender, EventArgs e)
        {
            HapticFeedback.Perform(HapticFeedbackType.Click);

            Button clearBtn = this.FindByName<Button>("ClearBtn");
            Label input1 = this.FindByName<Label>("Input1");
            Label input2 = this.FindByName<Label>("Input2");
            Button key = (Button)sender;

            if (int.TryParse(key.Text, out _))
                isOperator = false;

            switch (key.Text)
            {
                case "1": current += "1"; break;
                case "2": current += "2"; break;
                case "3": current += "3"; break;
                case "4": current += "4"; break;
                case "5": current += "5"; break;
                case "6": current += "6"; break;
                case "7": current += "7"; break;
                case "8": current += "8"; break;
                case "9": current += "9"; break;
                case "0": current += "0"; break;

                case ".":
                    if (!current.Contains("."))
                        current += ".";
                    break;

                case "+":
                    if (!isOperator)
                        equation += current;
                    current = "+";
                    isOperator = true;
                    break;

                case "−":
                    if (!isOperator)
                        equation += current;
                    current = "−";
                    isOperator = true;
                    break;

                case "×":
                    if (!isOperator)
                        equation += current;
                    current = "×";
                    isOperator = true;
                    break;

                case "÷":
                    if (!isOperator)
                        equation += current;
                    current = "÷";
                    isOperator = true;
                    break;

                case "C":
                    equation = "";
                    current = "0";
                    clearBtn.Text = "AC";
                    break;

                case "AC":
                    equation = "";
                    current = "0";
                    break;

                case "DEL":
                    if (current.Length > 0 && current != "0")
                    {
                        current = current.Remove(current.Length - 1);
                        if (current.Length == 0 && equation == "")
                        {
                            current = "0";
                            clearBtn.Text = "AC";
                        }
                    }
                    break;

                case "=":
                    equation += current;
                    if (equation != "")
                    {
                        try
                        {
                            var expression = ParseExpression(equation);

                            DataTable dt = new DataTable();
                            double eq = Convert.ToDouble(dt.Compute(expression, ""));

                            current = eq.ToString();
                            equation = "";
                        }
                        catch (Exception ex)
                        {
                            App.Current.MainPage.DisplayAlert("Error", ex.Message, "Back");
                        }
                    }
                    break;

                default:
                    break;
            }

            if (current.Length > 0 && current != "0")
            {
                clearBtn.Text = "C";
                if (current[0] == '0')
                {
                    current = current.Remove(0, 1);
                }
            }

            // Output:
            input2.Text = equation;
            input1.Text = current;
        }

        private string ParseExpression(string input)
        {
            string ex = input.Replace("−", "-").Replace("×", "*").Replace("÷", "/");
            ex += ex.Contains('.') || ex.Contains('%') ? "" : ".0";
            return ex;
        }
    }
}
