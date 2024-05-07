using dummy.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dummy.Controllers;

[ApiController]
[Route("Demo/{controller}")]
public class StudentController : ControllerBase
{
    private readonly StudentContext studentContext;
    public StudentController(StudentContext context)
    {
        studentContext = context;
    }
    [HttpGet("GetAll")]
    public async Task<ActionResult<IEnumerable<StudentShenbaORM>>> GetAllStudentDetials()
    {
        return await studentContext.StudentShenbaORM.ToListAsync();
    }

    [HttpGet("GetById/{id}")]
    public async Task<ActionResult<StudentShenbaORM>> GetParticularStudent(int id)
    {
        return await studentContext.StudentShenbaORM.FindAsync(id);
    }

    [HttpPost("AddNewStudent")]
    public async Task<ActionResult<StudentShenbaORM>> AddStudent(StudentShenbaORM student)
    {
        studentContext.StudentShenbaORM.Add(student);
        await studentContext.SaveChangesAsync();
        return CreatedAtAction("GetParticularStudent", new { id = student.StudentId }, student);
    }

    [HttpDelete("DeleteById/{id}")]
    public async Task<ActionResult<IEnumerable<StudentShenbaORM>>> DeleteStudent(int id)
    {
        var studentdata = await studentContext.StudentShenbaORM.FindAsync(id);
        if (studentdata == null)
        {
            return NotFound();
        }
        studentContext.StudentShenbaORM.Remove(studentdata);
        await studentContext.SaveChangesAsync();
        return await studentContext.StudentShenbaORM.ToListAsync();
        // return NoContent();
    }
    [HttpPut("Update/{id}")]
    public async Task<IActionResult> UpdateStudent(int id, StudentShenbaORM student)
    {
        if (id != student.StudentId)
        {
            return BadRequest();
        }

        studentContext.Entry(student).State = EntityState.Modified;

        try
        {
            await studentContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!StudentExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }
        return NoContent();
    }
    private bool StudentExists(int id)
    {
        return studentContext.StudentShenbaORM.Any(e => e.StudentId == id);
        // return studentContext.StudentShenbaORM.Any(e => e.StudentId == id);
    }
}

























// if (id != student.StudentId)
// {
//     return BadRequest(student.StudentId + "StudentId in request body doesn't match id in route parameter" + id);
// }
// var studentdata = await studentContext.StudentShenbaORM.FindAsync(id);
// if (studentdata == null)
// {
//     return NotFound("Student record not found");
// }
// studentContext.Entry(student).State = EntityState.Modified;
// try
// {
//     await studentContext.SaveChangesAsync();
// }
// catch (DbUpdateConcurrencyException)
// {
//     throw;
// }

// return NotFound(studentdata.StudentId+"hell0");