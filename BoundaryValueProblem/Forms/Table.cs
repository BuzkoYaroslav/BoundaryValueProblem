using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using library;

namespace BoundaryValueProblem
{
    public partial class Table : Form
    {
        const string approximateY = "Approximate Y(X)";
        const string accurateY = "Accurate Y(X)";
        const string xString = "X";

        const int digitsAfterComma = 3;

        public Table()
        {
            InitializeComponent();
        }
        public Table(MathFunction accurateFunc, KeyValuePair<double, double>[] methodWorkResult): this()
        {
            InitializeDataGridView(accurateFunc, methodWorkResult);
        }

        private void InitializeDataGridView(MathFunction func, KeyValuePair<double, double>[] result)
        {
            DataGridViewColumn xCol = new DataGridViewColumn(),
                               approximationCol = new DataGridViewColumn(),
                               accurateCol = new DataGridViewColumn();

            ConfigureColumn(ref xCol, xString);
            ConfigureColumn(ref approximationCol, approximateY);
            ConfigureColumn(ref accurateCol, accurateY);

            dataGridView1.Columns.Add(xCol);
            dataGridView1.Columns.Add(approximationCol);
            dataGridView1.Columns.Add(accurateCol);

            for (int i = 0; i < result.Length; i++)
            {
                var newRow = new DataGridViewRow();

                newRow.HeaderCell.Value = i.ToString();
                for (int j = 0; j < 3; j++)
                    newRow.Cells.Add(new DataGridViewTextBoxCell());

                newRow.Cells[0].Value = Math.Round(result[i].Key, digitsAfterComma);
                newRow.Cells[1].Value = Math.Round(result[i].Value, digitsAfterComma);
                newRow.Cells[2].Value = Math.Round(func.Calculate(result[i].Key), digitsAfterComma);

                dataGridView1.Rows.Add(newRow);
            }
        }
        private void ConfigureColumn(ref DataGridViewColumn col, string headerText)
        {
            col.HeaderCell.Value = headerText;

            col.CellTemplate = new DataGridViewTextBoxCell();
        }
    }
}
