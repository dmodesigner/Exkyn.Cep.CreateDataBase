using System.Data.SqlClient;
using System.Data;
using System.Transactions;

namespace StartDataBase.Repositories
{
	public class InsertRepository : ConnectionStrings
	{
		public void InsertInDataBase(DataTable dt)
		{
			using (TransactionScope transectionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { Timeout = TimeSpan.FromMinutes(5) }))
			{
				SqlConnection connection = new SqlConnection(Base);
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
