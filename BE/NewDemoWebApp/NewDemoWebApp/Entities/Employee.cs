using demoWebApp.Entities.IDateTracking;
using System;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Summary description for Class1
/// </summary>
public class Employee : IDateTracking
{
    public string EmployeeCode { get; set; }

    public string Department {  get; set; }

    public string Unit {  get; set; }

    [ForeignKey("User")]
    public Guid UserId { get; set; }
    
    public User User { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }
}
