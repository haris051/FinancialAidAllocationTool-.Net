#pragma checksum "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Home\MeritList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "edf9db4676a3364af44b9c968a29309975fdb220"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_MeritList), @"mvc.1.0.view", @"/Views/Home/MeritList.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
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
#nullable restore
#line 2 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Home\MeritList.cshtml"
using System.Collections.Generic;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"edf9db4676a3364af44b9c968a29309975fdb220", @"/Views/Home/MeritList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"df650b1438e04e1d76f7e1f1a3e3493ff465c799", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_MeritList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<FinancialAidAllocationTool.Models.FaatClassDefinition>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Home\MeritList.cshtml"
  
Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div style=\"margin-top: 20px;\">\r\n      \r\n<section class=\"Boxes\"> \r\n\r\n          \r\n");
#nullable restore
#line 12 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Home\MeritList.cshtml"
  
  using(Html.BeginForm("SelectAll", "Home",FormMethod.Post))
  {

#line default
#line hidden
#nullable disable
            WriteLiteral("      <div class=\"row\">\r\n        <div class=\"col-sm\">\r\n");
#nullable restore
#line 17 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Home\MeritList.cshtml"
           if(TempData["IsAcceptAllEnabled"].ToString() == "1")
          {

#line default
#line hidden
#nullable disable
            WriteLiteral("              <button  name=\"AcceptAll\" class=\"btn btn-primary AcceptAll\"  value=\"Accept\" onclick=\"SubmitForms()\">Accept All</button>\r\n");
#nullable restore
#line 20 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Home\MeritList.cshtml"
          }
          else
          {

#line default
#line hidden
#nullable disable
            WriteLiteral("              <button  name=\"AcceptAll\" disabled class=\"btn btn-primary AcceptAll\"  value=\"Accept\" onclick=\"SubmitForms()\">Accept All</button>\r\n");
#nullable restore
#line 24 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Home\MeritList.cshtml"
          }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\r\n        <div class=\"col-sm\">\r\n");
#nullable restore
#line 27 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Home\MeritList.cshtml"
           if(TempData["IsRejectAllEnabled"].ToString() == "1")
          {

#line default
#line hidden
#nullable disable
            WriteLiteral("              <button  name=\"RejectAll\" class=\"btn btn-primary RejectAll\"  value=\"Reject\" onclick=\"SubmitForms()\">Reject All</button>\r\n");
#nullable restore
#line 30 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Home\MeritList.cshtml"
          }
          else
          {

#line default
#line hidden
#nullable disable
            WriteLiteral("              <button  name=\"RejectAll\" disabled class=\"btn btn-primary RejectAll\"  value=\"Reject\" onclick=\"SubmitForms()\">Reject All</button>\r\n");
#nullable restore
#line 34 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Home\MeritList.cshtml"
          }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\r\n    </div>\r\n");
#nullable restore
#line 37 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Home\MeritList.cshtml"
  }
                
  var count =0 ;
  
  

