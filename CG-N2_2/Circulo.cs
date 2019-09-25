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
        private double _positionX;
        private double _positionY;
        private bool _showCenterPoint;
        private bool _createBBox;
        private Matematica _math = new Matematica();
        private Ponto4D _pontoCentro;

        #region Getters and Setters
        public Ponto4D PontoCentro
        {
            get
            {
                return _pontoCentro;
            }
        }

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

        public double PositionX
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

        public double PositionY
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

        public Circulo(string rotulo, int positionX, int positionY, Color color, double raio, bool showCenter = false, int size = 5) : base(rotulo)
        {
            _positionX = positionX;
            _positionY = positionY;
            _color = color;
            _raio = raio;
            _size = size;
            _showCenterPoint = showCenter;
        }

        public Circulo(string rotulo, int positionX, int positionY, double raio, bool showCenterPoint, bool createBBox)
        : base(rotulo)
        {
            _positionX = positionX;
            _positionY = positionY;
            _showCenterPoint = showCenterPoint;
            _createBBox = createBBox;
            _color = Color.Yellow;
            _size = 1;
            _raio = raio;
        }

        public Circulo(string rotulo, Ponto4D pontoCentro, double _raio, bool mostrarPontoCentral = false, bool mostrarBBox = false)
        : base(rotulo)
        {
            this._pontoCentro = pontoCentro;
            this.Raio = _raio;
            this._showCenterPoint = mostrarPontoCentral;
            this._createBBox = mostrarBBox;
            this. _size = 3;
        }

        protected override void DesenharAramado()
        {
            base.PontosRemoverTodos();

            if (_showCenterPoint)
            {
                GL.PointSize(5);
                GL.Begin(PrimitiveType.Points);
                GL.Vertex2(_pontoCentro.X, _pontoCentro.Y);
                GL.Color3(_color);
                GL.End();
            }

            GL.PointSize(_size);
            GL.Begin(PrimitiveType.LineLoop);
            GL.Color3(_color);
            double angulo = 0;

            for (double i = .0; i <= 72.0; i++)
            {
                var ponto = _math.ptoCirculo(angulo, _raio);
                GL.Vertex2(ponto.X + _pontoCentro.X, ponto.Y + _pontoCentro.Y);
                angulo += 5;
            }

            GL.End();

            if (_createBBox)
            {
                GerarBBox(Color.Pink);
            }
        }

        public void GerarBBox(Color cor)
        {
            Ponto4D pontoSuperiorDireito = _math.ptoCirculo(45, _raio);
            Ponto4D pontoInferiorEsquerdo = _math.ptoCirculo(225, _raio);

            bBox = new BBox(pontoInferiorEsquerdo.X, pontoInferiorEsquerdo.Y, 0, pontoSuperiorDireito.X, pontoSuperiorDireito.Y, 0);
            bBox.desenhaBBox();
            bBox.atualizarCorBBox(cor);
        }

        public void Mover(Ponto4D ponto)
        {
            _pontoCentro = ponto;
            DesenharAramado();
        }
    }
}