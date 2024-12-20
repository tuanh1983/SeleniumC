public class Config
{
    public Dictionary<string, ModuleConfig> Modules { get; set; }
}

public class ModuleConfig
{
    public string BaseUrl { get; set; }
    public string Browser { get; set; }
    public int Timeout { get; set; }
    public Credentials Credentials { get; set; }
}

public class Credentials
{
    public string Username { get; set; }
    public string Password { get; set; }
}
