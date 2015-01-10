// ***********************************************************
// 医嘱列表显示窗口.
// Creator:YangMingkun  Date:2009-11-7
// Copyright:supconhealth
// ***********************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using Heren.Common.Controls;
using Heren.Common.Libraries;
using Heren.Common.DockSuite;
using Heren.Common.Report;
using Heren.Common.VectorEditor;
using Heren.Platform.Common;
using Heren.MedQC.Data;
using Heren.MedQC.DAL;
using Heren.MedQC.Common;
using Heren.Platform.Common.Command;
using Heren.MedQC.Orders.Utilities;
using Heren.MedQC.Orders.Forms;

namespace Heren.MedQC.Orders
{
    public partial class OrdersListForm : AbstractContent
    {
        public OrdersListForm()

        public override void RefreshView()
        private void ShowStatusMessage(string szMessage)

        public override void OnActiveContentChanged()
        private List<MedOrderInfo> lstOrderInfo = null;
        private void LoadOrderInfoList(int nOrderFlag)
        private ReportExplorerForm GetReportExplorerForm()
        /// <summary>
        /// 加载打印模板
        /// </summary>
        private byte[] GetReportFileData(string szReportName)
        private bool GetGlobalDataHandler(string name, ref object value)
        private void btnPrint_Click(object sender, EventArgs e)

        private void btnExport_Click(object sender, EventArgs e)
        private void ckbSwitch_CheckedChanged(object sender, EventArgs e)
        private void ReportExplorerForm_QueryContext(object sender, Heren.Common.Report.QueryContextEventArgs e)
        private void ReportExplorerForm_NotifyNextReport(object sender, Heren.Common.Report.NotifyNextReportEventArgs e)
        private void btnSearch_Click(object sender, EventArgs e)
        private void CalSearchRule()
        private bool HasOrder(string szOrderText)
        private short HasDocContent(string szDocContent, string szDocTypeID, ref MedDocList medDoclist)
        public short FullTextSearching(string szSearchKeyWord, string szDocTypeID, string szPatientID, string szVisitID, ref MDSDBLib.MedDocList lstDocInfo)
        private void btnOrdersCheck_Click(object sender, EventArgs e)
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        private void mnuCopyCheckBasis_Click(object sender, EventArgs e)
        private void tsbSearch_Click(object sender, EventArgs e)
    }
}