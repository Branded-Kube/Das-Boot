using DataBros.MVP.UserInterface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DataBros
{

        public partial class Form1 : Form, IAssesmentRecordView
        {
            public Form1()
            {
                InitializeComponent();
            }

            public event Action<int> ActualValueChanged;

            public void SetVarianceCategoryColor(Color color)
            {
                labelVariance.ForeColor = color;
            }

            private void textboxActualValue_TextChanged(object sender, EventArgs e)
            {
                int value;
                int.TryParse(textboxActualValue.Text, out value);

                ActualValueChanged?.Invoke(value);
            }

            /// <summary>
            /// Subscribes to model events
            /// </summary>
            public void HandleValueUpdated(int variance)
            {
                labelVariance.Text = variance.ToString();
            }
        }
    }

