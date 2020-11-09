using System;
using System.Collections.Generic;
using System.Text;

namespace Kartrider.API.Tests
{
    class MetadataSingleton
    {
        public static Metadata Metadata { get; private set; }
        static MetadataSingleton()
        {
            Metadata = new Metadata(true);
        }
    }
}
