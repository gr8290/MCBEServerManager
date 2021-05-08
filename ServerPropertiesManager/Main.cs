using ServerPropertiesManager.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerPropertiesManager
{
    public class Main
    {
        IDictionary<string, string> _Properties = null;

        public Main(string propertiesFilePath)
        {
            using (TextReader reader = File.OpenText(propertiesFilePath))
            {
                _Properties = PropertiesLoader.Load(reader);
            }
        }

        public IDictionary<string, string> GetProperties()
        {
            return _Properties;
        }

  

    }
}
