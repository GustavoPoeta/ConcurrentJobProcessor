using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;

namespace ConcurrentJobProcessor.Helpers
{
    public class CategoryConverter : DefaultTypeConverter
    {
        public override object ConvertFromString(
            string? text,
            IReaderRow row,
            MemberMapData memberMapData)
        {
            if (string.IsNullOrWhiteSpace(text))
                return new List<string>();

            return text
                .Split('|')
                .Select(x => x.Trim())
                .ToList();
        }
    }
}
