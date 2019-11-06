using System;
using System.Drawing;
using System.Collections.Generic;
using CG_Biblioteca;
using System.Linq;

namespace gcgcg
{
    internal class FuncoesDesenho
    {
        /// <summary>
        /// Alterna o tipo da primitiva entre <c>PrimitiveType.LineStrip</c> e <c>PrimitiveType.LineLoop</c> e redesenha a cena
        /// </summary>
        /// <param name="objetosLista">Recebe uma lista de desenhos e pega o ultimo</param>
        public static void AlterarPrimitivaDesenhos(List<Desenho> objetosLista)
        {
            var desenho = objetosLista.Last();
            desenho.AlterarPrimitiva();
            desenho.Redesenhar();
        }


        /// <summary>
        /// pega todos os pontos extremos da figura e atribui para a BBox
        /// </summary>
        /// <param name="objetosLista">Recebe uma lista de desenhos</param>
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

                desenho.BBox.ProcessarCentro();
            }
        }
    }
}