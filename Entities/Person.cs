using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PersonAPI.Entities;

[Table("person")]
public partial class Person
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [StringLength(200)]
    public string? Name { get; set; }

    [Column("surname")]
    [StringLength(200)]
    public string? Surname { get; set; }

    [Column("age")]
    public int? Age { get; set; }

    [Column("gender")]
    [StringLength(10)]
    public string? Gender { get; set; }

    [Column("salary")]
    public double? Salary { get; set; }

    [Column("campus_id")]
    [StringLength(5)]
    public string? CampusId { get; set; }

    [Column("fac_id")]
    [StringLength(5)]
    public string? FacId { get; set; }

    [Column("dept_id")]
    [StringLength(5)]
    public string? DeptId { get; set; }
}
