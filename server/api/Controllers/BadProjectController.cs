using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReactAdvantage.API.Services;
using ReactAdvantage.Data.EntityFramework;
using ReactAdvantage.Domain.ViewModels.Projects;

// This is an example of a bad controller
namespace ReactAdvantage.API.Controllers
{
    [Produces("application/json")]
    [Route("api/v1/BadProjects")]
    public class BadProjectController : Controller
    {
        [HttpGet]
        [Produces(typeof(ProjectListDto))]
        public async Task<IActionResult> GetAll()
        {
            //This makes controller dependent on implementation of ProjectService
            var context = new ReactAdvantageContext("ReactAdvantage");
            var projectService = new ProjectService(context);

            try
            {
                return new ObjectResult(await projectService.GetAllAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ProjectDto))]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var context = new ReactAdvantageContext("ReactAdvantage");
            var projectService = new ProjectService(context);

            try
            {
                var projectDto = await projectService.GetByIdAsync(id);

                if (projectDto.NoData)
                {
                    return NotFound();
                }

                return Ok(projectDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(ProjectEditDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> SaveAsync([FromBody] ProjectEditDto project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var context = new ReactAdvantageContext("ReactAdvantage");
            var projectService = new ProjectService(context);

            await projectService.CreateAsync(project);

            return CreatedAtAction(nameof(GetByIdAsync), new { id = project.Id }, project);
        }
    }


}
