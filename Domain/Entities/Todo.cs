// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Domain.Entities
{
    /// <summary>
    /// Tabela do projeto inicial
    /// </summary>
    public partial class Todo
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Title { get; set; }
        public int Complete { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateComplete { get; set; }
        [StringLength(50)]
        public string Description { get; set; }
    }
}