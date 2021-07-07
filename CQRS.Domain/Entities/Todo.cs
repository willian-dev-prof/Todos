﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CQRS.Domain.Models.ValidationAtributes;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace CQRS.Domain.Entities
{
    /// <summary>
    /// Tabela do projeto inicial
    /// </summary>
    public class Todo
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Title { get; set; }
        public bool Complete { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateComplete { get; set; }
        [StringLength(50)]
        public string Description { get; set; }

        public Todo( string title, bool complete, DateTime? dateComplete, string description) {

            if (string.IsNullOrEmpty(title)) {
                throw new ArgumentException("Title is Required");
            }else if (string.IsNullOrEmpty(description)) {
                throw new ArgumentException("Description is Required");
            }else if ((complete && dateComplete == null) || (complete == false && dateComplete != null)) {
                throw new ArgumentException("invalid arguments for approval/disapproval");
            }else if (title.Length < 6) {
                throw new ArgumentException("Title very Short");
            }else if (description.Length < 6) {
                throw new ArgumentException("Description very Short");
            }
                
            Title = title;
            Complete = complete;
            DateComplete = dateComplete;
            Description = description;
        }
    }
}