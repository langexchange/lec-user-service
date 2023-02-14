﻿using System;
using System.Collections.Generic;

#nullable disable

namespace LE.UserService.Entities
{
    public partial class Restrict
    {
        public Restrict()
        {
            Restrictpunishes = new HashSet<Restrictpunish>();
        }

        public int Restrictid { get; set; }
        public int? Days { get; set; }
        public string RestrictCode { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Restrictpunish> Restrictpunishes { get; set; }
    }
}