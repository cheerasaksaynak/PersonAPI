using Microsoft.EntityFrameworkCore;
using PersonAPI.Entities;
using PersonAPI.Models;
using Gridify;

namespace PersonAPI.Services
{
    public class PersonService
    {
        private readonly IDbContextFactory<PersonContext> _contextFactory;
        public PersonService(IDbContextFactory<PersonContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        /// <summary>
        /// ดึงข้อมูลบุคคลทั่วไป
        /// </summary>
        public async Task<Paging<PersonBasicInfo>> GetPersonBasicInfo(GridifyQuery gridifyQuery)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var query = context.PersonBasicInfo.AsQueryable();

                return await Task.FromResult(query.Gridify(gridifyQuery));
            }
        }

        /// <summary>
        /// ดึงข้อมูลโปรไฟล์
        /// </summary>
        public async Task<PersonProfileInfo?> GetPersonProfileInfo(string accessToken)
        {
            //อ่าน id ของบุคคลจาก Access Token
            string personId = Utility.GetValueFromToken(accessToken, "person_id");

            if (string.IsNullOrEmpty(personId))
                return null;

            using (var context = _contextFactory.CreateDbContext())
            {
                return await context.PersonProfileInfo.Where(p => p.Id == int.Parse(personId)).FirstOrDefaultAsync();
            }
        }

        /// <summary>
        /// ดึงข้อมูลเงินเดือนบุคคล
        /// </summary>
        public async Task<Paging<PersonSalaryInfo>> GetPersonSalaryInfo(string scopes, GridifyQuery gridifyQuery)
        {
            using (var context = _contextFactory.CreateDbContext())
            {
                var query = context.PersonSalaryInfo.AsQueryable();

                //รายการข้อมูลที่สามารถเข้าถึงได้ ตาม Scopes
                var queryScoped = Utility.ApplyScopes(query, scopes);

                return await Task.FromResult(queryScoped.Gridify(gridifyQuery));
            }
        }

    }
}
