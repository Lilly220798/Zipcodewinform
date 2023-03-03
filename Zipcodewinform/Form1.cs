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
using System.IO;
using System.Security.AccessControl;
using System.Reflection.Emit;

namespace Zipcodewinform
{
    public partial class Form1 : Form
    {
        private string pattern = @"^\d{5}(?:[-\s]\d{4})?$";
        private Dictionary<string, decimal> customerShoppingValues = new Dictionary<string, decimal>();
        public Form1()
        {
            InitializeComponent();
           
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string zipCode = txtZipCode.Text;
            decimal shoppingValue = decimal.Parse(txtShoppingValue.Text);

            bool isValid = Regex.IsMatch(zipCode, pattern);
            if (isValid)
            {
                if (customerShoppingValues.ContainsKey(zipCode))
                {
                    customerShoppingValues[zipCode] += shoppingValue;
                }
                else
                {
                    customerShoppingValues.Add(zipCode, shoppingValue);
                }

                MessageBox.Show("Shopping value added to customer.");
            }
            else
            {
                MessageBox.Show($"{zipCode} is not a valid zip code. Shopping value not added to customer.");
            }

            txtZipCode.Clear();
            txtShoppingValue.Clear();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string fileName = "supermarket.txt";
           
            string filePath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\", fileName));
            

            MessageBox.Show($"{filePath}");

            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                foreach (KeyValuePair<string, decimal> customer in customerShoppingValues)
                {
                    string customerData = $"{DateTime.Now.ToString()}, {customer.Value:C}, {customer.Key}";

                    writer.WriteLine(customerData);
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
