﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using ShopThrift.Core.Models;
using ShopThrift.Core.ViewModels;
using ShopThrift.Core.Contracts;
using System.Web;
using System.IO;

namespace ShopThrift.WebUIs.Controllers
{  
    public class ProductManagerController : Controller
    {
        IRepository<Product> context;
        IRepository<ProductCategory> productCategories;
        public ProductManagerController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext)
        {
            context = productContext;
            productCategories = productCategoryContext;
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
        }

        public ActionResult Create()
        {
            ProductManagerViewModel viewModel = new ProductManagerViewModel();
            viewModel.Product = new Product();
            viewModel.ProductCategories = productCategories.Collection();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            if(file != null){
                product.Image = product.Id + Path.GetExtension(file.FileName);
                file.SaveAs(Server.MapPath("//Content//ProductImages//") + product.Image);
            }
            context.Insert(product);
            context.Commit();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string Id)
        {
            Product product = context.Find(Id);
            if(product == null)
            {
                return HttpNotFound();
            }
            ProductManagerViewModel viewModel = new ProductManagerViewModel();
            viewModel.Product = product;
            viewModel.ProductCategories = productCategories.Collection();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(Product product, String Id, HttpPostedFileBase file)
        {
            Product productToEdit = context.Find(Id);
            if (productToEdit == null)
            {
                return HttpNotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(product);
            }
            if (file != null)
            {
                productToEdit.Image = product.Id + Path.GetExtension(file.FileName);
                file.SaveAs(Server.MapPath("//Content//ProductImages//") + productToEdit.Image);
            }
            productToEdit.Category = product.Category;
            productToEdit.Description = product.Description;
            productToEdit.Name = product.Name;
            productToEdit.Price = product.Price;

            context.Commit();

            return RedirectToAction("Index");
        }

        public ActionResult Delete(string Id)
        {
            Product productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            return View(productToDelete);
        }

        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string Id)
        {
            Product productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            context.Delete(Id);
            context.Commit();
            return RedirectToAction("Index");
        }
    }
}