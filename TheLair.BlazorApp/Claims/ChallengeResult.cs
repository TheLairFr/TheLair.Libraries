using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLair.BlazorApp.Claims;

public class ChallengeResult
{
    public bool Success;
    public string[] Errors = Array.Empty<string>();
}