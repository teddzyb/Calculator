using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace Calculator.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainCalculator : ContentView
    {
        private string equation = "";
        private string current = "0";
        private string answer = "0";
        private bool isOperator = false; 

        public MainCalculator()
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
                    {
                        current += ".";
                    }
                    isOperator = true;
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

                case "%":
                    if (!isOperator)
                        equation += current;
                    current = "%";
                    isOperator = true;
                    break;

                case "C":
                    equation = "";
                    answer = "";
                    current = "0";
                    clearBtn.Text = "AC";
                    break;

                case "AC":
                    equation = "";
                    answer = "";
                    current = "0";
                    break;

                case "DEL":
                    if (current.Length > 0 && current != "0")
                    {
                        current = current.Remove(current.Length - 1);
                        if (current.Length == 0 && equation != "")
                        {
                            current = equation.Remove(0, equation.Length - 1);
                            equation = equation.Remove(equation.Length - 1);
                        }
                        else if (current.Length == 0)
                        {
                            current = "0";
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
                            answer = eq.ToString();
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

            if (current.Length > 0 && current != "0" && current != "")
            {
                clearBtn.Text = "C";
                if (current[0] == '0')
                {
                    current = current.Remove(0, 1);
                }
                if (!isOperator)
                {
                    try
                    {
                        var expression = ParseExpression(equation + current);

                        DataTable dt = new DataTable();
                        double eq = Convert.ToDouble(dt.Compute(expression, ""));

                        answer = eq.ToString();
                    }
                    catch (Exception) { }
                }
            }

            // Output:
            if ((equation != "" && key.Text != "=") || int.TryParse(key.Text, out _) || (key.Text == "DEL" && current != "0"))
            {
                input1.FontSize = 35;
                input2.FontSize = 50;
                input1.TextColor = Color.Gray;
                input2.TextColor = Color.Black;

                input1.Text = "= " + answer;
                input2.Text = equation + current;
            }
            else
            {
                input1.FontSize = 50;
                input2.FontSize = 35;
                input1.TextColor = Color.Black;
                input2.TextColor = Color.Gray;

                if (key.Text == "=")
                {
                    input1.Text = answer;
                    input2.Text = "";
                }
                else if (key.Text == "C" || key.Text == "AC" || current == "0")
                {
                    input1.Text = "0";
                    input2.Text = "";
                }
                else
                {
                    input1.Text = "= " + answer;
                    input2.Text = equation + current;
                }
            }

        }

        private string ParseExpression(string input)
        {
            string ex = input.Replace("−", "-").Replace("×", "*").Replace("÷", "/");
            ex += ex.Contains('.') || ex.Contains('%') ? "" : ".0";
            return ex;
        }

    }
}