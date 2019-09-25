using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using System.Drawing;
using OpenTK.Input;
using CG_Biblioteca;

namespace gcgcg
{
    class Mundo : GameWindow
    {
        public static Mundo instance = null;

        public Mundo(int width, int height) : base(width, height) { }

        public static Mundo getInstance()
        {
            if (instance == null)
                instance = new Mundo(600, 600);
            return instance;
        }

        private Camera camera = new Camera();
        protected List<Objeto> objetosLista = new List<Objeto>();
        private bool moverPto = false;
        //FIXME: estes objetos não devem ser atributos do Mundo
        private Circulo _circuloCentro, _circuloMaior;
        private const double posicao = 0;
        private bool _mover = true;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            _circuloMaior = new Circulo("A", new Ponto4D(posicao, posicao, 0), 100, false, true);
            _circuloCentro = new Circulo("B", new Ponto4D(posicao, posicao, 0), 25, true);
            objetosLista.Add(_circuloMaior);
            objetosLista.Add(_circuloCentro);

            GL.ClearColor(Color.Gray);
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(camera.xmin, camera.xmax, camera.ymin, camera.ymax, camera.zmin, camera.zmax);

            if (_circuloCentro.PontoCentro.X > _circuloMaior.bBox.obterMenorX
            && _circuloCentro.PontoCentro.X < _circuloMaior.bBox.obterMaiorX
            && _circuloCentro.PontoCentro.Y > _circuloMaior.bBox.obterMenorY
            && _circuloCentro.PontoCentro.Y < _circuloMaior.bBox.obterMaiorY)
            {
                _circuloMaior.GerarBBox(Color.Pink);
                _mover = true;
            }
            else
            {
                double valor = (Convert.ToInt32((_circuloMaior.PontoCentro.X - _circuloCentro.PontoCentro.X)) ^ 2 + Convert.ToInt32((_circuloMaior.PontoCentro.Y - _circuloCentro.PontoCentro.Y)) ^ 2);

                if (valor <= _circuloMaior.Raio)
                {
                    _circuloMaior.GerarBBox(Color.Cyan);
                    _mover = true;
                }
                else
                {
                    _circuloMaior.GerarBBox(Color.Yellow);
                    _mover = false;
                    _circuloCentro.Mover(new Ponto4D(posicao, posicao, 0));
                }
            }
        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            Sru3D();

            for (var i = 0; i < objetosLista.Count; i++)
            {
                objetosLista[i].Desenhar();
            }

            this.SwapBuffers();
        }

        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            if (_mover)
            {
                _circuloCentro.Mover(new Ponto4D(e.Position.X, 600 - e.Position.Y, 0));
            }
        }

        private void Sru3D()
        {
            GL.LineWidth(1);
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Red);
            GL.Vertex3(-125, -125, 0); GL.Vertex3(0, -125, 0);
            GL.Color3(Color.Green);
            GL.Vertex3(-125, -125, 0); GL.Vertex3(-125, 0, 0);
            GL.Color3(Color.Blue);
            GL.Vertex3(0, 0, 0); GL.Vertex3(0, 0, 200);
            GL.End();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Mundo window = new Mundo(600, 600);
            window.Title = "CG_Template";
            window.Run(1.0 / 60.0);
        }
    }
}