#line default
#line hidden
#nullable disable
#nullable restore
#line 41 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Home\MeritList.cshtml"
   foreach(var obj in Model)
  {  



#line default
#line hidden
#nullable disable
            WriteLiteral("      <div class=\"table-responsive\">\r\n          <table class=\"table table-sm MyTable\">\r\n              <tr");
            BeginWriteAttribute("id", " id=", 1454, "", 1464, 1);
#nullable restore
#line 47 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Home\MeritList.cshtml"
WriteAttributeValue("", 1458, count, 1458, 6, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" onclick=\"ToggleMenu(this)\">\r\n                  <td class=\"MyTableTd\">\r\n                    <h5><u>");
#nullable restore
#line 49 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Home\MeritList.cshtml"
                      Write(obj.Discipline);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 49 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Home\MeritList.cshtml"
                                      Write(obj.SemesterCount);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 49 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Home\MeritList.cshtml"
                                                         Write(obj.Section);

#line default
#line hidden
#nullable disable
            WriteLiteral("</u></h5>\r\n                  </td>\r\n                  <td class=\"MyTableTd\" style=\"text-align:right\" colspan=\"6\"> \r\n                    <h5><u>Total Strength : ");
#nullable restore
#line 52 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Home\MeritList.cshtml"
                                       Write(obj.ClassStrength);

#line default
#line hidden
#nullable disable
            WriteLiteral("</u></h5>\r\n                  </td>\r\n              </tr>                               \r\n          </table>\r\n      </div>\r\n");
#nullable restore
#line 57 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Home\MeritList.cshtml"
 foreach(var i in obj.FaatScholarLog)
        
          {
            using(Html.BeginForm("MeritList", "Home",FormMethod.Post))
            { 

#line default
#line hidden
#nullable disable
            WriteLiteral("                <div class=\"table-responsive\">\r\n\r\n                <table class=\"table table-sm table-borderless table-hover MyTable\" style=\"table-layout:fixed\" >\r\n                      <tr");
            BeginWriteAttribute("id", " id=", 2248, "", 2258, 1);
#nullable restore
#line 65 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Home\MeritList.cshtml"
WriteAttributeValue("", 2252, count, 2252, 6, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            BeginWriteAttribute("class", " class=\"", 2258, "\"", 2281, 2);
            WriteAttributeValue("", 2266, "collapse", 2266, 8, true);
#nullable restore
#line 65 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Home\MeritList.cshtml"
WriteAttributeValue(" ", 2274, count, 2275, 6, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\r\n                          <td style=\"display:none\">\r\n                              ");
#nullable restore
#line 67 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Home\MeritList.cshtml"
                         Write(Html.TextBox("Discipline",obj.Discipline,new{@style="border: 0px none;"}));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n                          </td>\r\n                          <td style=\"display:none\">\r\n                            ");
#nullable restore
#line 71 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Home\MeritList.cshtml"
                       Write(Html.Hidden("SemesterNo",obj.Semester,new{@style="width:0px"}));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n                          </td>\r\n                          <td style=\"display:none\">\r\n                            ");
#nullable restore
#line 75 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Home\MeritList.cshtml"
                       Write(Html.Hidden("SemesterCount",obj.SemesterCount,new{@style="width:0px"}));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n                          </td>\r\n                          <td style=\"display:none\">\r\n                            ");
#nullable restore
#line 79 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Home\MeritList.cshtml"
                       Write(Html.Hidden("Section",obj.Section,new{@style="width:0px"}));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n                          </td>\r\n                          \r\n                          \r\n        \r\n                          <td style=\"vertical-align: middle;\" colspan=\"2\" class=\"MyTableTd\">\r\n                            <input type=\"hidden\"");
            BeginWriteAttribute("value", " value=", 3234, "", 3250, 1);
#nullable restore
#line 86 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Home\MeritList.cshtml"
WriteAttributeValue("", 3241, i.AridNo, 3241, 9, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" name=\"RegistrationNo\" style=\"border: 0px none;\" readonly></input>\r\n                              ");
#nullable restore
#line 87 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Home\MeritList.cshtml"
                         Write(i.AridNo);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                          </td>\r\n                            <td colspan=\"2\" style=\"vertical-align: middle;text-align:center;\" class=\"MyTableTd\">\r\n                              ");
#nullable restore
#line 90 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Home\MeritList.cshtml"
                         Write(Math.Round(Convert.ToDecimal(i.Cgpa), 2));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            <input type=\"hidden\"");
            BeginWriteAttribute("value", " value=", 3627, "", 3641, 1);
#nullable restore
#line 91 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Home\MeritList.cshtml"
WriteAttributeValue("", 3634, i.Cgpa, 3634, 7, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" name=\"CGPA\" style=\"border: 0px none;text-align:center;border:none;\" readonly></input>\r\n                            </td>\r\n                            <td style=\"padding: 0;vertical-align:middle;\" class=\"MyTableTd\">\r\n");
#nullable restore
#line 94 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Home\MeritList.cshtml"
                               if(i.Status == "Accept")
                              {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <input");
            BeginWriteAttribute("value", " value=", 3986, "", 4004, 1);
#nullable restore
#line 96 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Home\MeritList.cshtml"
WriteAttributeValue("", 3993, i.T.Credit, 3993, 11, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" name=\"AllocationAmount\" style=\"width:100%;\"></input>  \r\n");
#nullable restore
#line 97 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Home\MeritList.cshtml"
                              }
                              else
                              {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <input");
            BeginWriteAttribute("value", " value=\"", 4201, "\"", 4209, 0);
            EndWriteAttribute();
            WriteLiteral(" disabled name=\"AllocationAmount\" style=\"width:100%;\"></input>  \r\n");
#nullable restore
#line 101 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Home\MeritList.cshtml"
                              }

#line default
#line hidden
#nullable disable
            WriteLiteral("                          </td>\r\n                          <td>\r\n                            ");
