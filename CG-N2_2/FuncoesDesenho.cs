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
            desenho.AlterarPrimitiva();
            desenho.Redesenhar();
        }

        public static void AtualizarValoresBBox(List<Desenho> objetosLista)
        {
            foreach (Desenho desenho in objetosLista)
            {
                desenho.BBox = new BBox();
                bool primeiroPonto = true;

                foreach (Ponto4D ponto in desenho.RetornaListaPontos())
                {
                    if (primeiroPonto)
                    {
                        desenho.BBox.Atribuir(ponto);
                        primeiroPonto = false;
                    }
                    else
                    {
                        desenho.BBox.Atualizar(ponto);
                    }
                }
            }
        }
    }
}