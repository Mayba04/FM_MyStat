﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.DTOs.UsersDTO.Student
{
    public class DeleteStudentDTO
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}
