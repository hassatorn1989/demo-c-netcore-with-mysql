using System;
using DemoProductApp.Data;
using DemoProductApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoProductApp.Controllers;

public class ProductController : Controller
{

    private readonly AppDbContext _context;


    public ProductController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Products.ToListAsync());
    }


    public IActionResult Create(){
        return View();
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ProductName,ProductPrice")] Product product){
        if(ModelState.IsValid){
            product.CreatedAt = DateTime.Now;
            product.UpdatedAt = DateTime.Now;
            _context.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }


    public async Task<IActionResult> Detail(int? id){
        if(id == null){
            return NotFound();
        }

        var product = await _context.Products.FindAsync(id);

        if(product == null){
            return NotFound();
        }

        return View(product);
    }


    public async Task<IActionResult> Edit(int? id){
        if(id == null){
            return NotFound();
        }

        var product = await _context.Products.FindAsync(id);
        if(product == null){
            return NotFound();
        }

        return View(product);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,ProductName,ProductPrice")] Product product){
        if(id != product.Id){
            return NotFound();
        }   

        if(ModelState.IsValid){
            try{
                product.UpdatedAt = DateTime.Now;
                _context.Update(product);
                await _context.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException){
                if(!ProductExists(product.Id)){
                    return NotFound();
                }
                else{
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return Json(new { success = false, message = "Product not found" });
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return Json(new { success = true, message = "Product deleted successfully" });
    }

    private bool ProductExists(int id){
        return _context.Products.Any(e => e.Id == id);
    }

}
