using Core.CustomExceptions;
using Core.Data;
using Core.Models.Diary;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace Core.Services
{
    public class DiaryService : IDiaryService
    {
        private readonly IDiaryDataService _diaryDataService;

        /// <summary>
        /// Constructor for injecting dependencies and configuration
        /// </summary>
        /// <param name="diaryDataService"></param>
        public DiaryService(IDiaryDataService diaryDataService)
        {
            _diaryDataService = diaryDataService ?? throw new ArgumentNullException(nameof(diaryDataService));
        }

        /// <summary>
        /// Creates Diary note for user
        /// </summary>
        /// <param name="createDiaryNoteDto"></param>
        /// <returns></returns>
        public async Task<bool> CreateDiaryNote(CreateDiaryNoteDto createDiaryNoteDto)
        {
            if (createDiaryNoteDto.UserId <= 0)
            {
                throw new InvalidUserIdException("User Id is not valid");
            }

            if(createDiaryNoteDto.NoteDate.Date < DateTime.Now.Date)
            {
                throw new MinimumDateException("Cannot create dairy note for past date");
            }

            return await _diaryDataService.CreateDiaryNote(createDiaryNoteDto);
        }

        /// <summary>
        /// Returns Users diary notes
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<DiaryNoteDto>> GetUserDiaryNotes(int UserId)
        {
            if (UserId <= 0)
            {
                throw new InvalidUserIdException("User Id is not valid");
            }

            return await _diaryDataService.GetUserDiaryNotes(UserId);
        }
    }
}
