using System.Data.SqlClient;
using System.Transactions;

namespace StartDataBase.Repositories
{
	public class CreateBaseRepository : ConnectionStrings
	{
		private string Drop()
		{
			return @"IF OBJECT_ID('dbo.Adresses', 'U') IS NOT NULL
					BEGIN
						DROP TABLE dbo.Adresses
					END

					IF OBJECT_ID('dbo.Neighborhoods', 'U') IS NOT NULL
					BEGIN
						DROP TABLE dbo.Neighborhoods
					END

					IF OBJECT_ID('dbo.Cities', 'U') IS NOT NULL
					BEGIN
						DROP TABLE dbo.Cities
					END

					IF OBJECT_ID('dbo.States', 'U') IS NOT NULL
					BEGIN
						DROP TABLE dbo.States
					END";
		}

		private string Create()
		{
			return @"SET ANSI_NULLS ON

					SET QUOTED_IDENTIFIER ON

					CREATE TABLE [dbo].[Adresses](
						[AddressID] [int] IDENTITY(1,1) NOT NULL,
						[NeighborhoodID] [int] NOT NULL,
						[CityID] [int] NOT NULL,
						[StateID] [int] NOT NULL,
						[ZipCode] [char](8) NULL,
						[Address] [nvarchar](150) NULL,
						CONSTRAINT [PK_Endereco] PRIMARY KEY CLUSTERED 
					(
						[AddressID] ASC
					)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
					) ON [PRIMARY]

					SET ANSI_NULLS ON

					SET QUOTED_IDENTIFIER ON

					CREATE TABLE [dbo].[Cities](
						[CityID] [int] IDENTITY(1,1) NOT NULL,
						[StateID] [int] NOT NULL,
						[ZipCode] [char](8) NULL,
						[City] [varchar](100) NOT NULL,
						[Capital] [bit] NOT NULL,
						CONSTRAINT [PK_Cidade] PRIMARY KEY CLUSTERED 
					(
						[CityID] ASC
					)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
					) ON [PRIMARY]

					SET ANSI_NULLS ON

					SET QUOTED_IDENTIFIER ON

					CREATE TABLE [dbo].[Neighborhoods](
						[NeighborhoodID] [int] IDENTITY(1,1) NOT NULL,
						[CityID] [int] NOT NULL,
						[StateID] [int] NOT NULL,
						[Neighborhood] [varchar](100) NOT NULL,
						CONSTRAINT [PK_Bairro] PRIMARY KEY CLUSTERED 
					(
						[NeighborhoodID] ASC
					)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
					) ON [PRIMARY]

					SET ANSI_NULLS ON

					SET QUOTED_IDENTIFIER ON

					CREATE TABLE [dbo].[States](
						[StateID] [int] IDENTITY(1,1) NOT NULL,
						[FU] [char](2) NOT NULL,
						[State] [varchar](50) NOT NULL,
						CONSTRAINT [PK_Estado] PRIMARY KEY CLUSTERED 
					(
						[StateID] ASC
					)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
					) ON [PRIMARY]

					ALTER TABLE [dbo].[Adresses]  WITH NOCHECK ADD  CONSTRAINT [FK_Adresses_Cities] FOREIGN KEY([CityID])
					REFERENCES [dbo].[Cities] ([CityID])

					ALTER TABLE [dbo].[Adresses] CHECK CONSTRAINT [FK_Adresses_Cities]

					ALTER TABLE [dbo].[Adresses]  WITH NOCHECK ADD  CONSTRAINT [FK_Adresses_Neighborhoods] FOREIGN KEY([NeighborhoodID])
					REFERENCES [dbo].[Neighborhoods] ([NeighborhoodID])

					ALTER TABLE [dbo].[Adresses] CHECK CONSTRAINT [FK_Adresses_Neighborhoods]

					ALTER TABLE [dbo].[Adresses]  WITH NOCHECK ADD  CONSTRAINT [FK_Adresses_States] FOREIGN KEY([StateID])
					REFERENCES [dbo].[States] ([StateID])

					ALTER TABLE [dbo].[Adresses] CHECK CONSTRAINT [FK_Adresses_States]

					ALTER TABLE [dbo].[Cities]  WITH NOCHECK ADD  CONSTRAINT [FK_Cities_States] FOREIGN KEY([StateID])
					REFERENCES [dbo].[States] ([StateID])

					ALTER TABLE [dbo].[Cities] CHECK CONSTRAINT [FK_Cities_States]

					ALTER TABLE [dbo].[Neighborhoods]  WITH NOCHECK ADD  CONSTRAINT [FK_Neighborhoods_Cities] FOREIGN KEY([CityID])
					REFERENCES [dbo].[Cities] ([CityID])

					ALTER TABLE [dbo].[Neighborhoods] CHECK CONSTRAINT [FK_Neighborhoods_Cities]

					ALTER TABLE [dbo].[Neighborhoods]  WITH NOCHECK ADD  CONSTRAINT [FK_Neighborhoods_States] FOREIGN KEY([StateID])
					REFERENCES [dbo].[States] ([StateID])

					ALTER TABLE [dbo].[Neighborhoods] CHECK CONSTRAINT [FK_Neighborhoods_States]";
		}

		public void Start()
		{
			using (TransactionScope transectionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { Timeout = TimeSpan.FromMinutes(5) }))
			{
				SqlConnection connection = new SqlConnection(Base);

				connection.Open();

				SqlCommand commandDrop = new SqlCommand(Drop().Trim(), connection);
				commandDrop.ExecuteNonQuery();

				SqlCommand commandCreate = new SqlCommand(Create().Trim(), connection);
				commandCreate.ExecuteNonQuery();

				commandDrop.Dispose();
				commandCreate.Dispose();

				transectionScope.Complete();
				connection.Close();
			}
		}

		public void End()
		{
			using (TransactionScope transectionScope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { Timeout = TimeSpan.FromMinutes(5) }))
			{
				SqlConnection connection = new SqlConnection(Base);

				connection.Open();

				var query = @"UPDATE Adresses SET ZipCode = REPLICATE('0',8 - LEN(CONVERT(VARCHAR,ZipCode))) + CONVERT(VARCHAR,ZipCode);
								UPDATE Cities SET ZipCode = REPLICATE('0',8 - LEN(CONVERT(VARCHAR,ZipCode))) + CONVERT(VARCHAR,ZipCode) WHERE ZipCode IS NOT NULL;

								CREATE INDEX idx_Adresses_ZipCode ON Adresses (ZipCode);
								CREATE INDEX idx_Cities_ZipCode ON Cities (ZipCode);";

				SqlCommand commandCreate = new SqlCommand(query.Trim(), connection);
				commandCreate.ExecuteNonQuery();

				commandCreate.Dispose();

				transectionScope.Complete();
				connection.Close();
			}
		}
	}
}
