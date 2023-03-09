namespace ConnectionService.Models;

public class ConnectionInfo
{
    public ConnectionInfo(string username, string password, string host, string port, string database)
    {
        Username = username;
        Password = password;
        Host = host;
        Port = port;
        Database = database;
    }

    public string Username { get; set; }
    public string Password { get; set; }
    public string Host { get; set; }
    public string Port { get; set; }
    public string Database { get; set; }
}