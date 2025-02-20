using DocumentFormat.OpenXml.Office2010.Excel;
using Domain.Entity.TeacherTab;

namespace API.DTOs.TeacherTabs;

public class FullTeacherTabDto(
    Guid id,
    Guid ClassId,
    string Theme,
    int HourCount)
{
        public static FullTeacherTabDto FromDomainModel(TeacherTab teacherTab)
            => new(
                teacherTab.Id.Value,
                teacherTab.ClassId.Value,
                teacherTab.ClassTheme,
                teacherTab.HoursCount
            );
}
