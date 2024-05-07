using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dummy;

[ApiController]
[Route("[controller]")]
public class DepartmentController : ControllerBase
{
    private readonly DepartmentContext departmentContext;
    public DepartmentController(DepartmentContext context)
    {
        departmentContext = context;
    }
    [HttpGet("GetAll")]
    public async Task<ActionResult<IEnumerable<DepartmentsShenba>>> GetDepartmentDetials()
    {
        return await departmentContext.DepartmentsShenba.ToListAsync();
    }

     [HttpGet("GetById/{id}")]
    public async Task<ActionResult<DepartmentsShenba>> GetParticulardepartment(int id)
    {
        return await departmentContext.DepartmentsShenba.FindAsync(id);
    }

    [HttpPost("AddNewDepartment")]
    public async Task<ActionResult<DepartmentsShenba>> AddDepartment(DepartmentsShenba department)
    {
        departmentContext.DepartmentsShenba.Add(department);
        await departmentContext.SaveChangesAsync();
        // return CreatedAtAction("GetParticulardepartment", new { id = department.department_id }, department);
        return NoContent();
    }

    [HttpDelete("DeleteById/{id}")]
    public async Task<ActionResult<IEnumerable<DepartmentsShenba>>> Deletedepartment(int id)
    {
        var departmentdata = await departmentContext.DepartmentsShenba.FindAsync(id);
        if (departmentdata == null)
        {
            return NotFound();
        }
        departmentContext.DepartmentsShenba.Remove(departmentdata);
        await departmentContext.SaveChangesAsync();
        return await departmentContext.DepartmentsShenba.ToListAsync();
        // return NoContent();
    }
    [HttpPut("Update/{id}")]
    public async Task<IActionResult> Updatedepartment(int id, DepartmentsShenba department)
    {
        if (id != department.department_id)
        {
            return BadRequest();
        }

        departmentContext.Entry(department).State = EntityState.Modified;

        try
        {
            await departmentContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!departmentExists(id))
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
    private bool departmentExists(int id)
    {
        return departmentContext.DepartmentsShenba.Any(e => e.department_id == id);
        // return departmentContext.DepartmentsShenba.Any(e => e.department_id == id);
    }
}
