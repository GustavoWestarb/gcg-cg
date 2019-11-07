using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CG_Biblioteca;
using OpenTK.Graphics.OpenGL;

namespace gcgcg
{
    internal class ObjetoAramado : Objeto
    {
        protected List<Ponto4D> pontosLista = new List<Ponto4D>();
        private Color _cor;

        public ObjetoAramado(string rotulo) : base(rotulo)
        {
            this._cor = Color.White;
        }

		/// <summary>
		/// Muda a cor do poligono
		/// </summary>
		/// <param name="cor">Nova cor</param>
		public void AlterarCor(Color cor)
		{
			this._cor = cor;
		}

		/// <summary>
		/// Move o último ponto da lista.
		/// </summary>
		/// <param name="ponto">Novos valores para o último ponto.</param>
		public void MoverUltimoPonto(Ponto4D ponto)
		{
			this.pontosLista[this.pontosLista.Count - 1] = ponto;
		}

		/// <summary>
		/// Função utilizada para adcionar um ponto ao poligono atual, chama a função <c>AdcionarPonto</c> e <c>DesenharAramado</c> da super classe
		/// que adiciona os pontos na lista <c>base.pontosLista</c> da super classe e desenha os pontos em tela utilizando o openTK
		/// </summary>
		/// <param name="ponto">Ponto a ser adcionado</param>
		public void AdicionarPonto(Ponto4D ponto)
		{
			PontosAdicionar(ponto);
			PontosAdicionar(ponto);
			DesenharAramado();
		}

		/// <summary>
		/// Remove o ponto informado da <c>base.pontosLista</c>, recria o poligono e redesenha ele.
		/// </summary>
		/// Veja <see cref="Desenho.Redesenhar()"/> Para redesenhar o poligono
		/// <param name="ponto">Ponto a ser removido</param>
		public void RemoverPonto(Ponto4D ponto)
		{
			pontosLista.Remove(ponto);
			DesenharAramado();
		}

		/// <summary>
		/// Retornar a lista de pontos
		/// </summary>
		public List<Ponto4D> RetornarListaDePontos()
		{
			return this.pontosLista;
		}

		/// <summary>
		/// Conta quantos pontos estão na <c>base.pontosLista</c>
		/// </summary>
		public int RetornarQuantidadePontos()
		{
			return pontosLista.Count;
		}

		/// <summary>
		/// Função para desenhar todos os pontos do <c>pontosLista</c> na cena do <c>OpenTK</c>
		/// </summary>
		protected override void DesenharAramado()
        {
            GL.LineWidth(base.PrimitivaTamanho);
            GL.Color3(this._cor);
            GL.Begin(PrimitivaTipo);

            foreach (Ponto4D pto in pontosLista)
            {
                GL.Vertex2(pto.X, pto.Y);
            }

            GL.End();
        }

        /// <summary>
	    /// Verifica se o clique do usuário foi dentro ou fora do poligno. 
	    /// </summary>
	    /// <param name="clique">Ponto</param>
	    /// <returns></returns>
        public int PontoEmPoligno(Ponto4D clique)
        {
            if (pontosLista.Count > 1)
            {
                int qtdInterseccao = 0;

                for (int i = 0; i < pontosLista.Count; i++)
                {
					Ponto4D pontoA = pontosLista[i];
					Ponto4D pontoB;

                    if ((i + 1) < pontosLista.Count)
                    {
                        pontoB = pontosLista[i + 1];
                    }
                    else
                    {
                        pontoB = pontosLista[0];
                    }

                    float valorInterseccao = (float)((clique.Y - pontoA.Y)/ (pontoB.Y - pontoA.Y));

                    if (valorInterseccao > 0 && valorInterseccao < 1)
                    {
                        int valorResultanteXi = (int)(pontoA.X + ((pontoB.X - pontoA.X) * valorInterseccao));

                        if (valorResultanteXi >= clique.X)
                        {
                            qtdInterseccao++;
                        }
                    }
                }

                return qtdInterseccao;
            }

            return -1;
        }

        /// <summary>
        /// Adiciona um ponto na <c>pontosLista</c> 
        /// </summary>
        /// <param name="pto">Ponto a ser adcionado</param>
        protected void PontosAdicionar(Ponto4D pto)
        {
            pontosLista.Add(pto);

            if (pontosLista.Count.Equals(1))
            {
                base.BBox.Atribuir(pto);
            }
            else
            {
                base.BBox.Atualizar(pto);
            }

            base.BBox.ProcessarCentro();
        }

        /// <summary>
        /// Remove todos os pontos na <c>pontosLista</c> 
        /// </summary>
        protected void PontosRemoverTodos()
        {
            pontosLista.Clear();
        }

        /// <summary>
        /// Exibe todos os pontos no console assim como suas cordenadas
        /// </summary>
        /// <example>
        /// __ Objeto: A
        /// P0[179,530,0,1]
        /// P1[404,380,0,1]
        /// P2[66,348,0,1]
        /// P3[130,470,0,1]
        /// </example>
        protected override void PontosExibir()
        {
            Console.WriteLine("__ Objeto: " + base.Rotulo);
            for (var i = 0; i < pontosLista.Count; i++)
            {
                Console.WriteLine("P" + i + "[" + pontosLista[i].X + "," + pontosLista[i].Y + "," + pontosLista[i].Z + "," + pontosLista[i].W + "]");
            }
        }
    }
}