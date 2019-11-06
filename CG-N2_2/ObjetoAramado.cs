using System;
using System.Collections.Generic;
using System.Drawing;
using CG_Biblioteca;
using OpenTK.Graphics.OpenGL;

namespace gcgcg
{
    internal class ObjetoAramado : Objeto
    {
        protected List<Ponto4D> pontosLista = new List<Ponto4D>();
        protected Color Cor;

        public ObjetoAramado(string rotulo) : base(rotulo)
        {
            this.Cor = Color.White;
        }

        /// <summary>
        /// Função para desenhar todos os pontos do <c>pontosLista</c> na cena do <c>OpenTK</c>
        /// </summary>
        protected override void DesenharAramado()
        {
            GL.LineWidth(base.PrimitivaTamanho);
            GL.Color3(Cor);
            GL.Begin(PrimitivaTipo);

            foreach (Ponto4D pto in pontosLista)
            {
                GL.Vertex2(pto.X, pto.Y);
            }

            GL.End();
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
            Console.WriteLine("__ Objeto: " + base.rotulo);
            for (var i = 0; i < pontosLista.Count; i++)
            {
                Console.WriteLine("P" + i + "[" + pontosLista[i].X + "," + pontosLista[i].Y + "," + pontosLista[i].Z + "," + pontosLista[i].W + "]");
            }
        }
    }
}