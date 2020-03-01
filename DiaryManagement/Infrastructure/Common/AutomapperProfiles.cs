using AutoMapper;
using Core.Models.Diary;
using Infrastructure.Models;

namespace Infrastructure.Common
{
    /// <summary>
    /// Used to create profile for different mappings
    /// </summary>
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles()
        {
            // Custom mappings of Infrastucture models to Core Models

            CreateMap<CreateDiaryNoteDto, CreateDiaryNote>();

            // It will work for list also
            CreateMap<CreateDiaryNote, DiaryNoteDto>()
                .ForMember(dest => dest.NoteTitle, opt => opt.MapFrom(src => src.Title))
                .ForMember(dest => dest.NoteDescription, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.NoteDate, opt => opt.MapFrom(src => src.NoteDate))
                .ReverseMap();
        }
    }
}
