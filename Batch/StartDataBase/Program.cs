using Aspose.Cells;
using StartDataBase.Repositories;
using System.Data;

internal class Program
{
	private static void Main(string[] args)
	{
		var files = new List<string>
		{
			"Estado.xlsx",
			"Cidade.xlsx",
			"Bairro.xlsx",
			"Endereço.xlsx"
		};

		var insertRepository = new InsertRepository();

		foreach (var file in files)
		{
			Console.WriteLine($"Inserindo os registros na tabela {file.Replace(".xlsx", "")}.");

			var dt = ConvertExcelInDataTable(file);


			insertRepository.InsertInDataBase(dt);
		}

		Console.WriteLine("Todos os registros foram inseridos com sucesso.");

		string GetPath()
		{
			var pathAbsolute = Path.GetFullPath("Program.cs");

			var array = pathAbsolute.Split("CreateBase");

			return string.Concat(array[0], "CreateBase\\", "Files\\");

		}

		DataTable ConvertExcelInDataTable(string file)
		{
			Workbook excel = new Workbook(GetPath() + file);

			if (excel == null)
				throw new ArgumentException("O arquivo da planilha importada não foi encontrada.");

			Worksheet sheet = excel.Worksheets[0];

			if (sheet == null)
				throw new ArgumentException("Não foi encontrado nenhuma aba na planilha.");

			DataTable dt = sheet.Cells.ExportDataTable(0, 0, sheet.Cells.MaxRow + 1, sheet.Cells.MaxColumn + 1, true);
			dt.TableName = sheet.Name;

			return dt;
		}
	}
}