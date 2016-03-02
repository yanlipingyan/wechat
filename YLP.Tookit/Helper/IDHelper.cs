using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YLP.Tookit.Helper
{
    public class IDHelper
    {
        public static string Id32 { get { return Guid.NewGuid().ToString("N"); } }
    }
}
