using System;
using System.Drawing;
using CG_Biblioteca;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL;
namespace gcgcg
{
    class Spline : ObjetoAramado
    {
        private Ponto4D _pontoA;
        private Ponto4D _pontoB;
        private Ponto4D _pontoC;
        private Ponto4D _pontoD;
        private int _tamanho;
        private Color _cor;
        private double _indice = 0.0625;
        public Spline(string rotulo, Ponto4D pontoA, Ponto4D pontoB, Ponto4D pontoC, Ponto4D pontoD, int tamanho, Color cor) : base(rotulo)
        {
            _tamanho = tamanho;
            _pontoA = pontoA;
            _pontoB = pontoB;
            _pontoC = pontoC;
            _pontoD = pontoD;
            _cor = cor;
        }


        protected override void DesenharAramado() 
        {
            GL.LineWidth(_tamanho);
            GL.Begin(PrimitiveType.LineStrip);
            GL.Color3(_cor);
            for (double t = .0; t <= 1.0; t+= _indice)
            {
                var abx = _pontoA.X + (_pontoB.X-_pontoA.X) * t;
                var aby = _pontoA.Y + (_pontoB.Y-_pontoA.Y) * t;


                var bcx = _pontoB.X + (_pontoC.X-_pontoB.X) * t;
                var bcy = _pontoB.Y + (_pontoC.Y-_pontoB.Y) * t;

                var cdx = _pontoC.X + (_pontoD.X-_pontoC.X) * t;
                var cdy = _pontoC.Y + (_pontoD.Y-_pontoC.Y) * t;

                var abbcx = abx + (bcx-abx) * t;
                var abbcy = aby + (bcy-aby) * t;

                var bccdx = bcx + (cdx-bcx) * t;
                var bccdy = bcy + (cdy-bcy) * t;

                var abbcbccdx = abbcx + (bccdx-abbcx) * t;
                var abbcbccdy = abbcy + (bccdy-abbcy) * t;

                GL.Vertex2(abbcbccdx, abbcbccdy);

            }
            GL.End();
        }

        public void incrementarPontos()
        {
            if (_indice < 1)
            {
                _indice *= 2;
            }
        }
        public void decrementarPontos()
        {
            if (_indice >= 0.0625)
            {
                _indice /= 2;
            }
        }
    }
}