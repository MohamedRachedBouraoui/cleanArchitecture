using System.Reflection.Metadata;

namespace Uccspu.Affaires.Communs.Exceptions
{
    public class ReponseApi
    {
        public  const string MAUVAISE_DEMANDE = "Vous avez fait une mauvaise demande.";
        public  const string NON_AUTORISE = "Vous n'êtes pas autorisé.";
        public  const string RESSOURCE_INTROUVABLE = "Ressource introuvable.";
        public  const string ERREUR_INTERNE = "Une erreur interne a eu lieu ! S'il vous plait, veuillez contacter l'équipe de support.";
        public ReponseApi()
        {

        }
        public ReponseApi(int codeStatut, string message = null, string details = null)
        {
            ReponseApiCodeStatut_ = codeStatut;
            ReponseApiMessage_ = message ?? RecupererMsgParDefautSelonCodeStatut(codeStatut);
            ReponseApiDetailsException_ = details;
        }

        public int ReponseApiCodeStatut_ { get; set; }
        public string ReponseApiMessage_ { get; set; }

        public string ReponseApiDetailsException_ { get; set; } // Va Continir des détails tels que la trace de pile de l'exception

        private string RecupererMsgParDefautSelonCodeStatut(int codeStatut)
        {
            return codeStatut switch
            {

                400 => MAUVAISE_DEMANDE,
                401 => NON_AUTORISE,
                404 => RESSOURCE_INTROUVABLE,
                500 => ERREUR_INTERNE,

                _ => null
            };
        }
    }
}