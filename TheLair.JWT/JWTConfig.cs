namespace TheLair.JWT;

public class JWTConfig
{
    public string Issuer;
    public string Audience;
    public string Key;

    public JWTConfig(string issuer, string audience, string key)
    {
        Issuer = issuer;
        Audience = audience;
        Key = key;
    }
}