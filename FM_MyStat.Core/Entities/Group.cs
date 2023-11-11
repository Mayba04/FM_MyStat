﻿using FM_MyStat.Core.Entities.Users;
using FM_MyStat.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.Entities
{
    public class Group : IEntity
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public string TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public IEnumerable<Student> Students { get; set; }
        public IEnumerable<Homework> Homeworks { get; set; }
    }
}
