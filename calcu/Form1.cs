using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace calcu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        double? value1 = null, value2 = null;
        double mid2, reserve = 0;//reserve是计数器
        string mid;//mid是多位数输入时的中间变量，mid2是换正负时的中间变量
        string c = "", k = "";//运算符
        string str1, str2;//显示
        int l, strl1 = 0, strl2 = 0;//sl为显示字符串的长度
        bool f1 = false, f2 = false, f3 = false, f4 = false, f5 = false, f6 = false, f7 = true, f8 = false, f9 = true, f10 = false,f11=false;
        //f1是表示一个计算是否结束，
        //f2表示运算符是否输入,
        //f3是否应该直接将运算符放到显示框2中,
        //f4表示是否从计数器中读出了数字,
        //f5表示是否输入了小树点,
        //f6表示是否应该重始显示框里的变量,
        //f7是否应输入数字,
        //f8读出数后是否应该重新输入,
        //f9表示数字是否需要和符号一起进入显示框
        //f10计算是否封闭
        //f11计算过程中当前是否应换数字
        private void btn_Click(object sender, EventArgs e)
        {
            Button btnn = (Button)sender;
            if (f10 == true)
            {
                System.Media.SystemSounds.Beep.Play();
            }
            else
            {    
               
                if (c != "") 
                { 
                    f2 = true;
                    f6 = false;
                }
                if (f1 == true)//这是重新开始的输入
                {
                    textBox1.Text = "0";
                    textBox2.Text = "";
                    f1 = false;
                    f2 = false;//运算符
                    f5 = false;
                    f6 = false;
                    c = "";
                }
                if (textBox1.Text == "0")
                {
                    textBox1.Text = btnn.Text;
                    if (f2 == true && f8 == false)
                    {
                        f8 = true;
                    }
                }
                else
                {

                    if (f2 == true)//有符号
                    {
                        if (f8 == false)//有符号后的第一次输入
                        {
                            textBox1.Text = "";
                            mid = textBox1.Text;
                            l = mid.Length;
                            mid = mid.Insert(l, btnn.Text);
                            textBox1.Text = mid;
                            f8 = true;
                        }
                        else
                        {
                            if (f11 == true)
                            {
                                textBox1.Text = btnn.Text;
                                int kl = textBox2.Text.Length;
                                string kk = textBox2.Text;
                                for (int i = kl - 1; i >= 0; i--)
                                {
                                    if (kk[i] == '+' || kk[i] == '-' || kk[i] == '*' || kk[i] == '/' || i == 0)
                                    {
                                        textBox2.Text = textBox2.Text.Remove(i + 1, kl - i - 1);
                                        break;
                                    }
                                }
                                f11 = false;
                            }
                            else
                            {
                                mid = textBox1.Text;
                                l = mid.Length;
                                mid = mid.Insert(l, btnn.Text);
                                textBox1.Text = mid;
                            }
                        }
                        if (f6 == true) { f6 = false; }
                    }
                    else//无符号
                    {
                        mid = textBox1.Text;
                        l = mid.Length;
                        mid = mid.Insert(l, btnn.Text);
                        textBox1.Text = mid;
                    }

                }
                f7 = true;
            }
        }


        private void buttonCalcu_Click(object sender, EventArgs e)
        {
            if (f10 == true)
            {
                System.Media.SystemSounds.Beep.Play();
            }
            else
            {
                Button btnc = (Button)sender;
                f7 = false;//接下来不应该输入数字
                if (c != "" && f1 == true)//运算符未空且计算已结束
                {
                    c = "";//清空运算符
                }
                f1 = false;//当前计算未结束
                f5 = false;//当前未输入小数点
                f8 = false;
                f11 = false;
                if (f6 == false)//不应该重置显示框2里的运算符变量
                {
                    if (textBox2.Text == "")//显示框2中无字符串
                    {
                        str1 = (double.Parse(textBox1.Text)).ToString();
                        strl1 = str1.Length;
                        str1 = str1.Insert(strl1, btnc.Text);
                        textBox2.Text = str1;
                    }
                    else
                    {
                        strl1 = (textBox2.Text).Length;
                        if (f3 == true)
                        {
                            textBox2.Text = textBox2.Text.Insert(strl1, btnc.Text);
                            f3 = false;
                        }
                        else
                        {
                            str2 = (double.Parse(textBox1.Text)).ToString();
                            strl2 = str2.Length;
                            str2 = str2.Insert(strl2, btnc.Text);
                            textBox2.Text = (textBox2.Text).Insert(strl1, str2);
                        }
                    }

                }
                else//应该重置显示框1里的运算符变量
                {
                    int ll;
                    ll = textBox2.Text.Length;
                    textBox2.Text = (textBox2.Text).Remove(ll - 1, 1);
                    textBox2.Text = (textBox2.Text).Insert(ll - 1, btnc.Text);
                }
                textBox1.Text = (double.Parse(textBox1.Text)).ToString();
                if (c== "")//运算符未输入
                {
                    value1 = double.Parse(textBox1.Text);
                    c = btnc.Name;
                    mid = "";
                    l = 0;
                }
                else//这不是第一次输入运算符
                {
                    value2 = double.Parse(textBox1.Text);
                    if (f6 != true)
                    {
                        if (c == "Addtion") { value1 = value1 + value2; }
                        if (c == "Substration") { value1 = value1 - value2; }
                        if (c == "Multiplication") { value1 = value1 * value2; }
                        if (c == "Division") { value1 = value1 / value2; }
                    }
                    if (c == "Division" && value2 == 0)
                    {
                        k = "除数不能为零";
                        f10 = true;
                    }
                    if (k == "除数不能为零")
                        textBox1.Text = k;
                    else
                        textBox1.Text = value1.ToString();
                    c = btnc.Name;
                    mid = "";
                    l = 0;
                }
                f6 = true;
            }
        }

        private void buttonDot_Click(object sender, EventArgs e)
        {
            if (f10 == true)
            {
                System.Media.SystemSounds.Beep.Play();
            }
            else
            {
                f8 = true;
                if (f1 != true && f7 == true&&f11==false)
                {
                    if (f5 == false)
                    {
                        if (textBox1.Text == "0" || f1 == true)
                        {
                            textBox1.Text = "0.";
                        }
                        else
                        {
                            mid = textBox1.Text;
                            l = mid.Length;
                            mid = mid.PadRight(l + 1, '.');
                            l++;
                            textBox1.Text = mid;
                        }
                        f5 = true;
                    }
                }
                else
                {
                    textBox1.Text = "0.";
                    f5 = true;
                    if (f1 == true && c != "")
                    {
                        c = "";
                    }
                    f1 = false;
                    f7 = true;
                    if (f11 == true)
                    {
                        int kl = textBox2.Text.Length;
                        string kk = textBox2.Text;
                        for (int i = kl - 1; i >= 0; i--)
                        {
                            if (kk[i] == '+' || kk[i] == '-' || kk[i] == '*' || kk[i] == '/' || i == 0)
                            {
                                if (i == 0)
                                {
                                    textBox2.Text = "";
                                }
                                else
                                {
                                    textBox2.Text = textBox2.Text.Remove(i + 1, kl - i - 1);
                                }
                                break;
                            }
                        }
                        f11 = false;
                    }
                    
                }
            }
        }

        private void buttonEqual_Click(object sender, EventArgs e)
        {
            if (f10 == true)
            {
                System.Media.SystemSounds.Beep.Play();
            }
            else
            {
                textBox2.Text = "";
                if (f1 != true)
                {
                    value2 = double.Parse(textBox1.Text);
                }
                if (f2 == false)
                {
                    textBox1.Text = (double.Parse(textBox1.Text)).ToString();
                }
                if (c == "Addtion")
                {
                    textBox1.Text = (value1 + value2).ToString();
                    value1 = double.Parse(textBox1.Text);
                }
                if (c == "Substration")
                {
                    textBox1.Text = (value1 - value2).ToString();
                    value1 = double.Parse(textBox1.Text);
                }
                if (c == "Multiplication")
                {
                    textBox1.Text = (value1 * value2).ToString();
                    value1 = double.Parse(textBox1.Text);
                }
                if (c == "Division")
                {
                    if (value2 != 0)
                    {
                        textBox1.Text = (value1 / value2).ToString();
                        value1 = double.Parse(textBox1.Text);
                    }
                    else
                    {
                        textBox1.Text = "除数不能为零";
                        f10 = true;
                    }
                }

                mid = "";
                l = 0;
                f1 = true;
                f2 = false;
                f3 = false;
                f6 = false;
            }
        }

        private void buttonInverse_Click(object sender, EventArgs e)
        {
            if (f10 == true)
            {
                System.Media.SystemSounds.Beep.Play();
            }
            else
            {
                if (textBox1.Text == "0")
                {
                    textBox1.Text = "除数不能为零";
                    System.Media.SystemSounds.Beep.Play();
                    f10 = true;
                }
                else
                {
                    if (value2 == null)
                    {
                        f2 = true;
                    }
                    int ml = 0;
                    string mid3 = "reciproc()";
                    string mmid2 = textBox2.Text;
                    ml = mmid2.Length;
                    if (textBox2.Text == "")
                    {
                        mid3 = mid3.Insert(9, (double.Parse(textBox1.Text)).ToString());
                        textBox2.Text += mid3;
                        textBox1.Text = (1 / (double.Parse(textBox1.Text))).ToString();
                    }
                    else
                    {
                        if (mmid2[ml - 1] != ')')
                        {
                            mid3 = mid3.Insert(9, (double.Parse(textBox1.Text)).ToString());
                            textBox2.Text += mid3;
                            textBox1.Text = (1 / (double.Parse(textBox1.Text))).ToString();
                        }
                        else
                        {
                            int kk = 0;
                            for (int i = ml - 1; i >= 0; i--)
                            {
                                if (mmid2[i] == '+' || mmid2[i] == '-' || mmid2[i] == '*' || mmid2[i] == '/' || i == 0)
                                {
                                    kk = i;
                                    break;
                                }
                            }
                            if (kk == 0)
                            {
                                textBox2.Text = textBox2.Text.Insert(kk, "reciproc(");
                            }
                            else
                            {
                                textBox2.Text = textBox2.Text.Insert(kk + 1, "reciproc(");
                            }
                            textBox2.Text += ")";
                            textBox1.Text = (1 / (double.Parse(textBox1.Text))).ToString();
                        }

                    }
                    if (value1 == null)
                    {
                        f1 = true;
                        f5 = true;
                        mid = "";
                        ml = 0;
                        f2 = false;
                    }
                    f6 = false;
                    f3 = true;
                    f11 = true;
                }
                if (f8 == false && c != "")
                {
                    f8 = true;
                }
            }
        }

        private void buttonExtraction_Click(object sender, EventArgs e)
        {
            if (f10 == true)
            {
                System.Media.SystemSounds.Beep.Play();
            }
            else
            {
                if (double.Parse(textBox1.Text) < 0)
                {
                    textBox1.Text = "无效输入";
                    System.Media.SystemSounds.Beep.Play();
                    f10 = true;
                }
                else
                {
                    if (value2 == null)
                    {
                        f2 = true;
                    }
                    int ml;
                    string mid3 = "sqrt()";
                    string mmid = textBox2.Text;
                    ml = mmid.Length;
                    if (textBox2.Text == "")
                    {
                        mid3 = mid3.Insert(5, (double.Parse(textBox1.Text)).ToString());
                        textBox2.Text += mid3;
                        textBox1.Text = (Math.Pow(double.Parse(textBox1.Text), 0.5)).ToString();
                        
                    }
                    else
                    {
                        if (mmid[ml - 1] != ')')
                        {
                            mid3 = mid3.Insert(5, (double.Parse(textBox1.Text)).ToString());
                            textBox2.Text += mid3;
                            textBox1.Text = (Math.Pow(double.Parse(textBox1.Text), 0.5)).ToString();
                        }
                        else
                        {
                            int kk = 0;
                            for (int i = ml - 1; i >= 0; i--)
                            {
                                if (mmid[i] == '+' || mmid[i] == '-' || mmid[i] == '*' || mmid[i] == '/' || i == 0)
                                {
                                    kk = i;
                                    break;
                                }
                            }
                            if (kk == 0)
                            {
                                textBox2.Text = textBox2.Text.Insert(kk, "sqrt(");
                            }
                            else
                            {
                                textBox2.Text = textBox2.Text.Insert(kk + 1, "sqrt(");
                            }
                            textBox2.Text += ")";
                            textBox1.Text = (Math.Pow(double.Parse(textBox1.Text), 0.5)).ToString();
                        }
                    }
                    if (value1 == null)
                    {
                        f1 = true;
                        f5 = true;
                        mid = "";
                        ml = 0;
                        f2 = false;
                    }
                    f6 = false;
                    f3 = true;
                    if (c != "")
                    {
                        f2 = true;
                    }
                    f11 = true;
                }
                if (c != "" && f8 == false)
                {
                    f8 = true;
                }
            }
        }

        private void buttonAbs_Click(object sender, EventArgs e)
        {
            if (f10 == true)
            { 
                System.Media.SystemSounds.Beep.Play();
            }
            else
            {
                if (textBox1.Text == "正无穷大" || textBox1.Text == "负无穷大")
                {
                    if (textBox1.Text == "正无穷大")
                        textBox1.Text = "负无穷大";
                    else
                        textBox1.Text = "正无穷大";
                }
                else
                {
                    if (textBox1.Text != "非数字")
                    {
                        if (textBox1.Text != "0")
                        {
                            string midd = textBox1.Text;
                            int midll = midd.Length;
                            if (midd[0] != '-')
                            {
                                textBox1.Text = textBox1.Text.PadLeft(midll + 1, '-');
                            }
                            else
                            {
                                textBox1.Text = textBox1.Text.Remove(0, 1);
                            }
                        }
                    }
                }
                f6 = false;
                if (c != "")
                {
                    f2 = true;
                }
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            textBox1.Text = "0";
            textBox2.Text = "";
            value1 = null;
            value2 = null;
            mid = "";
            c = "";
            mid2 = 0;
            l = 0;
            f1 = false;
            f5 = false;
            f2 = false;
            f6 = false;
            f7 = true;
            f10 = false;
        }

        private void buttonCE_Click(object sender, EventArgs e)
        {
            if (f10 == true)
            {
                textBox1.Text = "0";
                textBox2.Text = "";
                value1 = null;
                value2 = null;
                mid = "";
                c = "";
                mid2 = 0;
                l = 0;
                f1 = false;
                f5 = false;
                f2 = false;
                f6 = false;
                f7 = true;
                f10 = false;
 
            }
            f10 = false;
            textBox1.Text = "0";
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            if (f10 == true)
            {
                System.Media.SystemSounds.Beep.Play();
            }
            else
            {
                if (f1 != true && f7 == true&&f11==false)
                {
                    if (textBox1.Text == "0")
                    {
                        System.Media.SystemSounds.Beep.Play();
                    }
                    else
                    {
                        string middd = textBox1.Text;
                        int mml = middd.Length;
                        if (textBox1.Text.Length == 1 || (textBox1.Text.Length == 2 && middd[0] == '-')  || (textBox1.Text.Length == 3 && middd[0] == '-' && middd[2] == '.'))
                        {
                            textBox1.Text = "0";
                            for (int i = 0; i < mml; i++)
                                if (middd[i] == '.')
                                {
                                    f5 = false; 
                                }

                        }
                        if (textBox1.Text != "0")
                        {
                            textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1, 1);
                            for (int i = 0; i < mml; i++)
                                if (middd[i] == '.')
                                {
                                    f5 = false;
                                }
                        }
                    }

                }
                else
                {
                    System.Media.SystemSounds.Beep.Play();
                }
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.D0: button0.PerformClick();
                    break;
                case Keys.D1: btn1.PerformClick();
                    break;
                case Keys.D2: btn2.PerformClick();
                    break;
                case Keys.D3: btn3.PerformClick();
                    break;
                case Keys.D4: btn4.PerformClick();
                    break;
                case Keys.D5: btn5.PerformClick();
                    break;
                case Keys.D6: btn6.PerformClick();
                    break;
                case Keys.D7: btn7.PerformClick();
                    break;
                case Keys.D8: btn8.PerformClick();
                    break;
                case Keys.D9: btn9.PerformClick();
                    break;
                case Keys.Enter: buttonEqual.PerformClick();
                    break;
                case Keys.Oemplus: buttonEqual.PerformClick();
                    break;
                case Keys.OemPeriod: buttonDot.PerformClick();
                    break;
                case Keys.Back: buttonBack.PerformClick();
                    break;
                case Keys.Shift | Keys.Oemplus: Addtion.PerformClick();
                    break;
                case Keys.Shift | Keys.D8: Multiplication.PerformClick();
                    break;
                case Keys.OemMinus: Substration.PerformClick();
                    break;
                case Keys.OemQuestion: Division.PerformClick();
                    break;
                case Keys.F9: buttonAbs.PerformClick();
                    break;
                case Keys.R: buttonInvaerse.PerformClick();
                    break;
                case Keys.Escape: buttonClear.PerformClick();
                    break;
                case Keys.Delete: buttonCE.PerformClick();
                    break;
            }
            return false;
        }

        private void buttonCent_Click(object sender, EventArgs e)
        {
            System.Media.SystemSounds.Beep.Play();
        }
    }
}
