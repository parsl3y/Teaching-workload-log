using Application.Common.Interfaces.Queries;
using Domain.Entity.TeacherTab;
using OfficeOpenXml;
using Optional.Unsafe;

namespace Application.Commands
{
    public class SaveTeacherTabToExcelCommand
    {
        private readonly ITeacherTabQuery _teacherTabRepository;
        private readonly IClassQuery _classRepository;

        public SaveTeacherTabToExcelCommand(ITeacherTabQuery teacherTabRepository, IClassQuery classRepository)
        {
            _teacherTabRepository = teacherTabRepository;
            _classRepository = classRepository;
        }

        public async Task ExecuteAsync(TeacherTabId teacherTabId, string excelFilePath, CancellationToken cancellationToken)
        {
            var teacherTabOption = await _teacherTabRepository.GetById(teacherTabId, cancellationToken);
            if (!teacherTabOption.HasValue)
            {
                throw new Exception("TeacherTab not found");
            }
            
            var teacherTab = teacherTabOption.ValueOrFailure();
            var classOption = await _classRepository.GetById(teacherTab.ClassId, cancellationToken);
            if (!classOption.HasValue)
            {
                throw new Exception("Class not found");
            }

            var classEntity = classOption.ValueOrFailure();
            var allTeacherTabs = await _teacherTabRepository.GetByClassName(classEntity.ClassName, cancellationToken);

            var sortedClasses = (await Task.WhenAll(
                allTeacherTabs.Select(async t => await _classRepository.GetById(t.ClassId, cancellationToken))
            ))
            .Where(opt => opt.HasValue)
            .Select(opt => opt.ValueOrFailure())
            .Distinct()
            .OrderBy(c => c.ClassNumberToday)
            .ToList();

            using (var package = new ExcelPackage(new FileInfo(excelFilePath)))
            {
                var worksheet = package.Workbook.Worksheets.Add("TeacherTab");

                worksheet.Cells[1, 1].Value = "Дисципліна";
                worksheet.Cells[1, 2].Value = "Тема";
                worksheet.Cells[1, 3].Value = "Кількість годин";
                worksheet.Cells[1, 4].Value = "Дата";

                int row = 2;
                foreach (var relatedClass in sortedClasses)
                {
                    var relatedTabs = allTeacherTabs.Where(t => t.ClassId == relatedClass.Id).ToList();
                    
                    foreach (var tab in relatedTabs)
                    {
                        worksheet.Cells[row, 1].Value = relatedClass.ClassName;
                        worksheet.Cells[row, 2].Value = tab.ClassTheme;
                        worksheet.Cells[row, 3].Value = tab.HoursCount;
                        worksheet.Cells[row, 4].Value = relatedClass.ClassDate.GetDateTimeFormats();
                        row++;
                    }
                }
                
                await package.SaveAsync(cancellationToken);
            }
        }
    }
}
