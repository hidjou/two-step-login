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

namespace Login_portal
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            username.Text = "";
            password.Text = "";
            pinInput.Text = "";
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            string Username = username.Text.Trim();
            if(Username.Length == 0)
            {
                MessageBox.Show("Please provide a username", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
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
            string Pin = pinInput.Text.Trim();
            if(Pin.Length != 16)
            {
                MessageBox.Show("Invalid pin, please try again", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!CheckValidity(Password, Pin))
            {
                MessageBox.Show("Pin does not match password", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                MessageBox.Show("Login successful, you may proceed good sir", "You are now logged in", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        string Numbers = "0123456789";
        string LowerCaseLetters = "abcdefghijklmnopqrstuvwxyz";
        string UpperCaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        private bool CheckValidity(string Password, string Pin)
        {
            int sum = 0;
            for (int i = 0; i < 8; i++)
            {
                sum += int.Parse(Password.Substring(i, 1));
            }

            //[00 : 10[		starts with a * ,every 3rd character is a b
            if (sum >= 0 && sum < 10)
            {
                if (CheckIfCharacterExsitsInString(Pin.Substring(0, 1), Numbers) == false) return false;
                if (CheckIfEveryNthCharacterHasString(Pin, "lowercase", 3) == false) return false;
            }
            //[10 : 20[		starts with a B ,every 4th character is a B
            else if (sum >= 10 && sum < 20)
            {
                if (CheckIfCharacterExsitsInString(Pin.Substring(0, 1), UpperCaseLetters) == false) return false;
                if (CheckIfEveryNthCharacterHasString(Pin, "uppercase", 4) == false) return false;
            }
            //[20 : 30[		starts with a b ,every 3rd character is a *
            else if (sum >= 20 && sum < 30)
            {
                if (CheckIfCharacterExsitsInString(Pin.Substring(0, 1), LowerCaseLetters) == false) return false;
                if (CheckIfEveryNthCharacterHasString(Pin, "number", 3) == false) return false;
            }
            //[30 : 40[		starts with a * ,every 3rd character is a B
            else if (sum >= 30 && sum < 40)
            {
                if (CheckIfCharacterExsitsInString(Pin.Substring(0, 1), Numbers) == false)  return false;
                if (CheckIfEveryNthCharacterHasString(Pin, "uppercase", 3) == false) return false;
            }
            //[40 : 50[		starts with a b ,every 4th character is a b
            else if (sum >= 40 && sum < 50)
            {
                if (CheckIfCharacterExsitsInString(Pin.Substring(0, 1), LowerCaseLetters) == false) return false;
                if (CheckIfEveryNthCharacterHasString(Pin, "lowercase", 4) == false) return false;
            }
            //[50 : 60[		starts with a b ,every 4th character is a *
            else if (sum >= 50 && sum < 60)
            {
                if (CheckIfCharacterExsitsInString(Pin.Substring(0, 1), LowerCaseLetters) == false) return false;
                if (CheckIfEveryNthCharacterHasString(Pin, "number", 4) == false) return false;
            }
            //[60 : 72]		starts with a B ,every 3rd character is a *
            else
            {
                if (CheckIfCharacterExsitsInString(Pin.Substring(0, 1), UpperCaseLetters) == false) return false;
                if (CheckIfEveryNthCharacterHasString(Pin, "number", 3) == false) return false;
            }

            return true;
        }
        private bool CheckIfCharacterExsitsInString(string Character, string String)
        {
            return String.Contains(Character);
        }
        private bool CheckIfEveryNthCharacterHasString(string Pin, string Case, int Position)
        {
            for (int i = 1; i < 16; i++)
            {
                if((i + 1) % Position == 0)
                {
                    switch (Case)
                    {
                        case "lowercase":
                            if (CheckIfCharacterExsitsInString(Pin.Substring(i, 1), LowerCaseLetters) == false)
                                return false;
                            break;
                        case "uppercase":
                            if (CheckIfCharacterExsitsInString(Pin.Substring(i, 1), UpperCaseLetters) == false)
                                return false;
                            break;
                        case "number":
                            if (CheckIfCharacterExsitsInString(Pin.Substring(i, 1), Numbers) == false)
                                return false;
                            break;
                    }
                }
            }
            return true;
        }

        private void password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                loginBtn.PerformClick();
            }
        }
    }
}
