using Newtonsoft.Json;

namespace TheLair.JWT;

public class JWTFrame
{
    [JsonProperty("nbf")]
    public int NotValidBefore { get; set; }

    [JsonProperty("exp")]
    public int Expiration { get; set; }

    [JsonProperty("iat")]
    public int IssuedAt { get; set; }
}