using System;

namespace Uccspu.Affaires.Communs.Attributs
{

    /// <summary>
    /// Utilisée pour vérifier si l'Utilisateur actuel est bien loggé
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class Authentifier : Attribute
    {
    }
}
