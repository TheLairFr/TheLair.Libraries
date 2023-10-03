namespace TheLair.JWT;

public class JWTConfig
{
    public string Issuer;
    public string Audience;
    public string Key;
    public int MinutesOfValidity = 60;

    public JWTConfig(string issuer, string audience, string key)
    {
        Issuer = issuer;
        Audience = audience;
        Key = key;
    }
}