#nullable restore
#line 104 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Home\MeritList.cshtml"
                       Write(Html.Hidden("Status",obj.Semester,new{@style="width:0px"}));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n                          </td>\r\n                            <td>\r\n");
#nullable restore
#line 108 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Home\MeritList.cshtml"
                               if(i.Status == "Accept")
                              {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                  <button type=\"submit\" disabled name=\"Submit\"");
            BeginWriteAttribute("id", " id=", 4700, "", 4713, 1);
#nullable restore
#line 110 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Home\MeritList.cshtml"
WriteAttributeValue("", 4704, i.AridNo, 4704, 9, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" value=\"Accept\"  class=\"btn btn-success btn-sm\">Accept</button>\r\n");
#nullable restore
#line 111 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Home\MeritList.cshtml"
                              }
                              else
                              {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                  <button type=\"submit\" name=\"Submit\"");
            BeginWriteAttribute("id", " id=", 4949, "", 4962, 1);
#nullable restore
#line 114 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Home\MeritList.cshtml"
WriteAttributeValue("", 4953, i.AridNo, 4953, 9, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" value=\"Accept\"  class=\"btn btn-success btn-sm\">Accept</button>\r\n");
#nullable restore
#line 115 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Home\MeritList.cshtml"
                              }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                          </td>\r\n                            <td>\r\n");
#nullable restore
#line 119 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Home\MeritList.cshtml"
                               if(i.Status == "Reject")
                              {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                  <button type=\"submit\" disabled name=\"Submit\" value=\"Reject\" class=\"btn btn-danger btn-sm\">Reject</button>\r\n");
#nullable restore
#line 122 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Home\MeritList.cshtml"
                              }    
                              else
                              {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                  <button type=\"submit\" name=\"Submit\" value=\"Reject\" class=\"btn btn-danger btn-sm\">Reject</button> \r\n");
#nullable restore
#line 126 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Home\MeritList.cshtml"
                              }

#line default
#line hidden
#nullable disable
            WriteLiteral("                          </td>\r\n                      </tr>\r\n\r\n\r\n                </table>\r\n              </div>\r\n");
#nullable restore
#line 133 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Home\MeritList.cshtml"
                                
            }
            
      
      
      }

#line default
#line hidden
#nullable disable
#nullable restore
#line 138 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Home\MeritList.cshtml"
       
      count++;
      }

#line default
#line hidden
#nullable disable
            WriteLiteral("</section>   \r\n");
            WriteLiteral(@"                  <script type=""text/javascript"">
                              function SubmitForms()
                              { 

                                
                              
                             
                                var AllocatedAmountLength=document.getElementsByName(""AllocationAmount"").length;
                                var Amount = [];
                                for (i = 0; i < AllocatedAmountLength; i++) { 
                                  //Push each element to the array
                                  Amount.push(document.getElementsByName(""AllocationAmount"")[i].value);
                                }


                                var RegistrationNoLength=document.getElementsByName(""RegistrationNo"").length;
                                var RegisterNo = [];
                                for (i = 0; i < RegistrationNoLength; i++) { 
                                  //Push each element to the array
                  ");
            WriteLiteral(@"                RegisterNo.push(document.getElementsByName(""RegistrationNo"")[i].value);
                                }

                                
                               
                               
                                                       $.post(""/Home/CompleteList"",
                                                        {
                                                          AllocatedAmount: Amount,
                                                          RegistrationNo : RegisterNo,
                                                          Status : ""Accept"",
                                                          Type : ""MeritList"",
                                                          Discipline: '");
#nullable restore
#line 178 "C:\Users\muham\Source\Repos\FinancialAidAllocationTool\FinancialAidAllocationTool\Views\Home\MeritList.cshtml"
                                                                  Write(Model[1].Discipline);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"'
                                                        },
                                                        function(data, status){
                                                         // alert(""Data: "" + data + ""\nStatus: "" + status);
                                                        });
                           
                              return true;
                              }

                              
                              function ToggleMenu(x)
                              {   
                                 
                                  $("".""+x.id).toggleClass(""collapse"");
                                  
                                  
                              }
                            </script>
");
            WriteLiteral(@"               
                            
                       
                          
             
                      
                       
                        
                       
              
         
               
               

                


");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<FinancialAidAllocationTool.Models.FaatClassDefinition>> Html { get; private set; }
    }
}
#pragma warning restore 1591
