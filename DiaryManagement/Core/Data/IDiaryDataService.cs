using Core.Models.Diary;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Data
{
    public interface IDiaryDataService
    {
        /// <summary>
        /// Creates Diary note for user
        /// </summary>
        /// <param name="createDiaryNoteDto"></param>
        /// <returns></returns>
        Task<bool> CreateDiaryNote(CreateDiaryNoteDto createDiaryNoteDto);

        /// <summary>
        /// Returns Users diary notes
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        Task<IEnumerable<DiaryNoteDto>> GetUserDiaryNotes(int UserId);
    }
}
