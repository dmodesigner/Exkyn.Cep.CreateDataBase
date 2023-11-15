using Aspose.Cells;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

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

		foreach (var file in files)
		{
			Console.WriteLine($"Inserindo os registros na tabela {file.Replace(".xlsx", "")}.");

			var dt = ConvertExcelInDataTable(file);

			InsertIntoInBase(dt);
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

		void InsertIntoInBase(DataTable dt)
		{
			using (TransactionScope transectionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { Timeout = TimeSpan.FromMinutes(5) }))
			{
				SqlConnection connection = new SqlConnection(Environment.GetEnvironmentVariable("ConnectionStrings:CepBrasil"));
				connection.Open();

				using (SqlBulkCopy sql = new SqlBulkCopy(connection))
				{
					sql.DestinationTableName = dt.TableName;
					sql.WriteToServer(dt);
				}

				transectionScope.Complete();
				connection.Close();
			}
		}
	}
}