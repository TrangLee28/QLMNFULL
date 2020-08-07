using IdentitySample.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SchoolManager.Models;

namespace IdentitySample.Controllers
{
    [Authorize(Roles = "Quản trị viên")]
    public class UsersAdminController : Controller
    {
        TruongMamNonEntities db;
        public UsersAdminController()
        {
            db = new TruongMamNonEntities();
        }
        
        public UsersAdminController(ApplicationUserManager userManager, ApplicationRoleManager roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
            db = new TruongMamNonEntities();
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        //
        // GET: /Users/
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.RoleId = new SelectList(RoleManager.Roles.ToList(), "Name", "Name");
            return View(db.AspNetUsers.Where(x=>!x.Deleted).ToList());
        }

        //
        // GET: /Users/Details/5
        [HttpGet]
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);

            ViewBag.RoleNames = await UserManager.GetRolesAsync(user.Id);

            return View(user);
        }

        //
        // GET: /Users/Create
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            //Get the list of Roles
            ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Name", "Name");
            return View();
        }

        //
        // POST: /Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DateTime NgaySinh, string GioiTinh,string selectedRoles, string UserName, string Email, string hoten, string PhoneNumber, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = UserName, Email = Email, PhoneNumber = PhoneNumber };
                var adminresult = await UserManager.CreateAsync(user, "333333");
                string subPath = "~/Content/UserUpload/img/account/";
                string img = "";
                bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));
                AspNetUser u = new AspNetUser();
                u = db.AspNetUsers.Where(x => x.Id == user.Id).FirstOrDefault();
                u.HoTen = hoten;

                u.NamSinh = NgaySinh.Year.ToString();
                if (!exists)
                    System.IO.Directory.CreateDirectory(Server.MapPath(subPath));
                try
                {
                    if (file!=null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var path = Path.Combine(Server.MapPath(subPath), fileName);
                        file.SaveAs(path);
                        
                        img = u.img = fileName;
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    //return Json(new { Success = "false", Error = ex.Message });
                    //return RedirectToAction("Index");
                }
                //Add User to the selected Roles 
                if (adminresult.Succeeded)
                {
                    if (selectedRoles != null)
                    {
                        if(selectedRoles =="Phụ huynh")
                        {
                            PhuHuynh ph = new PhuHuynh();
                            ph.HoTen = hoten;
                            ph.NgayTao = DateTime.Now;
                            ph.SDT = PhoneNumber;
                            ph.NamSinh = NgaySinh.Year.ToString();
                            u.PhuHuynh = ph;
                            //if (GioiTinh == "Nam")
                            //    ph.PhuHuynh_HocSinh = "Bố";
                            //else ph.QuanHeVoiHocSinh = "Mẹ";
                            //db.PhuHuynhs.Add(ph);
                            db.SaveChanges();
                        }
                        if (selectedRoles == "Giáo viên")
                        {
                            GiaoVien gv = new GiaoVien();
                            gv.HoTen = hoten;
                            gv.NgayTao = DateTime.Now;
                            gv.img = img;
                            gv.SDT = PhoneNumber;
                            gv.NgaySinh = NgaySinh;
                            gv.GioiTinh = GioiTinh;
                            u.GiaoVien = gv;
                            db.SaveChanges();
                        }
                        var result = await UserManager.AddToRolesAsync(user.Id, selectedRoles);
                        
                        if (!result.Succeeded)
                        {
                            ModelState.AddModelError("", result.Errors.First());
                            ViewBag.RoleId = new SelectList(await RoleManager.Roles.ToListAsync(), "Name", "Name");
                            return View();
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", adminresult.Errors.First());
                    ViewBag.RoleId = new SelectList(RoleManager.Roles, "Name", "Name");
                    return View();

                }
                return RedirectToAction("Index");
            }
            ViewBag.RoleId = new SelectList(RoleManager.Roles, "Name", "Name");
            return View();
        }

        //
        // GET: /Users/Edit/1
        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            var userRoles = await UserManager.GetRolesAsync(user.Id);

            return View(new EditUserViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                RolesList = RoleManager.Roles.ToList().Select(x => new SelectListItem()
                {
                    Selected = userRoles.Contains(x.Name),
                    Text = x.Name,
                    Value = x.Name
                })
            });
        }

        //
        // POST: /Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Email,Id")] EditUserViewModel editUser, params string[] selectedRole)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(editUser.Id);
                if (user == null)
                {
                    return HttpNotFound();
                }

                user.UserName = editUser.Email;
                user.Email = editUser.Email;

                var userRoles = await UserManager.GetRolesAsync(user.Id);

                selectedRole = selectedRole ?? new string[] { };

                var result = await UserManager.AddToRolesAsync(user.Id, selectedRole.Except(userRoles).ToArray<string>());

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                result = await UserManager.RemoveFromRolesAsync(user.Id, userRoles.Except(selectedRole).ToArray<string>());

                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", result.Errors.First());
                    return View();
                }
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("", "Something failed.");
            return View();
        }

        //
        // GET: /Users/Delete/5
        [HttpGet]
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        //
        // POST: /Users/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            if (ModelState.IsValid)
            {
                if (id == null)
                {
                    return Json(new { Success = "false" });
                }


                var user = db.AspNetUsers.Where(x=>x.Id== id).FirstOrDefault();
                if (user == null)
                {
                    return Json(new { Success = "false" });
                }

                user.Deleted = true;
                db.SaveChanges();
                
                return Json(new { Success = "true" });
            }
            return Json(new { Success = "true" });
        }
    }
}
