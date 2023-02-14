﻿using System;
using System.Collections.Generic;
using System.Collections;

#nullable disable

namespace LE.UserService.Infrastructure.Infrastructure.Entities
{
    public partial class Grouppost
    {
        public Guid Postid { get; set; }
        public Guid Groupid { get; set; }
        public BitArray IsQualified { get; set; }
        public BitArray IsHidden { get; set; }

        public virtual Group Group { get; set; }
        public virtual Post Post { get; set; }
    }
}
