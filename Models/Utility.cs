using Microsoft.IdentityModel.JsonWebTokens;
using PersonAPI.Entities;

namespace PersonAPI.Models
{
    public static class Utility
    {
        public static string GetValueFromToken(string jwtString, string key)
        {
            if (!string.IsNullOrEmpty(jwtString))
            {
                var tokenHandler = new JsonWebTokenHandler();

                var jwtToken = tokenHandler.ReadJsonWebToken(jwtString);

                var result = jwtToken.Claims.FirstOrDefault(c => c.Type == key)?.Value;

                if (result != null)
                {
                    return result;
                }
            }

            return string.Empty;
        }

        public static IQueryable<PersonSalaryInfo> ApplyScopes(IQueryable<PersonSalaryInfo> query, string scopes)
        {
            IQueryable<PersonSalaryInfo> resultQuery = query.Where(p => false);

            string[] scopeItems = scopes.TrimEnd(';').Split(';');

            foreach (var scopeItem in scopeItems)
            {
                string[] scopeIds = scopeItem.Split('.');

                IQueryable<PersonSalaryInfo> scopeQuery = query;

                if (!string.IsNullOrEmpty(scopeIds[0]) && scopeIds[0] != "*")
                {
                    scopeQuery = scopeQuery.Where(p => p.CampusId == scopeIds[0]);
                }

                if (!string.IsNullOrEmpty(scopeIds[1]) && scopeIds[1] != "*")
                {
                    scopeQuery = scopeQuery.Where(p => p.FacId == scopeIds[1]);
                }

                if (!string.IsNullOrEmpty(scopeIds[2]) && scopeIds[2] != "*")
                {
                    scopeQuery = scopeQuery.Where(p => p.DeptId == scopeIds[2]);
                }

                resultQuery = resultQuery.Union(scopeQuery);
            }

            return resultQuery;
        }
    }
}

