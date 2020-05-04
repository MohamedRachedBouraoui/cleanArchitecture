using System;
using System.Collections.Generic;
using System.Text;
using Uccspu.Domaine.Communs;

namespace Uccspu.Domaine.Entites
{
   public  class Episode : Auditable
    {
        public int Id { get; set; }
        public string Libelle { get; set; }
    }
}
