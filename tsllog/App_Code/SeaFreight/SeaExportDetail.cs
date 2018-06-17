﻿using System;

namespace C2
{
    public class SeaExportDetail
    {
        private int sequenceId;
        private string refNo;
        private string jobNo;
        private string chgCode;
        private string description;
        private decimal qty;
        private decimal price;
        private decimal amount;
        private string currency;
        private bool printInd;
        private string wtEntryInd;
        private string printTerm;

        public int SequenceId
        {
            get { return this.sequenceId; }
        }

        public string RefNo
        {
            get { return this.refNo; }
            set { this.refNo = value; }
        }
        public string CloseInd
        {
            get
            {
                string s = "N";
                string sql = "select RefCloseInd from SeaExportRef where RefNo='" + this.refNo + "'";
                s = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "N");
                return s;
            }
        }

        public string JobNo
        {
            get { return this.jobNo; }
            set { this.jobNo = value; }
        }

        public string ChgCode
        {
            get { return this.chgCode; }
            set { this.chgCode = value; }
        }

        public string Description
        {
            get { return this.description; }
            set { this.description = value; }
        }

        public decimal Qty
        {
            get { return this.qty; }
            set { this.qty = value; }
        }

        public decimal Price
        {
            get { return this.price; }
            set { this.price = value; }
        }

        public decimal Amount
        {
            get { return this.amount; }
            set { this.amount = value; }
        }

        public string Currency
        {
            get { return this.currency; }
            set { this.currency = value; }
        }

        public bool PrintInd
        {
            get { return this.printInd; }
            set { this.printInd = value; }
        }

        public string WtEntryInd
        {
            get { return this.wtEntryInd; }
            set { this.wtEntryInd = value; }
        }
        public string PrintTerm
        {
            get { return this.printTerm; }
            set { this.printTerm = value; }
        }
        public string RefStatusCode
        {
            get
            {
                string s = "USE";
                string sql = "select StatusCode from SeaExportRef where RefNo='" + this.refNo + "'";
                s = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");
                return s;
            }
        }
        public string JobStatusCode
        {
            get
            {
                string s = "USE";
                string sql = "select StatusCode from SeaExport where JobNo='" + this.jobNo + "'";

                s = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");
                return s;
            }
        }
    }
}