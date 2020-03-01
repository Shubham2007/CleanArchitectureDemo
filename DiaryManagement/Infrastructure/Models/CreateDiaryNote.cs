using System;

namespace Infrastructure.Models
{
    /// <summary>
    /// Used to insert Diary note into the database
    /// </summary>
    public class CreateDiaryNote
    {
        public int UserId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime NoteDate { get; set; }

        public bool IsImportant { get; set; }
    }
}
