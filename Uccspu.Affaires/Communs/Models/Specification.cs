using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Uccspu.Affaires.Communs.Interfaces;

namespace Uccspu.Affaires.Communs.Models
{
    public class Specification<T> : ISpecification<T>
    {
        public Specification()
        {

        }
        public Specification(Expression<Func<T, bool>> critere)
        {
            Critere = critere;
        }

        public Expression<Func<T, bool>> Critere { get; }

        public List<Expression<Func<T, object>>> Inclusions { get; } = new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> TriesPar { get; private set; }

        public Expression<Func<T, object>> TriesParDesc { get; private set; }

        public bool AvecPagination { get; private set; }

        public int PagesASauter { get; private set; }

        public int UnePageDoitInclure { get; private set; }

        protected void AjouterInclusions(Expression<Func<T, object>> expr)
        {
            Inclusions.Add(expr);
        }

        protected void AjouterTriesPar(Expression<Func<T, object>> expr)
        {
            TriesPar = expr;
        }

        protected void AjouterTriesParDesc(Expression<Func<T, object>> expr)
        {
            TriesParDesc = expr;
        }

        protected void AppliquerPagination(int pagesASauter, int unePageDoitInclure)
        {
            AvecPagination = true;
            PagesASauter = (pagesASauter - 1) * unePageDoitInclure; // Le num de la page désirée
            UnePageDoitInclure = unePageDoitInclure;
        }
    }
}