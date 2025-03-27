using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet_Pr2
{
    class DownlowdDataFromAPI
    {
        public HttpClient client;
        public async Task<string> GetData(string call)
        {
            client = new HttpClient();
            string response = await client.GetStringAsync(call);
            return response;
        }
    }
}
