using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactWeb.Models;

namespace ReactWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentInfoDetailController : ControllerBase
    {

        private readonly StudentInfoDetailContext _studentInfoDetailContext;


        public StudentInfoDetailController(StudentInfoDetailContext studentInfoDetailContext)
        {
            _studentInfoDetailContext = studentInfoDetailContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentInfoDetail>>> GetStudentInfoDetail()
        {
            if (_studentInfoDetailContext.StudentInfoDetail == null)
            {
                return NotFound();
            }
            return await _studentInfoDetailContext.StudentInfoDetail.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudentInfoDetail>> GetStudentInfoDetail(long id)
        {
            if (_studentInfoDetailContext.StudentInfoDetail == null)
            {
                return NotFound();
            }
            var studentInfoDetail = await _studentInfoDetailContext.StudentInfoDetail.FindAsync(id);
            if (studentInfoDetail == null)
            {
                return NotFound();
            }
            return studentInfoDetail;
        }

        [HttpPost]
        public async Task<ActionResult<StudentInfoDetail>> PostStudentInfoDetail(StudentInfoDetail studentInfoDetail)
        {
            _studentInfoDetailContext.StudentInfoDetail.Add(studentInfoDetail);
            await _studentInfoDetailContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStudentInfoDetail), new { id = studentInfoDetail.ID }, studentInfoDetail);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutStudentInfoDetail(long id, StudentInfoDetail StudentInfoDetail)
        {
            if (id != StudentInfoDetail.ID)
            {
                return BadRequest();
            }

            _studentInfoDetailContext.Entry(StudentInfoDetail).State = EntityState.Modified;
            try
            {
                await _studentInfoDetailContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStudentInfoDetail(long id)
        {
            if (_studentInfoDetailContext.StudentInfoDetail == null)
            {
                return NotFound();
            }
            var studentInfoDetail = await _studentInfoDetailContext.StudentInfoDetail.FindAsync(id);
            if (studentInfoDetail == null)
            {
                return NotFound();
            }
            _studentInfoDetailContext.StudentInfoDetail.Remove(studentInfoDetail);
            await _studentInfoDetailContext.SaveChangesAsync();
            return Ok();


        }
    }
}

