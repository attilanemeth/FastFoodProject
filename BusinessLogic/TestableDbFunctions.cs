using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public static class TestableDbFunctions
    {
        [DbFunction("Edm", "TruncateTime")]
        public static DateTime? TruncateTime(DateTime? dateValue)
        {
            return dateValue?.Date;
        }
    }
}
