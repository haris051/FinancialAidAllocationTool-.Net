#pragma checksum "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Home/MeritBasedClassStudent.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "159fbdd7dcf0f8ed893b2f51640d9203e8e0d4c8"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_MeritBasedClassStudent), @"mvc.1.0.view", @"/Views/Home/MeritBasedClassStudent.cshtml")]
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
#line 1 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/_ViewImports.cshtml"
using FinancialAidAllocationTool;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/_ViewImports.cshtml"
using FinancialAidAllocationTool.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 1 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Home/MeritBasedClassStudent.cshtml"
using HtmlHelpers.BeginCollectionItemCore;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"159fbdd7dcf0f8ed893b2f51640d9203e8e0d4c8", @"/Views/Home/MeritBasedClassStudent.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"df650b1438e04e1d76f7e1f1a3e3493ff465c799", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_MeritBasedClassStudent : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<FinancialAidAllocationTool.Models.FaatScholarLog>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("AllocationAmount form-control"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("width:100%;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 4 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Home/MeritBasedClassStudent.cshtml"
  
    ViewData["Title"] = "Edit";
    

#line default
#line hidden
#nullable disable
            WriteLiteral("<li class=\"list-group-item border border-dark StdClass\" ");
#nullable restore
#line 8 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Home/MeritBasedClassStudent.cshtml"
                                                         if (Model.Status == "Accept") { 

#line default
#line hidden
#nullable disable
            WriteLiteral(" style=\"background-color:#D9FEE5;\" ");
#nullable restore
#line 8 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Home/MeritBasedClassStudent.cshtml"
                                                                                                                                          } else if (Model.Status == "Reject") { 

#line default
#line hidden
#nullable disable
            WriteLiteral(" style=\"background-color:#E9967A;\" ");
#nullable restore
#line 8 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Home/MeritBasedClassStudent.cshtml"
                                                                                                                                                                                                                                  } else {}

#line default
#line hidden
#nullable disable
            WriteLiteral(">\r\n<div class=\"row\">\r\n");
