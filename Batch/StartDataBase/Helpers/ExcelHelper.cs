using Aspose.Cells;
using System.Data;

namespace StartDataBase.Helpers
{
	public static class ExcelHelper
	{
		private static void Validate(string file)
		{
			if (string.IsNullOrEmpty(file))
				throw new ArgumentNullException(nameof(file), "Você deve informar o nome do arquivo que deseja usar.");

			if (!File.Exists(PathHelper.GetPath() + file))
				throw new ArgumentException("$\"O arquivo {file} não foi encontrada.\"");
		}

		public static DataTable ConvertExcelInDataTable(string file)
		{
			Validate(file);

			Workbook excel = new Workbook(PathHelper.GetPath() + file);

			if (excel == null)
				throw new ArgumentException($"O arquivo {file} não possui nenhuma planilha ou é invalido.");

			Worksheet sheet = excel.Worksheets[0];

			if (sheet == null)
				throw new ArgumentException("Não foi encontrado nenhuma aba na planilha.");

			DataTable dt = sheet.Cells.ExportDataTable(0, 0, sheet.Cells.MaxRow + 1, sheet.Cells.MaxColumn + 1, true);
			dt.TableName = sheet.Name;

			return dt;
		}
	}
}
