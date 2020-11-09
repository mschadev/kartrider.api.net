using System;
using System.Collections.Generic;
using System.Text;

namespace Kartrider.API.Tests
{
    class KartAPISingleton
    {
        public static KartAPI KartAPI { get; private set; }
        static KartAPISingleton()
        {
            KartAPI = new KartAPI(Define.API_KEY);
        }
    }
}
