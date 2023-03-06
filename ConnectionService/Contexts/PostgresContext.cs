using System.Diagnostics;
using Npgsql;

namespace ConnectionService.Contexts;

public class PostgresContext : IDatabaseContext
{
    public void Connection()
    {
        string strConnString = "Server=remote_server;Port=5432;User Id=user;Password=mypassword;Database=test";
        try
        {
            NpgsqlConnection objpostgraceConn = new NpgsqlConnection(strConnString);
            objpostgraceConn.Open();
            string strpostgracecommand = "select employeeid , employeename , employeesalary  from employee";
            NpgsqlDataAdapter objDataAdapter = new NpgsqlDataAdapter(strpostgracecommand, objpostgraceConn);
            objpostgraceConn.Close();
        }
        catch (Exception ex)
        {
            //System.Windows.Forms.MessageBox.Show(ex.Message, "Error Occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    public void export()
    {
        var process = new Process();
        var startInfo = new ProcessStartInfo();
        startInfo.FileName = Path.Combine("PostgreSQL", "postgresql-backup.bat");
        var host = "localhost";
        var port = "5432";
        var user = "postgres";
        var database = "postgres";
        var outputPath = "C://";

// use pg_dump, specifying the host, port, user, database to back up, and the output path.
// the host, port, user, and database must be an exact match with what's inside your pgpass.conf (Windows)
        startInfo.Arguments = $@"{host} {port} {user} {database} ""{outputPath}""";
        startInfo.CreateNoWindow = true;
        startInfo.UseShellExecute = false;
        process.StartInfo = startInfo;
        process.Start();
        process.WaitForExit();
        process.Close();
    }
}