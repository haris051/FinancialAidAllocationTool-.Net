#pragma checksum "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Ledger\AddDonationLedger copy.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4289a731f375f641c443fa04028c987bd1cd2d5b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Ledger_AddDonationLedger_copy), @"mvc.1.0.view", @"/Views/Ledger/AddDonationLedger copy.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\_ViewImports.cshtml"
using FinancialAidAllocationTool;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\_ViewImports.cshtml"
using FinancialAidAllocationTool.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4289a731f375f641c443fa04028c987bd1cd2d5b", @"/Views/Ledger/AddDonationLedger copy.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"df650b1438e04e1d76f7e1f1a3e3493ff465c799", @"/Views/_ViewImports.cshtml")]
    public class Views_Ledger_AddDonationLedger_copy : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<FinancialAidAllocationTool.Models.Ledger.DonationLedger>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Ledger\AddDonationLedger copy.cshtml"
  
    Layout=null;

#line default
#line hidden
#nullable disable
            WriteLiteral("<tr>\r\n    <td>\r\n        ");
#nullable restore
#line 7 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Ledger\AddDonationLedger copy.cshtml"
   Write(Model.TransactionId);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </td> \r\n      <td>\r\n        ");
#nullable restore
#line 10 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Ledger\AddDonationLedger copy.cshtml"
   Write(Html.DisplayFor(x => x.TransactionDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </td>\r\n      <td>\r\n        ");
#nullable restore
#line 13 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Ledger\AddDonationLedger copy.cshtml"
   Write(Model.Credit);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </td>\r\n      <td>\r\n        ");
#nullable restore
#line 16 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Ledger\AddDonationLedger copy.cshtml"
   Write(Model.Debit);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </td>\r\n      <td>\r\n        ");
#nullable restore
#line 19 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Ledger\AddDonationLedger copy.cshtml"
   Write(Model.Memo);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </td>\r\n    \r\n</tr>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<FinancialAidAllocationTool.Models.Ledger.DonationLedger> Html { get; private set; }
    }
}
#pragma warning restore 1591
