using System;
using System.Drawing;
using CG_Biblioteca;
using OpenTK.Graphics.OpenGL;

namespace gcgcg
{
    internal class Circulo : ObjetoAramado
    {
        private double _raio;
        private int _size;
        private Color _color;
        private int _positionX;
        private int _positionY;


        #region Getters and Setters
        public double Raio
        {
            get
            {
                return _raio;
            }
            private set
            {
                _raio = value;
            }
        }

        public int Size
        {
            get
            {
                return _size;
            }
            private set
            {
                _size = value;
            }
        }

        public Color Color
        {
            get
            {
                return _color;
            }
            private set
            {
                _color = value;
            }
        }

        public int PositionX
        {
            get
            {
                return _positionX;
            }
            private set
            {
                _positionX = value;
            }
        }

        public int PositionY
        {
            get
            {
                return _positionY;
            }
            private set
            {
                _positionY = value;
            }
        }
        #endregion

        public Circulo(string rotulo, int positionX, int positionY, Color color, double raio, int size = 5) : base(rotulo)
        {
            _positionX = positionX;
            _positionY = positionY;
            _color = color;
            _raio = raio;
            _size = size;
        }

        protected override void DesenharAramado()
        {
            
            GL.PointSize(_size);
            GL.Begin(PrimitiveType.Points);
            GL.Color3(_color);
            var mat = new Matematica();
            double angulo = 0;

            for (double i = .0; i <= 72.0; i++) 
            {
                var ponto = mat.ptoCirculo(angulo, _raio);
                GL.Vertex2(ponto.X + _positionX, ponto.Y + _positionY);
                angulo += 5;
            }

            GL.End();
        }

        public Ponto4D RetornarPontosCentro()
        {
            return new Ponto4D(_positionX, _positionY);
        }
    }
}