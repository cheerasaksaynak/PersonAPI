using Microsoft.AspNetCore.Mvc;
using PersonAPI.Entities;
using PersonAPI.Services;
using Gridify;

namespace PersonAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly PersonService _personService;
        public PersonController(PersonService personService)
        {
            _personService = personService;
        }

        /// <summary>
        /// ข้อมูลบุคคลทั่วไป (ตัวอย่าง API Level 1)
        /// </summary>
        [HttpGet("L1/Basic")]
        public async Task<IActionResult> GetPersonBasicInfoFilter([FromQuery] GridifyQuery gridifyQuery)
        {
            var isValid = gridifyQuery.IsValid<PersonBasicInfo>();
            if (!isValid)
            {
                return StatusCode(400, "OrderBy or Filter is invalid.");
            }

            try
            {
                var personList = await _personService.GetPersonBasicInfo(gridifyQuery);
                if (personList != null && personList.Count > 0)
                {
                    return Ok(personList);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            return StatusCode(404, "Data not found.");
        }

        /// <summary>
        /// ข้อมูลโปรไฟล์ (ตัวอย่าง API Level 2)
        /// </summary>
        [HttpGet("L2/Profile")]
        public async Task<IActionResult> GetPersonProfileInfo([FromHeader(Name = "token")] string? accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                return StatusCode(400, "Access Token not found.");
            }

            try
            {
                var person = await _personService.GetPersonProfileInfo(accessToken);
                if (person != null)
                {
                    return Ok(person);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            return StatusCode(404, "Data not found.");
        }

        /// <summary>
        /// ข้อมูลเงินเดือน (ตัวอย่าง Level 3)
        /// </summary>
        [HttpGet("L3/Salary")]
        public async Task<IActionResult> GetPersonSalaryInfoFilter([FromHeader(Name = "scopes")] string? scopes, [FromQuery] GridifyQuery gridifyQuery)
        {
            if (string.IsNullOrEmpty(scopes))
            {
                return StatusCode(400, "Please specific data scope.");
            }

            var isValid = gridifyQuery.IsValid<PersonSalaryInfo>();
            if (!isValid)
            {
                return StatusCode(400, "OrderBy or Filter is invalid.");
            }

            try
            {
                var personList = await _personService.GetPersonSalaryInfo(scopes, gridifyQuery);
                if (personList != null && personList.Count > 0)
                {
                    return Ok(personList);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

            return StatusCode(404, "Data not found.");
        }

    }
}
