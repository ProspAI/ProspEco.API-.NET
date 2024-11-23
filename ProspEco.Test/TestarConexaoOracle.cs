using Oracle.ManagedDataAccess.Client; // Certifique-se de que este pacote está instalado

static void TestarConexaoOracle(string connectionString)
{
    try
    {
        using (var connection = new OracleConnection(connectionString))
        {
            connection.Open(); // Tenta abrir a conexão
            Console.WriteLine("Conexão com o Oracle foi bem-sucedida!");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro ao conectar ao Oracle: {ex.Message}");
    }
}