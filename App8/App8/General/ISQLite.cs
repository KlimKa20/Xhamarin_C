using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App8
{
    public interface ISQLite
    {
        string GetDatabasePath(string filename);
    }
}
