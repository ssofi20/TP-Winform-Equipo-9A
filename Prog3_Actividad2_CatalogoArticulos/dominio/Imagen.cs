﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
    internal class Imagen
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int ArticuloId { get; set; }
    }
}
