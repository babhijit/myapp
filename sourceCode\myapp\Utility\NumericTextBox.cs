using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace Utility
{
    public class NumericTextBox : TextBox
    {
        bool allowSpace = false;

        // Restricts the entry of characters to digits (including hex), the negative sign,
        // the decimal point, and editing keystrokes (backspace).
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            // ugly solution to handle backspace
            Boolean backSpaceHandling = false;

            base.OnKeyPress(e);

            NumberFormatInfo numberFormatInfo = System.Globalization.CultureInfo.CurrentCulture.NumberFormat;
            string decimalSeparator = numberFormatInfo.NumberDecimalSeparator;
            string groupSeparator = numberFormatInfo.NumberGroupSeparator;
            string negativeSign = numberFormatInfo.NegativeSign;

            string keyInput = e.KeyChar.ToString();

            if (Char.IsDigit(e.KeyChar))
            {
                // Digits are OK
            }
            else if (keyInput.Equals(decimalSeparator) || keyInput.Equals(groupSeparator) ||
             keyInput.Equals(negativeSign))
            {
                // Decimal separator is OK
            }
            else if (e.KeyChar == '\b')
            {
                // Backspace key is OK
                backSpaceHandling = true;
            }
            //    else if ((ModifierKeys & (Keys.Control | Keys.Alt)) != 0)
            //    {
            //     // Let the edit control handle control and alt key combinations
            //    }
            else if (this.allowSpace && e.KeyChar == ' ')
            {

            }
            else
            {
                // Swallow this invalid key and beep
                e.Handled = true;
                //    MessageBeep();
            }

            // ugly handling for error condition
            if((e.Handled == false) && (backSpaceHandling == false))
            {
                String text = this.Text + keyInput;
                Boolean handled = false;

                if(TextIsInt)
                {
                    Int32 intTemp;
                    handled = !Int32.TryParse(text, out intTemp);
                }
                else
                {
                    Decimal decTemp;
                    handled = !Decimal.TryParse(text, out decTemp);
                }

                e.Handled = handled;
            }
        }

        public int IntValue
        {
            get
            {
                if (String.IsNullOrEmpty(this.Text))
                    return 0;
                return Int32.Parse(this.Text);
            }
        }

        public decimal DecimalValue
        {
            get
            {
                if (String.IsNullOrEmpty(this.Text))
                    return 0;

                return Decimal.Parse(this.Text);
            }
        }


        public Double DoubleValue
        {
            get
            {
                if (String.IsNullOrEmpty(this.Text))
                    return 0;

                return Double.Parse(this.Text);
            }
        }

        public bool AllowSpace
        {
            set
            {
                this.allowSpace = value;
            }

            get
            {
                return this.allowSpace;
            }
        }

        private Boolean m_textIsInt = false;
        public Boolean TextIsInt
        {
            get { return m_textIsInt; }
            set { m_textIsInt = value; }
        }
    }
}
