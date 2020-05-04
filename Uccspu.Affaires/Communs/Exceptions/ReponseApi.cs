namespace Uccspu.Affaires.Communs.Exceptions
{
    public class ReponseApi
    {
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

                400 => "Vous avez fait une mauvaise demande.",
                401 => "Vous n'êtes pas autorisé.",
                404 => "Ressource introuvable.",
                500 => "Une erreur interne a eu lieu ! S'il vous plait, veuillez contacter l'équipe de support.",

                _ => null
            };
        }
    }
}