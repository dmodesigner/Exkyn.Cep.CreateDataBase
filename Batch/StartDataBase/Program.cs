using StartDataBase.Helpers;
using StartDataBase.Repositories;

var insertRepository = new InsertRepository(); 

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

	var dt = ExcelHelper.ConvertExcelInDataTable(file);

	insertRepository.InsertInDataBase(dt);
}

Console.WriteLine("Todos os registros foram inseridos com sucesso.");