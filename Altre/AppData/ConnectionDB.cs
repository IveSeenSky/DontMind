using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Altre.AppData
{
    internal class ConnectionDB
    {
        public static AltrebaseEntities context;

        public static AltrebaseEntities GetCont()
        {
            if (context == null) 
                context = new AltrebaseEntities();
            return context;
        }
    }
}
