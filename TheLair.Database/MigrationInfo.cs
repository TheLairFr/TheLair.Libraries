using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLair.Database;

public class MigrationInfo<TClass> where TClass : BDDContext
{
    public bool Migated;
}