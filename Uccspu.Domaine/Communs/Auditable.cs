using System;

namespace Uccspu.Domaine.Communs
{
    public abstract class Auditable
    {
        public DateTime CreeLe { get; set; }
        public string CreePar { get; set; }
        public DateTime? ModifieLe { get; set; }
        public string ModifiePar { get; set; }
    }
}
