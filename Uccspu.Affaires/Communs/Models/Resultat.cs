using System.Collections.Generic;
using System.Linq;

namespace Uccspu.Affaires.Communs.Models
{
   public class Resultat
    {
        internal Resultat(bool avecSuccess, IEnumerable<string> erreurs=null)
        {
            EstAvecSuccess = avecSuccess;
            if (erreurs != null)
            {
            Erreurs = erreurs.ToArray();
            }
        }

        public bool EstAvecSuccess { get; set; }

        public string[] Erreurs { get; set; }

        public static Resultat Success()
        {
            return new Resultat(true);
        }

        public static Resultat Echec(IEnumerable<string> erreurs)
        {
            return new Resultat(false, erreurs);
        }
    }
}
