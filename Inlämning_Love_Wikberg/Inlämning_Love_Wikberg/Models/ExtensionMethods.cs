using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Inlämning_Love_Wikberg.Models
{
    public static class ExtensionMethods
    {
        public static FileInfo GetMostRecentlyCreatedFile(this FileInfo[] directory)
        {
            var mostRecentFile = directory.OrderBy(f => f.CreationTime).Last();
            return mostRecentFile;
        }
    }
}
