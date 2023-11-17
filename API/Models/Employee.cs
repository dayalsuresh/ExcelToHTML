using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Models;

public class Employee
{
    [Required]
    [Key]
    public string GPN {get;set;} = "";

    [Required]
    [Phone]
    public string ContactNumber {get;set;} ="";

    [Required]
    [EmailAddress]
    public string Email {get;set;} ="";
    
    [Required]
    public string Address {get;set;} ="";
    
    [Required]
    public string Name {get;set;} ="";

}
