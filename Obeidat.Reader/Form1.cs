using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Obeidat.Reader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        #region Properties

        bool IsSqlReader = false;
        DataTable LogDataTable;
        SqlConnection Connection;
        #endregion

        #region EventHandlers
        private void button1_Click(object sender, EventArgs e)
        {
            if (IsSqlReader)
                return;

            var dialogRslt = fileSelected.ShowDialog();

            if (dialogRslt == DialogResult.OK)
            {
                var filePath = fileSelected.FileName;

                if (File.Exists(filePath))
                {
                    txtFilePath.Text = filePath;
                }
                else
                {
                    MessageBox.Show("File does not exist, or have been deleted", "File Doesn't Exist", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    IsSqlReader = false;
                    break;
                case 1:
                    IsSqlReader = true;
                    break;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            datePickerBegin.Value = datePickerEnd.Value = DateTime.Now;
        }

        private string txtConnStringHolder = "";
        private void txtConnectionString_Enter(object sender, EventArgs e)
        {
            txtConnStringHolder = txtConnectionString.Text;
            txtConnectionString.Text = string.Empty;
        }
        private void txtConnectionString_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtConnectionString.Text) || string.IsNullOrWhiteSpace(txtConnectionString.Text))
            {
                txtConnectionString.Text = string.IsNullOrEmpty(txtConnStringHolder) ? "Insert log sql connection string..." : txtConnStringHolder;
            }
        }

        private string txtFilePathHolder = "";
        private void txtFilePath_Enter(object sender, EventArgs e)
        {
            txtFilePathHolder = txtFilePath.Text;
            txtFilePath.Text = string.Empty;
        }
        private void txtFilePath_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilePath.Text) || string.IsNullOrWhiteSpace(txtFilePath.Text))
            {
                txtFilePath.Text = string.IsNullOrEmpty(txtFilePathHolder) ? "Or insert file path here..." : txtFilePathHolder;
            }
        }

        private string txtProjectNameHolder = "";
        private void txtProjectName_Enter(object sender, EventArgs e)
        {
            txtProjectNameHolder = txtProjectName.Text;
            txtProjectName.Text = string.Empty;
        }
        private void txtProjectName_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtProjectName.Text) || string.IsNullOrWhiteSpace(txtProjectName.Text))
            {
                txtProjectName.Text = string.IsNullOrEmpty(txtProjectNameHolder) ? "Project Name" : txtProjectNameHolder;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilePath.Text) || string.IsNullOrWhiteSpace(txtFilePath.Text)
                || !File.Exists(txtFilePath.Text))
            {
                MessageBox.Show("Please select a valid log file", "File not selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            txtInfo.Invoke(new MethodInvoker(() =>
            {
                txtInfo.AppendText(string.Format("Initializing...{0}", Environment.NewLine));
            }));

            string fileName = Path.GetFileName(txtFilePath.Text);
            string[] fileNameParts = fileName.Split();
            string projectName = fileNameParts.Length > 0 ? fileNameParts[0] : string.Empty;
            string fileDate = fileNameParts.Length > 1 ? fileNameParts[1] : string.Empty;
            string logLevel = fileNameParts.Length > 3 ? fileNameParts[3].Split('.')[0] : string.Empty;
            string fileExt = Path.GetExtension(txtFilePath.Text).TrimStart('.');



            txtInfo.Invoke(new MethodInvoker(() =>
            {
                txtInfo.AppendText(string.Format("{0} : {1}{2}", "Project Name", projectName, Environment.NewLine));
                txtInfo.AppendText(string.Format("{0} : {1}{2}", "Log Level", logLevel, Environment.NewLine));
                txtInfo.AppendText(string.Format("{0} : {1}{2}", "File Date", fileDate, Environment.NewLine));
                txtInfo.AppendText(string.Format("{0} : {1}{2}", "File Type", fileExt, Environment.NewLine));
            }));

            dataGrid.DataSource = null;
            LogDataTable = new DataTable();


            if (fileExt == "xml")
            {
                dataGrid.Invoke(new MethodInvoker(async () =>
                {
                    Dictionary<string, List<string>> XmlValues = new Dictionary<string, List<string>>();

                    try
                    {
                        XmlReaderSettings settings = new XmlReaderSettings();
                        settings.Async = true;

                        using (XmlReader reader = XmlReader.Create(new StreamReader(txtFilePath.Text, Encoding.UTF8), settings))
                        {
                            while (await reader.ReadAsync())
                            {
                                if (!LogDataTable.Columns.Contains(reader.Name))
                                    LogDataTable.Columns.Add(reader.Name, typeof(string));
                                if (!XmlValues.ContainsKey(reader.Name))
                                    XmlValues.Add(reader.Name, new List<string>());

                                XmlValues[reader.Name].Add(reader.Value);
                            }
                        }

                        //foreach () how should it be read?
                        //{
                        //    LogDataTable.Rows.Add()
                        //}

                        dataGrid.DataSource = LogDataTable;
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show(string.Format("IO Error: {0}", ex.Message), "Error Reading File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }));


                return;

            }

            dataGrid.Invoke(new MethodInvoker(() =>
            {
                try
                {
                    FillDataGridView();
                }
                catch (IOException ex)
                {
                    MessageBox.Show(string.Format("IO Error: {0}", ex.Message), "Error Reading File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(string.Format("Error: {0}", ex.Message), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }));


        }
        private void btnApply_Click(object sender, EventArgs e)
        {
            if (dataGrid.Rows == null || dataGrid.Rows.Count == 0)
                return;

            string filter = "";
            bool first = true;
            bool dateAdded = false;
            if (!IsEmptyFilterFields())
            {
                foreach (Control child in Filters.Controls)
                {
                    if (child is TextBox)
                    {
                        if (!string.IsNullOrEmpty(child.Text) && !string.IsNullOrWhiteSpace(child.Text))
                        {
                            bool isInt = false;
                            if (IsSqlReader && combLevel.Text.ToLower() == "exception" && child.Name.ToLower() == "linenumber")
                                isInt = true;

                            if (isInt)
                            {
                                if (first)
                                {
                                    filter += string.Format(" [{0}] = '{1}' ", child.Name, child.Text);
                                    first = false;
                                }
                                else
                                {
                                    filter += string.Format(" AND [{0}] = '{1}' ", child.Name, child.Text);
                                }
                                continue;
                            }

                            if (first)
                            {
                                filter += string.Format(" [{0}] LIKE '%{1}%' ", child.Name, child.Text);
                                first = false;
                            }
                            else
                            {
                                filter += string.Format(" AND [{0}] LIKE '%{1}%' ", child.Name, child.Text);
                            }
                        }
                    }


                    if (child is DateTimePicker)
                    {
                        if (!dateAdded)
                        {
                            if ((child.Name == "txtBeginTime" && child.Text != "00:00:00") || (child.Name == "txEndTime" && child.Text != "23:59:59"))
                            {
                                //DateTime begin = new DateTime(DateTime.MinValue.Year, 1, 1, 0, 0, 0);
                                //DateTime end = new DateTime(DateTime.MaxValue.Year, 1, 1, 0, 0, 0);
                                //TimeSpan beginSpan = new TimeSpan(txtBeginTime.Value.Hour, txtBeginTime.Value.Minute, txtBeginTime.Value.Second);
                                //TimeSpan EndSpan = new TimeSpan(txEndTime.Value.Hour, txEndTime.Value.Minute, txEndTime.Value.Second);
                                //begin += beginSpan;
                                //end += EndSpan;

                                if (first)
                                {
                                    filter += string.Format(" {0} >= '{1}' AND {0} <= '{2}' ",
                                        IsSqlReader ? "[Time]" : "[StringTime]",
                                         txtBeginTime.Text,
                                         txEndTime.Text);
                                    first = false;
                                }
                                else
                                {
                                    filter += string.Format(" AND {0} >= '{1}' AND {0} <= '{2}' ",
                                        IsSqlReader ? "[Time]" : "[StringTime]",
                                        txtBeginTime.Text,
                                        txEndTime.Text);
                                }
                            }
                        }
                    }
                }

                LogDataTable.DefaultView.RowFilter = filter;
            }
            else
            {
                LogDataTable.DefaultView.RowFilter = null;
            }
        }
        private void btnLoadData_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtConnectionString.Text) || string.IsNullOrWhiteSpace(txtConnectionString.Text) ||
                txtConnectionString.Text.Equals("Insert log sql connection string..."))
            {
                MessageBox.Show("Please insert a valid connection string", "Invalid Connection string", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            if (combLevel.SelectedIndex < 0)
            {
                MessageBox.Show("Please select log level", "Log Level Not Selected", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(txtProjectName.Text) || string.IsNullOrWhiteSpace(txtProjectName.Text))
            {
                MessageBox.Show("Please enter project name", "project name empty", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }

            dataGrid.BeginInvoke(new MethodInvoker(async () =>
            {
                if (!await IsConnectionReady())
                    return;

                dataGrid.DataSource = null;
                LogDataTable = new DataTable();

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Connection;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = string.Format(" SELECT * FROM [{0}_{1}] WHERE CAST([Time] AS DATE) BETWEEN '{2}' AND '{3}' ",
                    txtProjectName.Text, combLevel.Text.ToLower().Equals("information") ? "Info" : combLevel.Text,
                    datePickerBegin.Value.ToShortDateString(), datePickerEnd.Value.ToShortDateString());

                using (SqlDataAdapter SqlAdapter = new SqlDataAdapter(cmd))
                {
                    SqlAdapter.Fill(LogDataTable);
                }

                AdjustSearchFilters(LogDataTable);
                dataGrid.DataSource = LogDataTable;

            }));
        }

        #endregion

        #region Methods

        /// <summary>
        /// Fills the datagridview control with rows from the log file.
        /// </summary>
        private async void FillDataGridView()
        {
            string line = "";
            using (StreamReader sr = new StreamReader(txtFilePath.Text))
            {
                line = sr.ReadLine();
                if (!ExtractDataGridViewColumns(line))
                {
                    MessageBox.Show("The log file is empty!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                AdjustSearchFilters(LogDataTable);

                int count = 1;
                while ((line = await sr.ReadLineAsync()) != null)
                {
                    count++;
                    AddLineToDataGridView(line, count);
                }

                txtInfo.Invoke(new MethodInvoker(() =>
                {
                    txtInfo.AppendText(string.Format("{0} : {1}{2}", "Total Document Count", count, Environment.NewLine));
                }));

                dataGrid.DataSource = LogDataTable;
            }
        }

        /// <summary>
        /// Adjusts Search filters to match what exists in the log file.
        /// </summary>
        /// <param name="logDataTable">The datatable which contains the log column headers</param>
        private void AdjustSearchFilters(DataTable logDataTable)
        {
            foreach (Control child in Filters.Controls)
            {
                if (child is TextBox)
                {
                    child.Text = string.Empty;
                }

                if (child is DateTimePicker)
                {
                    ((DateTimePicker)child).Value = child.Name == "txEndTime" ? new DateTime(DateTime.Now.Year, DateTime.Now.Month,
                        DateTime.Now.Day, 23, 59, 59, 0) : new DateTime(DateTime.Now.Year, DateTime.Now.Month,
                        DateTime.Now.Day, 0, 0, 0, 0);
                }

                if (!(child is DateTimePicker) && !(child is Label) && !(child is Button))
                {
                    child.Enabled = false;
                }
            }

            if (logDataTable.Columns.Count == 0)
                return;

            var columns = logDataTable.Columns.Cast<DataColumn>().Select(x => x.ColumnName);

            foreach (var column in columns)
            {
                foreach (Control child in Filters.Controls)
                {
                    if (child.Name == column)
                    {
                        child.Enabled = true;
                    }
                }
            }
        }

        /// <summary>
        /// Sets the columns for datagridview control from first line text in file and adds the row.
        /// </summary>
        /// <param name="line">First line of text in the log file</param>
        private bool ExtractDataGridViewColumns(string line)
        {
            if (string.IsNullOrEmpty(line) || string.IsNullOrWhiteSpace(line))
                return false;

            string[] lineParts = line.Split(new string[] { "|=>" }, StringSplitOptions.None);
            List<string> row = new List<string>();

            // DataGridViewTextBoxColumn col;
            LogDataTable.Columns.Add("Number", typeof(int));
            row.Add("1");
            foreach (var text in lineParts)
            {
                //col = new DataGridViewTextBoxColumn();
                //col.ReadOnly = true;
                //col.Resizable = DataGridViewTriState.False;
                //col.SortMode = DataGridViewColumnSortMode.Automatic;
                //col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //col.Width = 100;
                var trimmedText = text.Trim();
                var trimmedTextParts = trimmedText.Split(':');
                if (trimmedTextParts.Length > 0)
                {
                    switch (trimmedTextParts[0].Trim())
                    {
                        case "StringTime":
                            var time = string.Join(":", trimmedTextParts.Skip(1).ToArray());
                            // col.Name = trimmedTextParts[0].Trim();
                            //col.HeaderText = trimmedTextParts[0].Trim();
                            row.Add(time.Trim());
                            break;
                        case "Caption":
                        case "Message":
                        case "FileName":
                        case "Method":
                        case "LineNumber":
                        case "IP":
                        case "Username":
                        case "Client":
                        case "DeviceName":
                        case "URL":
                            //col.Name = trimmedTextParts[0].Trim();
                            //col.HeaderText = trimmedTextParts[0].Trim();
                            row.Add(trimmedTextParts[1].Trim());
                            break;
                        case "Data": break;
                        default: break;
                    }
                }
                //adding column to dataGridView
                LogDataTable.Columns.Add(trimmedTextParts[0].Trim(), typeof(string));
            }

            LogDataTable.Rows.Add(row.ToArray());

            return true;
        }

        /// <summary>
        /// Adds a line to datagridview control, be aware this will throw an exception if cells count after line split didn't align with columns in the control.
        /// </summary>
        /// <param name="line">Text line from log file to be split and inserted into datagridview control</param>
        private void AddLineToDataGridView(string line, int count = 2)
        {

            if (string.IsNullOrEmpty(line) || string.IsNullOrWhiteSpace(line))
                return;

            string[] lineParts = line.Split(new string[] { "|=>" }, StringSplitOptions.None);
            List<string> row = new List<string>();
            row.Add(Convert.ToString(count));
            foreach (var text in lineParts)
            {

                var trimmedText = text.Trim();
                var trimmedTextParts = trimmedText.Split(':');
                if (trimmedTextParts.Length > 0)
                {
                    if (trimmedTextParts[0].Trim().Equals("StringTime"))
                    {
                        var time = string.Join(":", trimmedTextParts.Skip(1).ToArray());
                        row.Add(time.Trim());
                    }
                    else
                    {
                        row.Add(trimmedTextParts[1].Trim());
                    }
                }
            }

            LogDataTable.Rows.Add(row.ToArray());
        }

        /// <summary>
        /// Check if all filtering fields are empty.
        /// </summary>
        /// <returns>True if all filtering fields are empty</returns>
        private bool IsEmptyFilterFields()
        {
            foreach (Control child in Filters.Controls)
            {
                if (child is TextBox)
                {
                    if (!string.IsNullOrEmpty(child.Text) && !string.IsNullOrWhiteSpace(child.Text))
                    {
                        return false;
                    }
                }

                if (child is DateTimePicker)
                {
                    if (child.Name == "txtBeginTime")
                    {
                        if (child.Text != "00:00:00")
                            return false;
                    }

                    if (child.Name == "txEndTime")
                    {
                        if (child.Text != "23:59:59")
                            return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Checks if connection is initialized and if connection is open, performs both operations if not.
        /// </summary>
        private async Task<bool> IsConnectionReady()
        {
            try
            {
                if (Connection == null)
                    Connection = new SqlConnection(txtConnectionString.Text);

                if (Connection.State == ConnectionState.Closed)
                    await Connection.OpenAsync();

                return true;
            }
            catch (SqlException ex)
            {
                MessageBox.Show(string.Format("Error : {0}", ex.Message), "Error connecting to database",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        #endregion

    }
}
