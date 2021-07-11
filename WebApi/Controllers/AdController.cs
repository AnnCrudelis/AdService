using WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using WebApi.Wrappers;
using WebApi.Filter;
using WebApi.Helpers;
using WebApi.Services;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdController : ControllerBase
    {
        private readonly IUriService uriService;
        private readonly AdContext db; 

        public AdController(AdContext context, IUriService uriService)
        {
            db = context;
            this.uriService = uriService;
            if (!db.Ads.Any())
            {
                db.Ads.Add(new Ad { Name = "Внимание!", Description = "Спасибо за внимание!", Date = DateTime.Now, Price = 0 , Photo = "" }) ;
                db.Ads.Add(new Ad { Name = "Внимание!", Description = "Спасибо за внимание!", Date = DateTime.Now, Price = 0, Photo = "" });
                db.Ads.Add(new Ad { Name = "Внимание!", Description = "Спасибо за внимание!", Date = DateTime.Now, Price = 0, Photo = "" });
                db.Ads.Add(new Ad { Name = "Внимание!", Description = "Спасибо за внимание!", Date = DateTime.Now, Price = 0, Photo = "" });
                db.Ads.Add(new Ad { Name = "Внимание!", Description = "Спасибо за внимание!", Date = DateTime.Now, Price = 0, Photo = "" });
                db.Ads.Add(new Ad { Name = "Внимание!", Description = "Спасибо за внимание!", Date = DateTime.Now, Price = 0, Photo = "" });
                db.Ads.Add(new Ad { Name = "Внимание!", Description = "Спасибо за внимание!", Date = DateTime.Now, Price = 0, Photo = "" });
                db.Ads.Add(new Ad { Name = "Внимание!", Description = "Спасибо за внимание!", Date = DateTime.Now, Price = 0, Photo = "" });
                db.Ads.Add(new Ad { Name = "Внимание!", Description = "Спасибо за внимание!", Date = DateTime.Now, Price = 0, Photo = "" });
                db.Ads.Add(new Ad { Name = "Внимание!", Description = "Спасибо за внимание!", Date = DateTime.Now, Price = 0, Photo = "" });
                db.Ads.Add(new Ad { Name = "Внимание!", Description = "Спасибо за внимание!", Date = DateTime.Now, Price = 0, Photo = "" });
                db.Ads.Add(new Ad { Name = "Внимание!", Description = "Спасибо за внимание!", Date = DateTime.Now, Price = 0, Photo = "" });
                db.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationFilter filter)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize, filter.OrderBy);
            var pagedData = await db.Ads.Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToListAsync();

            if (filter.OrderBy == "Price")
                pagedData = await db.Ads.OrderBy(w => w.Price).Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToListAsync();
            if (filter.OrderBy == "PriceDesc")
                pagedData = await db.Ads.OrderByDescending(w => w.Price).Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToListAsync();
            if (filter.OrderBy == "Date")
                pagedData = await db.Ads.OrderBy(w => w.Date).Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToListAsync();
            if (filter.OrderBy == "DateDesc")
                pagedData = await db.Ads.OrderByDescending(w => w.Date).Skip((validFilter.PageNumber - 1) * validFilter.PageSize).Take(validFilter.PageSize).ToListAsync();

            var totalRecords = await db.Ads.CountAsync();
            var pagedReponse = PaginationHelper.CreatePagedReponse<Ad>(pagedData, validFilter, totalRecords, uriService, route);
            return Ok(pagedReponse);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Ad>> GetById(int id)
        {
            Ad ad = await db.Ads.FirstOrDefaultAsync(x => x.Id == id);
            if (ad == null)
                return NotFound();
            return new ObjectResult(ad);
        }

        [HttpPost]
        public async Task<ActionResult<Ad>> Post(Ad ad)
        {
            try
            {
                if (ad == null)
                {
                    return BadRequest();
                }

                if (ad.Price < 0)
                    ModelState.AddModelError("Price", "Price must be > 0");


                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                db.Ads.Add(ad);
                await db.SaveChangesAsync();
                return Ok(ad);
            }
            catch (Exception ex)
            {
                var q = ex;
                return BadRequest();
            }
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
