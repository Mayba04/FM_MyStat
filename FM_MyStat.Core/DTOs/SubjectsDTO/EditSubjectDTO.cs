﻿using FM_MyStat.Core.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.DTOs.SubjectsDTO
{
    public  class EditSubjectDTO
    {
        public string Name { get; set; }
        public IEnumerable<Teacher> Teachers { get; set; }
    }
}