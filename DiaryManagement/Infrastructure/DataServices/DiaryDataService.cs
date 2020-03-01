using Core.Data;
using Core.Models.Diary;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using AutoMapper;
using System;
using Infrastructure.Models;
using System.Reflection;
using Microsoft.Extensions.Logging;
using Infrastructure.Data;

namespace Infrastructure.DataServices
{
    public class DiaryDataService : IDiaryDataService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<DiaryDataService> _logger;
        private readonly DataContext _context;

        public DiaryDataService(IMapper mapper, ILogger<DiaryDataService> logger, DataContext context)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public static Dictionary<int, CreateDiaryNote> DiaryNotes = new Dictionary<int, CreateDiaryNote>();
        public static int diaryNoteKey = 1;

        /// <summary>
        /// Creates Diary note for user
        /// </summary>
        /// <param name="createDiaryNoteDto"></param>
        /// <returns></returns>
        public async Task<bool> CreateDiaryNote(CreateDiaryNoteDto createDiaryNoteDto)
        {
            _logger.LogInformation($"Executing {MethodBase.GetCurrentMethod().Name} Method Of Class: { this.GetType().Name}");

            // Map to database model
            CreateDiaryNote diaryNote = _mapper.Map<CreateDiaryNote>(createDiaryNoteDto);

            // Add to actual database using context
            DiaryNotes.Add(diaryNoteKey, diaryNote);
            diaryNoteKey++;

            return true;
        }

        /// <summary>
        /// Returns Users diary notes
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<DiaryNoteDto>> GetUserDiaryNotes(int UserId)
        {
            var diaryNotes = DiaryNotes?.Where(kvp => kvp.Value.UserId == UserId)?.Select(kvp => kvp.Value);

            return _mapper.Map<IEnumerable<DiaryNoteDto>>(diaryNotes);
        }
    }
}
