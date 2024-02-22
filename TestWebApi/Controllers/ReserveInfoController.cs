using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestWebApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReserveInfoController : ControllerBase
    {
        private readonly TestWebApiContext _context;

        public ReserveInfoController(TestWebApiContext context)
        {
            _context = context;
        }


        // GET: api/Reserve
        [HttpGet]
        public ActionResult<IEnumerable<ReserveInfo>> Get()
        {
            return _context.ReserveInfo.ToList();
        }

        // GET api/Reserve/5
        [HttpGet("{ReserveID}")]
        public ActionResult<ReserveInfo> Get(string ReserveID)
        {
            var result = _context.ReserveInfo.FirstOrDefault(x => x.ReserveID == ReserveID);
            if (result == null)
            {
                return NotFound("查無資料，請確認是否輸入正確預約編號");
            }
            return result;
        }

        // POST api/Reserve
        [HttpPost]
        public ActionResult<ReserveInfo> Post([FromBody] ReserveInfo value)
        {
            value.ID = Guid.NewGuid();
            value.CreateTime = DateTime.Now;
            value.UpdateTime = DateTime.Now;
            _context.ReserveInfo.Add(value);
            _context.SaveChanges();
            var minSIDReserveInfo = _context.ReserveInfo.Where(x => x.ReserveDate == value.ReserveDate).OrderBy(x => x.SID).FirstOrDefault();
            var currentSID = value.SID;
            string defaultReserveID = string.Empty;
            if (minSIDReserveInfo != null && minSIDReserveInfo.SID == currentSID)
            {
                defaultReserveID = "000001";
            }
            else if(minSIDReserveInfo != null && minSIDReserveInfo.SID != currentSID)
            {
                defaultReserveID = (value.SID - minSIDReserveInfo.SID).ToString().PadLeft(6, '0');
            }
            if (!string.IsNullOrWhiteSpace(defaultReserveID))
            {
                value.ReserveID = string.Format("{0}{1}", value.ReserveDate.ToString("yyyyMMdd"), defaultReserveID);
            }

            _context.Update(value).Property(x => x.SID).IsModified = false;
            _context.SaveChanges();
            return CreatedAtAction(nameof(Get), new { ReserveID = value.ReserveID }, value);
        }

        // PUT api/Reserve/5
        [HttpPut("{ReserveID}")]
        public IActionResult Put(string ReserveID, [FromBody] ReserveInfo value)
        {
            if (ReserveID != value.ReserveID)
            {
                return BadRequest();
            }
            var entity = _context.ReserveInfo.FirstOrDefault(x => x.ReserveID == value.ReserveID);
            entity.ReserveUserName = value.ReserveUserName;
            entity.ReserveUserPhone = value.ReserveUserPhone;
            entity.NumberOfPeople = value.NumberOfPeople;
            entity.ReserveDate= value.ReserveDate;
            entity.UpdateTime = DateTime.Now;
            _context.Update(entity).Property(x => x.SID).IsModified = false;
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (!_context.ReserveInfo.Any(x => x.ReserveID == ReserveID))
                {
                    return NotFound();
                }
                else
                {
                    return StatusCode(500, "存取發生錯誤");
                }
            }
            return NoContent();
        }

        // DELETE api/Reserve/5
        [HttpDelete("{ReserveID}")]
        public IActionResult Delete(string ReserveID)
        {
            var entity = _context.ReserveInfo.FirstOrDefault(x => x.ReserveID == ReserveID);
            if(entity == null)
            {
                return NotFound();
            }
            _context.ReserveInfo.Remove(entity);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
