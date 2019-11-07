using System;
using System.Drawing;
using System.Collections.Generic;
using CG_Biblioteca;
using System.Linq;

namespace gcgcg
{
	internal static class Funcoes
	{
		/// <summary>
		/// Alterna o tipo da primitiva entre <c>PrimitiveType.LineStrip</c> e <c>PrimitiveType.LineLoop</c> e redesenha a cena
		/// </summary>
		/// <param name="objetoEmFoco">Recebe uma lista de desenhos e pega o ultimo</param>
		public static void AlterarPrimitivaObjetoAtual(ObjetoAramado objetoEmFoco)
		{
			if (objetoEmFoco != null)
			{
				objetoEmFoco.AlterarPrimitiva();
			}
		}

		/// <summary>
		/// pega todos os pontos extremos da figura e atribui para a BBox
		/// </summary>
		/// <param name="objetoEmFoco">Recebe uma lista de desenhos</param>
		public static void AtualizarBBoxObjetoAtual(ObjetoAramado objetoEmFoco)
		{
			if (objetoEmFoco != null)
			{
				objetoEmFoco.BBox = new BBox();
				bool primeiraInteracao = true;

				foreach (var ponto in objetoEmFoco.RetornarListaDePontos())
				{
					if (primeiraInteracao)
					{
						objetoEmFoco.BBox.Atribuir(ponto);
						primeiraInteracao = !primeiraInteracao;
					}
					else
					{
						objetoEmFoco.BBox.Atualizar(ponto);
					}
				}

				objetoEmFoco.BBox.ProcessarCentro();
			}
		}

		/// <summary>
		/// Verifica se o ponto do clique foi dentro ou fora de um objeto
		/// </summary>
		public static void SelecionarPoligno(List<ObjetoAramado> listaObjetos, Ponto4D pontoClique, ref ObjetoAramado objetoSelecionado)
		{
			objetoSelecionado = null;

			foreach (var objetoAtual in listaObjetos)
			{
				if (objetoSelecionado != null)
				{
					break;
				}

				int resultadoOperacao = objetoAtual.PontoEmPoligno(pontoClique);

				if ((resultadoOperacao % 2) != 0)
				{
					objetoSelecionado = objetoAtual;
					break;
				}
				else
				{
					if (objetoAtual.ObjetosLista.Count > 0)
					{
						SelecionarPoligno(objetoAtual.ObjetosLista, pontoClique, ref objetoSelecionado);
					}
				}
			}
		}

		public static void FuncaoKeyE(List<ObjetoAramado> listaObjetos)
		{
			foreach (var objeto in listaObjetos)
			{
				objeto.PontosExibirObjeto();
			}
		}
	}
}