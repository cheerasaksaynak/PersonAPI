using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PersonAPI.Entities;

[Keyless]
public partial class PersonBasicInfo
{
    [Column("id")]
    public int? Id { get; set; }

    [Column("name")]
    [StringLength(200)]
    public string? Name { get; set; }

    [Column("surname")]
    [StringLength(200)]
    public string? Surname { get; set; }
}
