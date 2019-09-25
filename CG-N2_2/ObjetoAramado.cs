using System;
using System.Collections.Generic;
using CG_Biblioteca;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace gcgcg
{
    internal class ObjetoAramado : Objeto
    {
        protected List<Ponto4D> pontosLista = new List<Ponto4D>();
        protected Color Cor { get; set; } = Color.Yellow;

        public ObjetoAramado(string rotulo) : base(rotulo) { }

        protected override void DesenharAramado()
        {
            GL.PointSize(base.PrimitivaTamanho);
            GL.Color3(Cor);
            GL.Begin(base.PrimitivaTipo);
            foreach (Ponto4D pto in pontosLista)
            {
                GL.Vertex2(pto.X, pto.Y);
            }
            GL.End();
        }

        protected void PontosAdicionar(Ponto4D pto)
        {
            pontosLista.Add(pto);
        }

        protected void PontosRemoverTodos()
        {
            pontosLista.Clear();
        }

    }
}