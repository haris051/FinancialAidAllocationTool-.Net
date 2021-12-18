using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using FinancialAidAllocationTool.Models;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using FinancialAidAllocationTool.Models.Application;




namespace FinancialAidAllocationTool.Controllers
{
    public class AccountController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly FaaToolDBContext _context;

        public DBAccessLayer DBManager;
        public AccountController(IConfiguration configuration,FaaToolDBContext context)
        {
            this.configuration = configuration;
            DBManager = new DBAccessLayer(configuration,_context);
            _context = context;
        }
//public DBAccessLayer obj = new DBAccessLayer(configuration);
        [HttpGet]
        public IActionResult Login()
        {
        // TempData["Roles"] = "";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login (LoginView model)
        {        


            if(model.Mode == "Parent")
            {
                model.Password = DecodeFrom64(_context.Users.Where(e=>e.AridNo == model.Email).FirstOrDefault().Password);
            }
           
            if(LoginUser(model.Email, model.Password)!=null)
            {   
                LoginView obj = LoginUser(model.Email,model.Password);
                var name = _context.Users.Where(e=>e.AridNo==obj.Email).ToList().Select(e=> e.Name).FirstOrDefault();
               // var img = _context.FaatAppMotherOtherIncomeResourceFiles.Select(e=> new{e.FileData,e.FileType}).FirstOrDefault();
               // string img1 = Convert.ToBase64String(img.FileData);
                
                var msg = "";   
                var attempt = _context.FaatApplication.Where(e=>e.AridNo == obj.Email).Count();
                if(attempt>0)
                {
                    msg = "";
                }
                else
                {
                    msg = "first";
                }                  
                var claims = new List<Claim>
                {
                    
                    new Claim(ClaimTypes.Email, obj.Email),
                    new Claim(ClaimTypes.Role , obj.Role),
                    new Claim(ClaimTypes.Name , name),
                    new Claim(ClaimTypes.IsPersistent,model.RememberMe.ToString()),
                    new Claim("Attempt",msg)
                    //new Claim("image",img1),
           //         new Claim(ClaimTypes.UserData,img1)
                    
                };

                var userIdentity = new ClaimsIdentity(claims, "login");
                

                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);
            // TempData["Login User"] = _context.Users.ToList().Where(d=>d.EmailAddress==model.Email).Select(d=>d.Id);
                //Just redirect to our index after logging in. 
                if(obj.Role =="Admin" || obj.Role=="Committee")
                {
                    return RedirectToAction("Dashboard","Home");
                }
                else
                {
                    if(model.Mode=="Parent")
                    {
                        var App = _context.FaatApplication.Where(e=>e.AridNo == model.Email).FirstOrDefault();
                        if(App != null)
                        {
                return RedirectToAction("Parent","Application");
                        }
                        else
                        {
                            TempData["ParentLogin"] = "Student didnt create a profile yet";
                        }
                    }
                    else if(model.Mode=="Student")
                    {
                return RedirectToAction("index","Application");

                    }
                }
            }
            		     
            return View();
         }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterView model)
        {
            if(ModelState.IsValid)
            {
                 
                model.Password = EncodePasswordToBase64(model.Password);
                DBManager.RegisterUser(model,_context);
                return RedirectToAction("Index","Home");
            }

            return View();
        }

        
        public IActionResult LoginCheck(string Email)
        {         
            var User = _context.Users.ToList();

            var SearchData = User.Where(d=>d.AridNo.Contains(Email)).FirstOrDefault();
            
          //  bool FindUser = DBManager.FindUser(Email);
            if(SearchData==null)
            {
                return Json(new 
                { 
                    
                    
                    status ="Error"
                     
                });
            }
            else if(SearchData.Role == "Admin" || SearchData.Role=="Committee") 
            {
                return Json(new 
                { 
                    status ="AC"
                     
                });
            }
            else
            {
                return Json(new 
                { 
                    status ="OK"
                     
                });

            }
        }
        [AcceptVerbs("GET","Post")]
        public IActionResult EmailAvailabilty(string AridNo)
        {         
            var User = _context.Users.ToList();

            var SearchData = User.Where(d=>d.AridNo.Contains(AridNo)).SingleOrDefault();
            
            bool FindUser = DBManager.FindUser(AridNo);
            if(SearchData==null && FindUser==true)
            {
                return Json(true);
            }
            else if(FindUser==false)
            {
                return Json($"Arid No  {AridNo} does not exists.");
            }
            else 
            {
                return Json($"Arid No  {AridNo} is already in use.");
            }
        }
        public static string EncodePasswordToBase64(string password) 
        {
                    try 
                {
                    byte[] encData_byte = new byte[password.Length]; 
                    encData_byte = System.Text.Encoding.UTF8.GetBytes(password); 
                    string encodedData = Convert.ToBase64String(encData_byte); 
                    return encodedData; 
                } 
                    catch (Exception ex) 
                { 
                    throw new Exception("Error in base64Encode" + ex.Message); 
                } 
        }
        [HttpGet]
        public async Task<IActionResult> LogOut()
        {


            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);


            return RedirectToAction("index","Home");
        }
    private LoginView LoginUser(string email, string password)
	{    
         
		
          var UsersList  =  _context.Users.ToList().Where(obj=>obj.AridNo==email && DecodeFrom64(obj.Password).Equals(password)).SingleOrDefault();        
          if(UsersList!= null)
          {
             LoginView obj = new LoginView(); 
             obj.Email = UsersList.AridNo;
             obj.Password = UsersList.Password;
             obj.Role = UsersList.Role; 
             return obj;
      
          }
          else
          {
              return null;
          }
          


	}
    public string DecodeFrom64(string encodedData) 
    {
        System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding(); 
        System.Text.Decoder utf8Decode = encoder.GetDecoder();
        byte[] todecode_byte = Convert.FromBase64String(encodedData); 
        int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length); 
        char[] decoded_char = new char[charCount]; 
        utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0); 
        string result = new String(decoded_char); 
        return result;
    }  
    [HttpGet]
    public IActionResult Committee()
    {
        List<Users> users = _context.Users.Where(e=>e.Role=="Committee").ToList();
        foreach(var user in users)
        {
            user.Password = DecodeFrom64(user.Password);
        }
        var CommitteeData = new Tuple<List<Users>,Users>(users,new Users());
                return View(CommitteeData);
    }      
    [HttpPost]
      public IActionResult Committee([Bind(Prefix = "Item2")]Users user)
    {
       
        user.Password=EncodePasswordToBase64(user.Password);
        user.Role = "Committee";
        if(_context.Users.Where(e=>e.AridNo.Contains(user.AridNo)).Count()==0)
        {
        _context.Add(user);
        _context.SaveChanges();
        return RedirectToAction("Committee");
        }
        else
        {
        TempData["Error"]= "Sorry user Already Exists";
         return RedirectToAction("Committee");

        }
      
        
    } 
        
        public IActionResult DeleteMember(int id)
        {
            
            Users user = new Users{Id=id};
            List<FaatAppComments> comments = new List<FaatAppComments>();
            comments = _context.FaatAppComments.Where(e=>e.UserId == id).ToList();
            _context.RemoveRange(comments);
            _context.SaveChanges();
            _context.Users.Remove(user);
            _context.SaveChanges();
            
            return Ok();
            
        }

}
    



}