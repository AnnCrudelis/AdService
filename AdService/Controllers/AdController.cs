using AdService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdController : ControllerBase
    {
        AdContext db;
        public AdController(AdContext context)
        {
            db = context;
            if (!db.Ads.Any())
            {
                db.Ads.Add(new Ad { Name = "Внимание!", Description = "Спасибо за внимание!", Date = DateTime.Now, Price = 0 , Photo = "https://lh3.googleusercontent.com/proxy/4Nn3okrqkjHmvv0wTqWhY-BzIeHW0GmZPW_JtCYRzeIKwnqs-kM42KfBk9bVnY6R5cyU98duJcU359uafOIt31wnIRO-zShkjcOFlVz0tWl2fwEja3Omw_6KylIKYoU" }) ;
                db.SaveChanges();
            }
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ad>>> Get()
        {
            return await db.Ads.OrderBy(w => w.Name).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ad>> Get(int id)
        {
            Ad ad = await db.Ads.FirstOrDefaultAsync(x => x.Id == id);
            if (ad == null)
                return NotFound();
            return new ObjectResult(ad);
        }

        [HttpPost]
        public async Task<ActionResult<Ad>> Post(Ad ad)
        {
            if (ad == null)
            {
                return BadRequest();
            }

            db.Ads.Add(ad);
            await db.SaveChangesAsync();
            return Ok(ad);
        }

        [HttpPut]
        public async Task<ActionResult<Ad>> Put(Ad ad)
        {
            if (ad == null)
            {
                return BadRequest();
            }
            if (!db.Ads.Any(x => x.Id == ad.Id))
            {
                return NotFound();
            }

            db.Update(ad);
            await db.SaveChangesAsync();
            return Ok(ad);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Ad>> Delete(int id)
        {
            Ad ad = db.Ads.FirstOrDefault(x => x.Id == id);
            if (ad == null)
            {
                return NotFound();
            }
            db.Ads.Remove(ad);
            await db.SaveChangesAsync();
            return Ok(ad);
        }
    }
}
