namespace StartDataBase.Repositories
{
	public abstract class ConnectionStrings
	{
		protected string? Base { get => Environment.GetEnvironmentVariable("ConnectionStrings:CepBrasil"); }
	}
}
