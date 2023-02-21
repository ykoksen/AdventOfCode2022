using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Day6
{
    public static class Reader
    {
        public static async Task<string> Read()
        {
            var reader = InputLoader.LoadReader(6);

            var input = await reader.ReadToEndAsync();

            for (int i = 13; i < input.Length; i++)
            {
                if (input[(i-13)..(i+1)].Distinct().Count() == 14)
                {
                    return (i+1).ToString();
                }
            }

            return "Not found";
        }
    }
}
