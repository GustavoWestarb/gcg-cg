using System;
using System.Drawing;
using System.Collections.Generic;
using CG_Biblioteca;

namespace gcgcg
{
    internal class FuncoesDesenho
    {
        public static void AlterarPrimitivaDesenhos(List<Objeto> objetosLista)
        {
            bool primeiraVerificacao = true;

            foreach (Objeto objeto in objetosLista)
            {
                Desenho desenho = (Desenho)objeto;
                desenho.AlterarPrimitiva(primeiraVerificacao);
                desenho.Redesenhar();
                primeiraVerificacao = false;
            }
        }
    }
}