using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Kartrider.API.Tests
{
    class Define
    {
        private static string _apiKey;
        public static string API_KEY
        {
            get
            {
                return _apiKey;
            }
        }
        static Define()
        {
            if (File.Exists("Key.txt"))
            {
                _apiKey = File.ReadAllText("Key.txt");
            }
            else
            {
                _apiKey = Environment.GetEnvironmentVariable("UNIT_TEST_API_KEY");
            }
            
        } 
    }
}
