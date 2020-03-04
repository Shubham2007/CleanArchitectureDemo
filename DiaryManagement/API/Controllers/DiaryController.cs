using Core.CustomExceptions;
using Core.Models.Diary;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace API.Controllers
{
    //[Route("api/{server}/[controller]")]
    [CustomRoute]
    [ApiController]
    public class DiaryController : ControllerBase
    {
        private readonly ILogger<DiaryController> _logger;
        private readonly Lazy<IDiaryService> _diaryService;

        public DiaryController(Lazy<IDiaryService> diaryService, ILogger<DiaryController> logger)
        {
            _diaryService = diaryService ?? throw new ArgumentNullException(nameof(diaryService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Add diary note for user
        /// </summary>
        /// <param name="createDiaryNoteDto"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(string), 400)]
        [HttpPost("CreateDiaryNote")]
        public async Task<IActionResult> CreateDiaryNote(CreateDiaryNoteDto createDiaryNoteDto)
        {
            try
            {
                _logger.LogInformation($"Executing {MethodBase.GetCurrentMethod().Name} Method Of Class: { this.GetType().Name}");

                bool success = await _diaryService.Value.CreateDiaryNote(createDiaryNoteDto);

                if (!success)
                {
                    return StatusCode(500, "Error occured while adding diary note");
                }

                return Ok("Diary note added successfully");
            }
            catch (InvalidUserIdException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get diary notes of user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(IEnumerable<DiaryNoteDto>), 200)]
        [ProducesResponseType(typeof(string), 500)]
        [ProducesResponseType(typeof(string), 400)]
        [HttpGet("GetUserDiaryNotes/{userId}")]
        public async Task<IActionResult> GetUserDiaryNotes(int userId)
        {
            try
            {
                _logger.LogInformation($"Executing {MethodBase.GetCurrentMethod().Name} Method Of Class: { this.GetType().Name}");

                IEnumerable<DiaryNoteDto> diaryNotes = await _diaryService.Value.GetUserDiaryNotes(userId);

                if (diaryNotes == null || !diaryNotes.Any())
                {
                    return Ok("No diary notes found for this user");
                }

                return Ok(
                    new
                    {
                        DiaryNotes = diaryNotes
                    });
            }
            catch (InvalidUserIdException ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}