using System;
using System.Drawing;
using System.Collections.Generic;
using CG_Biblioteca;
using System.Linq;

namespace gcgcg
{
    internal class FuncoesDesenho
    {
        public static void AlterarPrimitivaDesenhos(List<Desenho> objetosLista)
        {
            var desenho = objetosLista.Last();
            desenho.AlterarPrimitiva(true);
            desenho.Redesenhar();
        }
    }
}