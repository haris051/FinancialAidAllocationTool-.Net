#pragma checksum "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Application/AddSibJobHolder.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8ad9c341e691f15f5cf7feef41087798e23ae01d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Application_AddSibJobHolder), @"mvc.1.0.view", @"/Views/Application/AddSibJobHolder.cshtml")]
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
#line 2 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Application/AddSibJobHolder.cshtml"
using HtmlHelpers.BeginCollectionItemCore;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8ad9c341e691f15f5cf7feef41087798e23ae01d", @"/Views/Application/AddSibJobHolder.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"df650b1438e04e1d76f7e1f1a3e3493ff465c799", @"/Views/_ViewImports.cshtml")]
    public class Views_Application_AddSibJobHolder : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<FinancialAidAllocationTool.Models.Application.FaatAppSibJobHolder>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-control"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", "file", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("customFile6"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            WriteLiteral("\n");
            WriteLiteral("\n");
#nullable restore
#line 8 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Application/AddSibJobHolder.cshtml"
 using (Html.BeginHtmlFieldPrefixScope(@ViewData["ContainerPrefix"].ToString()))
{
    

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Application/AddSibJobHolder.cshtml"
     using(Html.BeginCollectionItem("FaatAppSibJobHolder"))
{

#line default
#line hidden
#nullable disable
            WriteLiteral("   <section class=\"JobHolders\" style=\"margin-top: 4px;\">\n           <div class=\"row\">\n          <div class=\"col-12\">\n             <label class=\"text-xs font-weight-bold text-success text-uppercase mb-1\">JobHolder ");
#nullable restore
#line 15 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Application/AddSibJobHolder.cshtml"
                                                                                           Write(TempData["JobCount"]);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</label>
          </div>
      </div>
      <div class=""row"" style=""margin-top:2px;"">
            <div class=""col-xl-3 col-lg-6 col-md-6 col-6 MyFont"">
                <label class=""font-weight-bold"" style=""margin-top: 6px;"">Name</label>

            </div>
            <div class=""col-xl-3 col-lg-6 col-md-6 col-6"">
                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "8ad9c341e691f15f5cf7feef41087798e23ae01d6108", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
#nullable restore
#line 24 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Application/AddSibJobHolder.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Name);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"

            </div>
             <div class=""col-xl-3 col-lg-6 col-md-6 col-6 MyFont"">
            <label class=""font-weight-bold"" style=""margin-top: 6px;"">Company Name</label>

             </div>
            <div class=""col-xl-3 col-lg-6 col-md-6 col-6"">
             ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "8ad9c341e691f15f5cf7feef41087798e23ae01d7929", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
#nullable restore
#line 32 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Application/AddSibJobHolder.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Company);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"

            </div>


    </div>
      <div class=""row"" style=""margin-top:2px;"">
            <div class=""col-xl-3 col-lg-6 col-md-6 col-6 MyFont"">
                    <label class=""font-weight-bold"" style=""margin-top: 6px;"">Designation</label>

            </div>
            <div class=""col-xl-3 col-lg-6 col-md-6 col-6"">
                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "8ad9c341e691f15f5cf7feef41087798e23ae01d9830", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
#nullable restore
#line 44 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Application/AddSibJobHolder.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Designation);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"

            </div>
             <div class=""col-xl-3 col-lg-6 col-md-6 col-6 MyFont"">
                    <label class=""font-weight-bold"" style=""margin-top: 6px;"">Monhly Income</label>

             </div>
            <div class=""col-xl-3 col-lg-6 col-md-6 col-6"">
                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "8ad9c341e691f15f5cf7feef41087798e23ae01d11674", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
#nullable restore
#line 52 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Application/AddSibJobHolder.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.MonthlyIncome);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"

            </div>
    </div>
    <div class=""row"" style=""margin-top:2px;"">
              <div class=""col-xl-3 col-lg-6 col-md-6 col-6 MyFont"">
                           <label class=""font-weight-bold"" style=""margin-top: 6px;"">Salary Slip</label>
              </div>
              <div class=""col-xl-3 col-lg-6 col-md-6 col-6 MyFont"">                   
                                     
                                      <a class=""AttachFile"" ><i class=""fa fa-paperclip"" aria-hidden=""true""></i></a><img class=""ViewImage"" hidden");
            BeginWriteAttribute("alt", " alt=\"", 2472, "\"", 2478, 0);
            EndWriteAttribute();
            BeginWriteAttribute("src", " src=\"", 2479, "\"", 2485, 0);
            EndWriteAttribute();
            WriteLiteral(" style=\"width: 50px;height:50px;margin-left:4px;border: 3px solid black\">\n\n                                      ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "8ad9c341e691f15f5cf7feef41087798e23ae01d14143", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
#nullable restore
#line 64 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Application/AddSibJobHolder.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.ContractFile);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __tagHelperExecutionContext.AddHtmlAttribute("hidden", Html.Raw(__tagHelperStringValueBuffer), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.Minimized);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                                     
                                    
                            
              </div>
              <div class=""col-xl-3 col-lg-6 col-md-6 col-6 MyFont"">
                         <label class=""font-weight-bold"" style=""margin-top: 6px;"">Discard</label>
              </div>
              <div class=""col-xl-3 col-lg-6 col-md-6 col-6"">
                   <a href=""#"" class=""btn btn-danger delete-JobHolder"">
                    <i class=""fa fa-trash"" aria-hidden=""true""></i>
                </a>
       </div>
    </div>
     
 </section>  
");
#nullable restore
#line 80 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Application/AddSibJobHolder.cshtml"
    
   
 





}

#line default
#line hidden
#nullable disable
#nullable restore
#line 88 "/home/muhammad/Projects/FinancialAidAllocationTool (4-sept-2020)   /FinancialAidAllocationTool/Views/Application/AddSibJobHolder.cshtml"
 
}

#line default
#line hidden
#nullable disable
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<FinancialAidAllocationTool.Models.Application.FaatAppSibJobHolder> Html { get; private set; }
    }
}
#pragma warning restore 1591
