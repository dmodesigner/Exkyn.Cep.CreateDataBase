using StartDataBase.Helpers;
using StartDataBase.Repositories;

var insertRepository = new InsertRepository(); 
var createBaseRepository = new CreateBaseRepository();

var files = new List<string>
{
	"Estado.xlsx",
	"Cidade.xlsx",
	"Bairro.xlsx",
	"Endereço.xlsx"
};

Console.WriteLine("Criando as tabelas do banco de dados.");
createBaseRepository.Start();

foreach (var file in files)
{
	Console.WriteLine($"Inserindo os registros na tabela {file.Replace(".xlsx", "")}.");

	var dt = ExcelHelper.ConvertExcelInDataTable(file);

	insertRepository.InsertInDataBase(dt);
}

Console.WriteLine("Realizando os últimos ajustes.");
createBaseRepository.End();

Console.WriteLine("Todas as etapas foram realizadas com sucesso.");