using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Models.Diary
{
    /// <summary>
    /// Used to create user's diary note
    /// </summary>
    public class CreateDiaryNoteDto
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        [StringLength(maximumLength: 20, ErrorMessage = "Title cannot exceeds than 20 characters")]
        public string Title { get; set; }

        [Required]
        [StringLength(maximumLength: 50, ErrorMessage = "Description cannot exceeds than 100 characters")]
        public string Description { get; set; }

        [Required]
        [DataType(dataType: DataType.Date)]
        public DateTime NoteDate { get; set; }

        public bool IsImportant { get; set; } = false;
    }
}
