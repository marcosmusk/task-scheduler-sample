using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskScheduler.Context;

namespace TaskScheduler.Controllers
{
    [Route("api/[controller]")]
    public class TaskController : Controller
    {
        public TaskDbContext _context;
        public TaskController(TaskDbContext context)
        {
            _context = context;
        }

        [HttpGet("[action]")]
        public async Task<IEnumerable<Models.Task>> GetTasks()
        {
            return await _context.Tasks.OrderBy(ob => ob.Priority).ToListAsync();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Edit(Guid? taskId)
        {
            if (taskId.HasValue)
            {
                var task = await _context.Tasks.FindAsync(taskId.Value);

                return Ok(task);
            }

            return Ok(new Models.Task());
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Edit([FromBody]Models.Task model)
        {
            if (model.Id == Guid.Empty)
            {
                await _context.AddAsync(model);
            }
            else
            {
                _context.Update(model);
            }

            await _context.SaveChangesAsync();

            return Ok(model);
        }

        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> Delete(Guid taskId)
        {
            var task = await _context.Tasks.FindAsync(taskId);
            _context.Remove(task);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
