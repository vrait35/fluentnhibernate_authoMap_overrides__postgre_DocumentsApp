using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DosumentsApp.Models;
using System.IO;
using DbContextLibrary.MongoDb;
using Microsoft.AspNetCore.Http;
using DbContextLibrary.PostgreSql;
using DbContextLibrary.Entities;

namespace DosumentsApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly MongoDbContext db;
        public HomeController(MongoDbContext mongoContext)
        {
            db = mongoContext;
        }
        public async Task< IActionResult> Index()
        {            
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {                    
                    var account= session.Query<Account>().Where(x => x.Login == "login3").FirstOrDefault();
                    if(account==null)account= new Account { Login = "login3", Password = "pass3" };                    
                    var personFrom = session.Query<Person>().Where(x => x.Name == "Anuar32").FirstOrDefault();
                    var personTo = session.Query<Person>().Where(x => x.Name == "Anuar4").FirstOrDefault();
                    var orgranization = session.Query<Organization>().Where(x => x.Name == "org1").FirstOrDefault();
                    if (orgranization == null) orgranization = new Organization { Name = "org1" };
                    await session.SaveOrUpdateAsync(orgranization);
                    if (personFrom == null)
                    {
                        personFrom = new Person
                        { 
                            Name="Anuar32",
                            Organization=orgranization,
                            Account=account
                        };
                       // account.Persons.Add(personFrom);
                    }
                    if (personTo == null)
                    {
                        personTo = new Person
                        {
                            Name = "Anuar4",
                            Organization=orgranization,
                            Account=account
                        };
                        account.Persons.Add(personTo);
                    }                    
                    session.SaveOrUpdate(account);
                    session.SaveOrUpdate(personTo);
                    session.SaveOrUpdate(personFrom);
                    transaction.Commit();
                }                
            }
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Create(FileViewModel model)
        {            
            List<IFormFile> files = model.Files;
            //if (ModelState.IsValid && files.Count()>0)
            //{
                int isTrue = 0;
                foreach(var f in model.FileTypes)
                {
                    if (f == FileTypeEnum.Dogovor || f == FileTypeEnum.Otchet) isTrue++;
                }
                if (isTrue == 0) return View(model);
                //string docName = "testDocName";
                string docName = model.DocumentName;
                string id = null;
                List<string> listFileId = new List<string>();
                for (int i = 0; i < files.Count; i++)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await files[i].CopyToAsync(memoryStream);
                        id = await db.CreateFile(memoryStream.ToArray(), files[i].FileName);
                        listFileId.Add(id);
                    }                    
                }
                using (var session = NHibernateHelper.OpenSession())
                {
                    using (var transaction = session.BeginTransaction())
                    {
                        var personTo = session.Query<Person>().Where(x => x.Name == "Anuar4").FirstOrDefault();
                        var personFrom = session.Query<Person>().Where(x => x.Name == "Anuar32").FirstOrDefault();
                        var docForNumber = session.Query<Document>().OrderByDescending(x => x.Number).FirstOrDefault();
                        long number = 1;
                        if (docForNumber != null) number = docForNumber.Number + 1;
                        var document = new Document
                        {
                            Name = docName,
                            PersonTo = personTo,
                            PersonFrom = personFrom,
                            Number = number,
                            DateTimeRegistration = DateTime.Now,
                            DateTimeCreateDocument=model.DateTimeCreateDocument,
                            Status=true                            
                        };
                        session.SaveOrUpdate(document);
                        int i = 0;
                        foreach (var file in files)
                        {
                            var fileInfo = new DbContextLibrary.Entities.FileInfo
                            {
                                ExternalId = listFileId[i],
                                FileName = file.FileName,
                                Document=document,
                                FileType=model.FileTypes[i]
                            };                         
                            session.SaveOrUpdate(fileInfo);
                            i++;
                        }                        
                        transaction.Commit();
                    }
                }
                return RedirectToAction("Index");
           // }   
            //return View(null);
        }

        [HttpPost]
        public async Task<ActionResult> Create2(List<IFormFile> files)
        {
            if (ModelState.IsValid && files.Count() > 0)
            {
                string docName = "testDocName";
                string id = null;
                List<string> listFileId = new List<string>();
                for (int i = 0; i < files.Count; i++)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await files[i].CopyToAsync(memoryStream);
                        id = await db.CreateFile(memoryStream.ToArray(), files[i].FileName);
                        listFileId.Add(id);
                    }
                }
                using (var session = NHibernateHelper.OpenSession())
                {
                    using (var transaction = session.BeginTransaction())
                    {
                        var personTo = session.Query<Person>().Where(x => x.Name == "Anuar4").FirstOrDefault();
                        var personFrom = session.Query<Person>().Where(x => x.Name == "Anuar32").FirstOrDefault();
                        var docForNumber = session.Query<Document>().OrderByDescending(x => x.Number).FirstOrDefault();
                        long number = 1;
                        if (docForNumber != null) number = docForNumber.Number + 1;
                        var document = new Document
                        {
                            Name = docName,
                            PersonTo = personTo,
                            PersonFrom = personFrom,
                            Number = number,
                            DateTimeRegistration = DateTime.Now,
                            DateTimeCreateDocument = DateTime.Now,
                            Status = true
                           
                        };
                        session.SaveOrUpdate(document);
                        int i = 0;
                        foreach (var file in files)
                        {
                            var fileInfo = new DbContextLibrary.Entities.FileInfo
                            {
                                ExternalId = listFileId[i],
                                FileName = file.FileName,
                                Document = document
                            };
                            session.SaveOrUpdate(fileInfo);
                            i++;
                        }
                        transaction.Commit();
                    }
                }
                return RedirectToAction("Index");
            }
            return View(null);
        }
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }   
}
