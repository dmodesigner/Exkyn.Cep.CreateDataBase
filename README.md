
# Cep Brasil - Criação da Base de Dados

Esse projeto realiza a criação da base de dados e aplica a carga para as tabelas relacionadas ao CEP do Brasil. 
## Configurando a Conexão com a Base de Dados

Esse projeto faz uso de variáveis de ambiente para uso da conexão com o banco de dados.

Para executar esse projeto você pode criar sua Connection String em uma variável de ambiente chamada `ConnectionStrings:CepBrasil` e após configurar a variável de ambiente corretamente poderá executar o projeto sem ter que realizar nenhuma alteração.

Caso prefira usar a Connection String configurando o projeto, você poderá acessar a propriedade da conexão dentro da classe `ConnectionString` localizada na pasta `Repositories`.


## Executando o Projeto

Para executar o projeto você precisara:
- SQL Server da Microsoft
- Editor de códigos com suporte ao C# e .NET (VS Code, Visual Studio e etc.)
- .NET SDK em sua última versão oficial

Após seguir os passos descrito na seção **Configurando a Conexão com a Base de Dados** basta rodar o projeto que o mesmo irá criar as tabelas necessárias e alimentar as mesmas com as informações deixando a base de dados pronta para uso em pouco tempo.
## Autor

Esse projeto foi criado por [Daniel Moura](https://github.com/dmodesigner/)
## Licença

Esse projeto é oferecido sobre uso da licença MIT. Sendo livre seu uso pessoal ou comercial.

Sendo oferecido sem garantias e de sua total responsabilidade seu uso.

Para maiores detalhes consulte o arquivo de [licença](https://github.com/dmodesigner/Exkyn.Cep/blob/main/LICENSE.txt).