using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PartyWeb.Models.DataAccessPostgreSqlProvider;
using PartyMaker;

namespace WebApplication.Controllers
{
    public class UploadController : Controller
    {
        // GET: Upload
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DoUpload(IFormFile file)
        {
            using (var stream = file.OpenReadStream())
            {
                var xs = new XmlSerializer(typeof(PartyModel));
                var party = (PartyModel)xs.Deserialize(stream);


                using (var db = new PartyDbContext())
                {
                    var dbs = new DbParty()
                    {
                        TypeofEvent = party.TypeofEvent,
                        Distination = party.Distination,
                        Services = party.Services,
                    };
                    dbs.Workers = new Collection<DbWorkers>();
                    foreach (var worker in party.Workers)
                    {
                        dbs.Workers.Add(new DbWorkers()
                        {
                            Name = worker.Name,
                            Position = worker.Position,
                            Experience = worker.Experience,
                        });
                    }
                    db.ManyParties.Add(dbs);
                    db.SaveChanges();
                }

                return View(party);
            }
        }



        public ActionResult List()
        {
            List<DbParty> list;
            using (var db = new PartyDbContext())
            {
                list = db.ManyParties.Include(s => s.Workers).ToList();
            }
            return View(list);
        }


      
    }
}