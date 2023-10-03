using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TheLair.BlazorApp.Helpers;

public static class JWTHelper
{
    public static TFrame? GetTokenContent<TFrame>(string token)
    {
        try
        {
            string base64String = token.Split('.')[1];
            int length = base64String.Length % 4;

            base64String = length switch
            {
                2 => $"{base64String}==",
                3 => $"{base64String}=",
                _ => base64String
            };

            byte[] data = Convert.FromBase64String(base64String);
            string decodedString = Encoding.UTF8.GetString(data);

            return (JsonConvert.DeserializeObject<TFrame>(decodedString));
        }
        catch
        {
            // ignored
        }

        return (default);
    }
}