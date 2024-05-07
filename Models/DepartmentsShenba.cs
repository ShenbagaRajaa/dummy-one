using System.ComponentModel.DataAnnotations;

namespace dummy;

public class DepartmentsShenba
{
    [Key]
    public int department_id {get;set;}
    public string? department_name {get;set;}
}
