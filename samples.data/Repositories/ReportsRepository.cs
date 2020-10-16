using Data.Models;
using Flurl;
using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class ReportsRepository : IReportsRepository
    {

        private string apikey = "1OxG06LaJo7dGV21Q4uqm7d98ur4Yrga4wsZhSKT";
        private string hmackey = "3QUYuNlO3Ui2DT/9UfArYoj7HXG9HqryWRVndaldcvA=";
        private string baseUrl = "https://vbt400uv2g.execute-api.us-west-2.amazonaws.com/Primary/ERP";

        private static string GenerateToken(string uri, long iat, string apiKey, string hmacKey)
        {
            uri = uri.Replace("/Primary", "");

            var payload = new JWTPayload(iat, uri, apiKey);

            string jwtToken = Jose.JWT.Encode(payload, Encoding.UTF8.GetBytes(hmacKey), Jose.JwsAlgorithm.HS256);

            return jwtToken;
        }

        public async Task<IQueryable<Association>> GetAssociations(int branchId)
        {
            var url = baseUrl.AppendPathSegment("associations").SetQueryParam("branchId", branchId).SetQueryParam("take", 1000);

            var uri = new UriBuilder(url).Uri.AbsolutePath;

            var token = GenerateToken(uri, DateTimeOffset.Now.ToUnixTimeSeconds(), apikey, hmackey);

            var execTime = DateTime.Now;
            Console.WriteLine($"EXECUTE:{url} - TIME: {execTime.ToShortTimeString()}");
            var associations = await url.WithHeader("Authorization", String.Format("Bearer {0}", token)).WithHeader("x-api-key", apikey).GetJsonAsync<List<Association>>();
            Console.WriteLine($"EXECUTE TIME:{(DateTime.Now - execTime).TotalSeconds} - REGISTERS: {associations.Count()}");

            return associations.AsQueryable();
        }

    }
}
