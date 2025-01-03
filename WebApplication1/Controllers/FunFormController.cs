using EntityFrameworkCore.MySQL.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class FunFormController : Controller{
        private readonly AppDbContext _appDbContext;
        public FunFormController(AppDbContext appDbContext){
            _appDbContext = appDbContext;
        }

        public IActionResult Calendar(){ return View(); }
        
        [HttpGet]
        public IActionResult Add(){ return View(); }
        
        [HttpGet]
        public async Task<IActionResult> List(){
            var funForms = await _appDbContext.FunForms.ToListAsync();
            return View(funForms);
        }        

        [HttpPost]
        public async Task<IActionResult> Add(AddFunForm funForm){
            var form = new FunForm{
                Name = funForm.Name,
                DoB = funForm.DoB,
                PhoneNumber = funForm.PhoneNumber,
                Occupation = funForm.Occupation,
                BankAccounts = funForm.BankAccounts,
                Question = funForm.Question,
                Comments = funForm.Comments
            };
            await _appDbContext.FunForms.AddAsync(form);
            await _appDbContext.SaveChangesAsync();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id){
            var form = await _appDbContext.FunForms.FindAsync(id);
            return View(form);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(FunForm funForm){
            var form = await _appDbContext.FunForms.FindAsync(funForm.ID);
            if (form!=null){
                form.Name = funForm.Name;
                form.DoB = funForm.DoB;
                form.PhoneNumber = funForm.PhoneNumber;
                form.Occupation = funForm.Occupation;
                form.BankAccounts = funForm.BankAccounts;
                form.Comments = funForm.Comments;
                await _appDbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "FunForm");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(FunForm funForm)
        {
            var form = await _appDbContext.FunForms.AsNoTracking()
                .FirstOrDefaultAsync(x=>x.ID == funForm.ID);
            if (form != null)
            {
                _appDbContext.FunForms.Remove(funForm);
                await _appDbContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "FunForm");
        }
    }
}