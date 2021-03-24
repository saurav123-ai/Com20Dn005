using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Truyumcasestudy.Models;

namespace Truyumcasestudy.Controllers
{
    [HandleError]
   
    public class menusController : Controller
    {
        private truyum db = new truyum();

        // GET: menus
        [Authorize]
        public ActionResult Index()
        {
            return View(db.menus.ToList());
        }

        // GET: menus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            menu menu = db.menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // GET: menus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: menus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "menu_id,menu_name,price,date_of_lunch,active,category")] menu menu)
        {
            if (ModelState.IsValid)
            {
                db.menus.Add(menu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(menu);
        }

        // GET: menus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            menu menu = db.menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: menus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "menu_id,menu_name,price,date_of_lunch,active,category")] menu menu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(menu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(menu);
        }

        // GET: menus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            menu menu = db.menus.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            menu menu = db.menus.Find(id);
            db.menus.Remove(menu);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult menu()
        {
            TempData.Keep();
            var menus = db.menus.Where(m => m.active == "yes");
            return View(menus.ToList());
        }
        public ActionResult AddToCart(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //menu menu = db.menus.Find(id);
            //if (menu == null)
            //{
            //    return HttpNotFound();
            //}
            
           menu result = db.menus.Where(m => m.menu_id == id).SingleOrDefault();

           
            return View(result);
        }
        List<Item> li = new List<Item>();
        [HttpPost]
        public ActionResult AddToCart(menu menu,int qty, int id)
        {
            menu result = db.menus.Where(m => m.menu_id == id).SingleOrDefault();

            Item c = new Item();
            c.menu_id = result.menu_id;
            c.menu_name = result.menu_name;
            c.price = (double)result.price;
            c.quantity = qty;

            if(TempData["cart"]==null)
            {
                li.Add(c);
                TempData["cart"] = li;
            }
            else
            {
                List<Item> li2 = TempData["cart"] as List<Item>;
                foreach(var item in li2)
                {
                    if(item.menu_id==id)
                    {
                        int prevqty = item.quantity;
                        li2.Remove(item);
                        c.quantity = prevqty + qty;
                        li2.Add(c);
                        TempData["cart"] = li2;
                        break;

                    }
                    else
                    {
                        li2.Add(c);
                        TempData["cart"] = li2;
                    }
                }
                
            }
            
            TempData.Keep();

            return RedirectToAction("menu");

        }
        public ActionResult Cart()
        {
            TempData.Keep();
            return View();
        }

        public ActionResult Deletefromcart(int?id)
        {
            List<Item> cart = TempData["cart"] as List<Item>;
            foreach(var item in cart)
            {
                if(item.menu_id==id)
                {
                    cart.Remove(item);
                    break;
                }
                    
            }
            TempData["cart"] = cart;
            return RedirectToAction("menu");
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
