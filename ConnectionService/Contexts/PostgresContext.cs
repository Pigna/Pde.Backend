

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

    public void GetTablesAndColumns()
    {
        throw new NotImplementedException();
    }

    public void export()
    {
    }
}