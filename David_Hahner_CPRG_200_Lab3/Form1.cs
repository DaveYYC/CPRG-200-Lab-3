using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace David_Hahner_CPRG_200_Lab3
{
    /* CPRG 200 Lab 3 Assignment
     * Create a Windows Forms application connected to the nortwnd.mdf database
     * Author: David Hahner
     * Date: September 2020
     */

    public partial class FrmNorthwinds : Form
    {
        public FrmNorthwinds()
        {
            InitializeComponent();
        }

        private void FrmNorthwinds_Load(object sender, EventArgs e)
        {
            try
            {
                // This line of code loads data into the 'northwndDataSet.Categories' table. 
                this.categoriesTableAdapter.Fill(this.northwndDataSet.Categories);
                // This line of code loads data into the 'northwndDataSet.Suppliers' table. 
                this.suppliersTableAdapter.Fill(this.northwndDataSet.Suppliers);
                // This line of code loads data into the 'northwndDataSet.Order_Details' table. 
                this.order_DetailsTableAdapter.Fill(this.northwndDataSet.Order_Details);
                // This line of code loads data into the 'northwndDataSet.Products' table. 
                this.productsTableAdapter.Fill(this.northwndDataSet.Products);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unanticipated error when loading " +
                    ex.Message, ex.GetType().ToString());
            }
        }
        // user clicks on save button
        private void productsBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            try
            { 
                this.Validate();
                this.productsBindingSource.EndEdit();
                this.tableAdapterManager.UpdateAll(this.northwndDataSet);

            }
            catch (DBConcurrencyException)
            {
                MessageBox.Show("Another user updated or deleted data " +
                    "in the mean time. Try again ", "Concurrency Problem");
                // reload
                this.productsTableAdapter.Fill(this.northwndDataSet.Products);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unanticipated error happened when saving data: " +
                    ex.Message, ex.GetType().ToString());
            }
            //catch (SqlException ex)
            //{
            //    MessageBox.Show("Database error # " + ex.Number + ": " + ex.Message, ex.GetType().ToString());
            //}
        }

        private void order_DetailsDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            int row = e.RowIndex + 1;
            int col = e.ColumnIndex + 1;
            MessageBox.Show("Data error in the grid: row " + row + " and column " + col, e.Exception.GetType().ToString());
        }
    }
 }

