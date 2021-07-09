﻿using WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;

namespace WebApi.Controllers
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
                db.Ads.Add(new Ad { Name = "Внимание!", Description = "Спасибо за внимание!", Date = DateTime.Now, Price = 0 , Photo = "" }) ;
                db.SaveChanges();
            }
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ad>>> GetAll()
        {
            return await db.Ads.ToListAsync();
        }

        [HttpGet("{orderBy}/{asc}")]
        public async Task<ActionResult<IEnumerable<Ad>>> GetAllWithSort(string orderBy = null, bool asc = true)
        {
            if (orderBy == "Price" && asc)
                return await db.Ads.OrderBy(w => w.Price).ToListAsync();
            if (orderBy == "Price" && !asc)
                return await db.Ads.OrderByDescending(w => w.Price).ToListAsync();
            if (orderBy == "Date" && asc)
                return await db.Ads.OrderBy(w => w.Date).ToListAsync();
            if (orderBy == "Date" && !asc)
                return await db.Ads.OrderByDescending(w => w.Date).ToListAsync();
            else
                return await db.Ads.OrderBy(w => w.Id).ToListAsync();
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