using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Final_Project.Areas.Admin.ViewModels.Topic;
using Final_Project.Data;
using Final_Project.Models;
using Final_Project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Final_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TopicController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ITopicService _topicService;
        private readonly IWebHostEnvironment _env;

        public TopicController(AppDbContext context,
                                  ITopicService topicService,
                                  IWebHostEnvironment env)
        {
            _context = context;
            _topicService = topicService;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<TopicVM> list = new();

            List<Topic> datas = await _context.Topics.OrderByDescending(m => m.Id).ToListAsync();

            foreach (var item in datas)
            {
                list.Add(new TopicVM
                {
                    Id = item.Id,
                    Name = item.Name,
                    CreateDate = item.CreatedDate.ToString("dddd, dd MMMM yyyy")
                });
            }

            return View(list);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            Topic dbTopic = await _topicService.GetByIdAsync(id);
            if (dbTopic is null) return NotFound();
            return View(_topicService.GetMappedDatasAsync(dbTopic));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(TopicCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Topic topic = new()
            {
                Name = request.Name
            };

            await _context.Topics.AddAsync(topic);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();
            Topic dbTopic = await _topicService.GetByIdAsync(id);
            if (dbTopic is null) return NotFound();

            return View(new TopicEditVM { Id = dbTopic.Id, Name = dbTopic.Name });
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int? id, TopicEditVM request)
        {
            if (id is null) return BadRequest();
            Topic dbTopic = await _topicService.GetByIdAsync(id);
            if (dbTopic is null) return NotFound();

            dbTopic.Name = request.Name;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return BadRequest();

            await _topicService.DeleteAsync((int)id);

            return Ok();
        }
    }
}

