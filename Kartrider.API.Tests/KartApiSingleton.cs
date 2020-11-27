using System;
using System.Collections.Generic;
using System.Text;

namespace Kartrider.API.Tests
{
    class KartApiSingleton
    {
        public static KartApi KartApi { get; private set; }
        static KartApiSingleton()
        {
            KartApi = new KartApi(Define.API_KEY);
        }
    }
}
