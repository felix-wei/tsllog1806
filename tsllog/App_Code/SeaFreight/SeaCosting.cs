﻿using System;

namespace C2
{
    public class SeaCosting
    {

        private int sequenceId;
        private string refNo;
        private string jobType;
        private string jobNo;
        private string vendorId;
        private string chgCode;
        private string chgCodeDes;
        private string remark;
        private decimal saleQty;
        private decimal costQty;
        private decimal salePrice;
        private decimal costPrice;
        private string unit;
        private string saleCurrency;
        private decimal saleExRate;
        private decimal saleDocAmt;
        private decimal saleLocAmt;
        private string costCurrency;
        private decimal costExRate;
        private decimal costDocAmt;
        private decimal costLocAmt;
        private string payInd;
        private string verifryInd;
        private string docNo;
        private string salesman;
        public int SequenceId
        {
            get { return this.sequenceId; }
        }
          public string RefNo
        {
            get { return this.refNo; }
            set { this.refNo = value; }
        }
        public string JobType
        {
            get { return this.jobType; }
            set { this.jobType = value; }
        }
        public string JobNo
        {
            get { return this.jobNo; }
            set { this.jobNo = value; }
        }
      public string RefStatusCode
        {
            get
            {
                string s = "USE";
                string sql = "select StatusCode from SeaExportRef where RefNo='" + this.refNo+"'";
                if(jobType=="SI")
                    sql = "select StatusCode from SeaImportRef where RefNo='" + this.refNo + "'";

                s = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");
                return s;
            }
      }
      public string JobStatusCode
      {
          get
          {
              string s = "USE";
              string sql = "select StatusCode from SeaImport where JobNo='" + this.jobNo + "'";
              if (jobType == "SE")
                  sql = "select StatusCode from SeaExport where JobNo='" + this.jobNo + "'";

              s = SafeValue.SafeString(C2.Manager.ORManager.ExecuteScalar(sql), "USE");
              return s;
          }
      }
        public string ChgCode
        {
            get { return this.chgCode; }
            set { this.chgCode = value; }
        }

        public string ChgCodeDes
        {
            get { return this.chgCodeDes; }
            set { this.chgCodeDes = value; }
        }

        public string Remark
        {
            get { return this.remark; }
            set { this.remark = value; }
        }

        public string VendorId
        {
            get { return this.vendorId; }
            set { this.vendorId = value; }
        }



        public decimal SaleQty
        {
            get { return this.saleQty; }
            set { this.saleQty = value; }
        }
        public decimal CostQty
        {
            get { return this.costQty; }
            set { this.costQty = value; }
        }

        public decimal SalePrice
        {
            get { return this.salePrice; }
            set { this.salePrice = value; }
        }
        public decimal CostPrice
        {
            get { return this.costPrice; }
            set { this.costPrice = value; }
        }

        public string Unit
        {
            get { return this.unit; }
            set { this.unit = value; }
        }

        public string SaleCurrency
        {
            get { return this.saleCurrency; }
            set { this.saleCurrency = value; }
        }

        public decimal SaleExRate
        {
            get { return this.saleExRate; }
            set { this.saleExRate = value; }
        }
        public string CostCurrency
        {
            get { return this.costCurrency; }
            set { this.costCurrency = value; }
        }

        public decimal CostExRate
        {
            get { return this.costExRate; }
            set { this.costExRate = value; }
        }

        public decimal SaleDocAmt
        {
            get { return this.saleDocAmt; }
            set { this.saleDocAmt = value; }
        }
        public decimal SaleLocAmt
        {
            get { return this.saleLocAmt; }
            set { this.saleLocAmt = value; }
        }
        public decimal CostDocAmt
        {
            get { return this.costDocAmt; }
            set { this.costDocAmt = value; }
        }
        public decimal CostLocAmt
        {
            get { return this.costLocAmt; }
            set { this.costLocAmt = value; }
        }
        private string splitType;
        public string SplitType
        {
            get { return this.splitType; }
            set { this.splitType = value; }
        }
        public string PayInd
        {
            get { return this.payInd; }
            set { this.payInd = value; }
        }
        public string VerifryInd
        {
            get { return this.verifryInd; }
            set { this.verifryInd = value; }
        }
        public string DocNo
        {
            get { return this.docNo; }
            set { this.docNo = value; }
        }
        public string Salesman
        {
            get { return this.salesman; }
            set { this.salesman = value; }
        }
    }
}