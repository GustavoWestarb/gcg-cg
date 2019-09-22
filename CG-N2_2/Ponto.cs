using System;
using CG_Biblioteca;
using OpenTK.Graphics.OpenGL; 
using System.Drawing;

namespace gcgcg {
    class Ponto : ObjetoAramado
    {
        private Ponto4D _ponto;
        int _tamanho;

        private Color _cor;
        public Color Cor 
        {
            get => _cor; 
            set => _cor = value;
        }
        public Ponto(string rotulo, Ponto4D ponto, int tamanho, Color cor) : base(rotulo)
        {
            _ponto = ponto;
            _tamanho = tamanho;
            _cor = cor;
        }

        protected override void DesenharAramado()
        {
            GL.PointSize(_tamanho);
            GL.Begin(PrimitiveType.Points);
            GL.Color3(_cor);
            GL.Vertex2(_ponto.X, _ponto.Y);
            GL.End();
        }

        public void incrementarPosicao(double x, double y) 
        {
            _ponto.X += x;
            _ponto.Y += y;
        }
    }
}