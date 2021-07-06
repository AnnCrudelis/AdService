using AdServiceApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using FromBodyAttribute = Microsoft.AspNetCore.Mvc.FromBodyAttribute;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using HttpPutAttribute = Microsoft.AspNetCore.Mvc.HttpPutAttribute;


namespace AdServiceApi.Controllers
{
    public class ValuesController : ApiController
    {
        AdContext db = new AdContext();

        public IEnumerable<AdModel> GetBooks()
        {
            return db.Ads;
        }

        public AdModel GetBook(int id)
        {
            AdModel ad = db.Ads.Find(id);
            return ad;
        }

        [HttpPost]
        public void CreateBook([FromBody] AdModel ad)
        {
            db.Ads.Add(ad);
            db.SaveChanges();
        }

        [HttpPut]
        public void EditBook(int id, [FromBody] AdModel ad)
        {
            if (id == ad.Id)
            {
                db.Entry(ad).State = EntityState.Modified;

                db.SaveChanges();
            }
        }

        public void DeleteBook(int id)
        {
            AdModel ad = db.Ads.Find(id);
            if (ad != null)
            {
                db.Ads.Remove(ad);
                db.SaveChanges();
            }
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
