using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

//using namespace System.Diagnostics;


namespace Pin_gen
{
    public partial class Form1 : Form
    {
        Random _random = new Random();
        public Form1()
        {
            InitializeComponent();
        }

        private void generateBtn_Click(object sender, EventArgs e)
        {
            string Password = password.Text.Trim();
            if (Password.Length != 8)
            {
                MessageBox.Show("Password has to be 8 digits long", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (!Regex.IsMatch(Password, @"^\d+$"))
            {
                MessageBox.Show("Password must contain only numbers", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            pinOutput.Text = generate(Password);
     
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            password.Text = "";
            pinOutput.Text = "";
        }


        private string generate(string password)
        {
            int sum = 0;
            for (int i = 0; i < 8; i++)
            {
                sum += int.Parse(password.Substring(i, 1));
            }

            //MessageBox.Show(sum.ToString());// to comm
            // * = number, b = lower case letter, B = upper case letter
            //[00 : 10[		starts with a * ,every 3rd character is a b
            if (sum >= 0 && sum < 10)
                return pattern("number", "lowercase", 3);
            //[10 : 20[		starts with a B ,every 4th character is a B
            else if (sum >= 10 && sum < 20)
                return pattern("uppercase", "uppercase", 4);
            //[20 : 30[		starts with a b ,every 3rd character is a *
            else if (sum >= 20 && sum < 30)
                return pattern("lowercase", "number", 3);
            //[30 : 40[		starts with a * ,every 3rd character is a B
            else if (sum >= 30 && sum < 40)
                return pattern("number", "uppercase", 3);
            //[40 : 50[		starts with a b ,every 4th character is a b
            else if (sum >= 40 && sum < 50)
               return pattern("lowercase", "lowercase", 4);
            //[50 : 60[		starts with a b ,every 4th character is a *
            else if (sum >= 50 && sum < 60)
                return pattern("lowercase", "number", 4);
            //[60 : 72]		starts with a B ,every 3rd character is a *
            else
                return pattern("uppercase", "number", 3);
        }
        // 1 CAP ; 0 LOWER ;  -1 Number
        private string pattern(string firstChar, string specialChar, int Position)
        {
            string pin = "";
            switch(firstChar)
            {
                case "lowercase" : pin += GetLowerCaseLetter();break;
                case "uppercase" : pin += GetUpperCaseLetter();break;
                case "number" : pin += GetNumber();break;
            }
            for (int i = 1; i < 16; i++ )
            {
                if ((i + 1) % Position == 0)
                {
                    switch (specialChar)
                    {
                        case "lowercase": pin += GetLowerCaseLetter(); break;
                        case "uppercase": pin += GetUpperCaseLetter(); break;
                        case "number": pin += GetNumber(); break;
                    }
                }
                else
                {
                    pin += GetRandomChar();
                }
                //Console.WriteLine(pin);
            }
            return pin;
        }
            
       public char GetLowerCaseLetter()
        {
            // This method returns a random lowercase letter.
            // ... Between 'a' and 'z' inclusize.
            int num = _random.Next(0, 26); // Zero to 25
            char let = (char)('a' + num);
            //Console.WriteLine("GetLowerCaseLetter : " + let);
            return let;
        }
       public char GetUpperCaseLetter()
       {
           // This method returns a random uppercase letter.
           // ... Between 'A' and 'Z' inclusize.
           int num = _random.Next(0, 26); // Zero to 25
           char let = (char)('A' + num);
           //Console.WriteLine("GetUpperCaseLetter : " + let);
           return let;
       }
       public int GetNumber()
       {
           // This method returns a random lowercase letter.
           // ... Between '0' and '9' inclusize.
           int num = _random.Next(0, 10); // Zero to 25
           //Console.WriteLine("GetNumber : " + num);
           return num;
       }
       public string GetRandomChar()
       {
           string def = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
           StringBuilder ret = new StringBuilder();
           string let = def.Substring(_random.Next(def.Length), 1);
           //Console.WriteLine("GetRandomChar : " + let);
           return let;
           
       }
        private void password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                generateBtn.PerformClick();
            }

        }
        private void pinOutput_TextChanged(object sender, EventArgs e)
        {

        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void password_TextChanged(object sender, EventArgs e)
        {

        }
        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}