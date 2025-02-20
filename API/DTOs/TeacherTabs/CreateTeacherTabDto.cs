using Domain.Entity.TeacherTab;

namespace API.DTOs.TeacherTabs;
public record CreateTeacherTabDto(
   Guid ClassId,
   string Theme,
   int HourCount)
{
    public static CreateTeacherTabDto FromDomainModel(TeacherTab teacherTab)
        => new(
            teacherTab.ClassId.Value,
            teacherTab.ClassTheme,
            teacherTab.HoursCount
            );
}