using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FinancialAidAllocationTool.Models.Policy;
using FinancialAidAllocationTool.Models.Ledger;


namespace FinancialAidAllocationTool.Models.Settings
{
    public partial class Settings
    {
        public FaatPolicy policy {get;set;}
        public DonationLedger DonationLedger {get;set;}

    }

}