#nullable restore
#line 10 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Home/MeritBasedClassStudent.cshtml"
     using (Html.BeginHtmlFieldPrefixScope(@ViewData["ContainerPrefix"].ToString()))
    {
    

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Home/MeritBasedClassStudent.cshtml"
     using (Html.BeginCollectionItem("FaatScholarLog"))
    {
   
         

#line default
#line hidden
#nullable disable
            WriteLiteral("         <div style=\"text-align: center;\" class=\"col-6 col-lg-2 col-md-2 AridNoCell\">        \r\n            ");
#nullable restore
#line 17 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Home/MeritBasedClassStudent.cshtml"
       Write(Model.AridNo);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            ");
#nullable restore
#line 18 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Home/MeritBasedClassStudent.cshtml"
       Write(Html.HiddenFor(m => m.AridNo));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n         <div style=\"text-align: center;\" class=\"col-6 col-lg-2 col-md-2 px-0 NameCell\">        \r\n            ");
#nullable restore
#line 21 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Home/MeritBasedClassStudent.cshtml"
       Write(Model.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n");
            WriteLiteral("        <div  style=\"text-align:center;\" class=\"col-6 col-lg-2 col-md-2 Cell2\">\r\n            ");
#nullable restore
#line 26 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Home/MeritBasedClassStudent.cshtml"
       Write(Math.Round(Convert.ToDecimal(Model.Cgpa), 2));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            ");
#nullable restore
#line 27 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Home/MeritBasedClassStudent.cshtml"
       Write(Html.HiddenFor(m => m.Cgpa));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </div>\r\n");
            WriteLiteral("        <div style=\"text-align: center;\" class=\"col-6 col-lg-2 col-md-2 px-0 amount Cell2\">\r\n");
#nullable restore
#line 31 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Home/MeritBasedClassStudent.cshtml"
             if(Model.AllocationAmount == null || @Model.AllocationAmount == 0)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <span class=\"text-danger\">");
#nullable restore
#line 33 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Home/MeritBasedClassStudent.cshtml"
                                     Write(TempData["Error"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n");
#nullable restore
#line 34 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Home/MeritBasedClassStudent.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "159fbdd7dcf0f8ed893b2f51640d9203e8e0d4c89507", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
#nullable restore
#line 35 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Home/MeritBasedClassStudent.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => Model.AllocationAmount);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("</input>  \r\n        </div>\r\n");
            WriteLiteral("        <div class=\"col-6 col-lg-2 col-md-2 Cell2\">\r\n            <button type=\"button\"");
            BeginWriteAttribute("value", "value=\"", 1660, "\"", 1680, 1);
#nullable restore
#line 42 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Home/MeritBasedClassStudent.cshtml"
WriteAttributeValue("", 1667, Model.AridNo, 1667, 13, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"btn btn-success btn-sm Accept\" style=\"width: 100%;\" ");
#nullable restore
#line 42 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Home/MeritBasedClassStudent.cshtml"
                                                                                                                   if (Model.Status == "Accept") { 

#line default
#line hidden
#nullable disable
            WriteLiteral("disabled");
#nullable restore
#line 42 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Home/MeritBasedClassStudent.cshtml"
                                                                                                                                                                        }

#line default
#line hidden
#nullable disable
            WriteLiteral(">");
#nullable restore
#line 42 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Home/MeritBasedClassStudent.cshtml"
                                                                                                                                                                           if(Model.Status=="Accept"){

#line default
#line hidden
#nullable disable
            WriteLiteral("<i class=\"fa fa-ban\" aria-hidden=\"true\"></i>");
#nullable restore
#line 42 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Home/MeritBasedClassStudent.cshtml"
                                                                                                                                                                                                                                                  }

#line default
#line hidden
#nullable disable
            WriteLiteral("<i hidden class=\"fa fa-ban\" aria-hidden=\"true\"></i>Accept</button>\r\n        </div>\r\n        <div class=\"col-6 col-lg-2 col-md-2 Cell2\">\r\n            <button type=\"button\"");
            BeginWriteAttribute("value", " value=\"", 2040, "\"", 2061, 1);
#nullable restore
#line 45 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Home/MeritBasedClassStudent.cshtml"
WriteAttributeValue("", 2048, Model.AridNo, 2048, 13, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"btn btn-danger btn-sm Reject\" style=\"width: 100%;\" ");
#nullable restore
#line 45 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Home/MeritBasedClassStudent.cshtml"
                                                                                                                   if (Model.Status == "Reject") { 

#line default
#line hidden
#nullable disable
            WriteLiteral("disabled");
#nullable restore
#line 45 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Home/MeritBasedClassStudent.cshtml"
                                                                                                                                                                        }

#line default
#line hidden
#nullable disable
            WriteLiteral(">");
#nullable restore
#line 45 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Home/MeritBasedClassStudent.cshtml"
                                                                                                                                                                           if(Model.Status=="Reject"){

#line default
#line hidden
#nullable disable
            WriteLiteral("<i class=\"fa fa-ban\" aria-hidden=\"true\"></i>");
#nullable restore
#line 45 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Home/MeritBasedClassStudent.cshtml"
                                                                                                                                                                                                                                                  }

#line default
#line hidden
#nullable disable
            WriteLiteral("<i hidden class=\"fa fa-ban\" aria-hidden=\"true\"></i> Reject</button> \r\n        </div> \r\n");
#nullable restore
#line 47 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Home/MeritBasedClassStudent.cshtml"


    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 49 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Home/MeritBasedClassStudent.cshtml"
     
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("    \r\n</div>\r\n</li>\r\n\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<FinancialAidAllocationTool.Models.FaatScholarLog> Html { get; private set; }
    }
}
#pragma warning restore 1591
