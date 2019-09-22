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
        Camera camera = new Camera();
        private static Ponto4D _pontoA = new Ponto4D(-200, -200);
        private static Ponto4D _pontoB = new Ponto4D(-200, 200);
        private static Ponto4D _pontoC = new Ponto4D(200, 200);
        private static Ponto4D _pontoD = new Ponto4D(200, -200);
        private static SegReta _segRetaA = new SegReta("A", _pontoA, _pontoB, 100, Color.Cyan);
        private static SegReta _segRetaB = new SegReta("B", _pontoB, _pontoC, 100, Color.Cyan);
        private static SegReta _segRetaC = new SegReta("C", _pontoC, _pontoD, 100, Color.Cyan);
        private static Ponto _pontoAA = new Ponto("AA", _pontoA, 20, Color.Black);
        private static Ponto _pontoBB = new Ponto("BB", _pontoB, 20, Color.Black);
        private static Ponto _pontoCC = new Ponto("CC", _pontoC, 20, Color.Black);
        private static Ponto _pontoDD = new Ponto("DD", _pontoD, 20, Color.Red);
        private static PontoAtual _pontoAtual = PontoAtual.D;
        private static Spline _spline = new Spline("Spline", _pontoD, _pontoC, _pontoB, _pontoA, 15, Color.Yellow);

        protected List<Objeto> objetosLista = new List<Objeto>();
        private bool moverPto = false;

        public Mundo(int width, int height) : base(width, height) { }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            objetosLista.Add(_spline);
            objetosLista.Add(_segRetaA);
            objetosLista.Add(_segRetaB);
            objetosLista.Add(_segRetaC);
            objetosLista.Add(_pontoAA);
            objetosLista.Add(_pontoBB);
            objetosLista.Add(_pontoCC);
            objetosLista.Add(_pontoDD);

            GL.ClearColor(Color.Gray);
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(camera.xmin, camera.xmax, camera.ymin, camera.ymax, camera.zmin, camera.zmax);
        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.MatrixMode(MatrixMode.Modelview);

            Sru3D();

            for (var i = 0; i < objetosLista.Count; i++)
            {
                objetosLista[i].Desenhar();
            }

            this.SwapBuffers();
        }

        protected override void OnKeyDown(OpenTK.Input.KeyboardKeyEventArgs e)
        {

       
                switch (e.Key)
                {
                    case Key.Escape:
                        Exit();
                        break;
                    case Key.Number1:
                        _pontoAtual = PontoAtual.A;
                        pintarPonto();
                        break;
                    case Key.Number2:
                        _pontoAtual = PontoAtual.B;
                        pintarPonto();
                        break;
                    case Key.Number3:
                        _pontoAtual = PontoAtual.C;
                        pintarPonto();
                        break;
                    case Key.Number4:
                        _pontoAtual = PontoAtual.D;
                        pintarPonto();
                        break;
                    case Key.W:
                    case Key.C:
                        moverPonto(0, 5);
                        break;
                    case Key.S:
                    case Key.B:
                        moverPonto(0, -5);
                        break;
                    case Key.A:
                    case Key.E:
                        moverPonto(-5, 0);
                        break;
                    case Key.D:
                        moverPonto(5, 0);
                        break;
                    case Key.Minus:
                        _spline.incrementarPontos();
                        break;
                    case Key.Plus:
                        _spline.decrementarPontos();
                        break;
                }
       


        }

        private void Sru3D()
        {
            GL.LineWidth(7);
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Red);
            GL.Vertex3(0, 0, 0); GL.Vertex3(200, 0, 0);
            GL.Color3(Color.Green);
            GL.Vertex3(0, 0, 0); GL.Vertex3(0, 200, 0);
            GL.Color3(Color.Blue);
            GL.Vertex3(0, 0, 0); GL.Vertex3(0, 0, 200);
            GL.End();
        }

        private void pintarPonto()
        {
            switch (_pontoAtual)
            {
                case PontoAtual.A:
                    _pontoAA.Cor = Color.Red;
                    _pontoBB.Cor = Color.Black;
                    _pontoCC.Cor = Color.Black;
                    _pontoDD.Cor = Color.Black;
                    break;
                case PontoAtual.B:
                    _pontoBB.Cor = Color.Red;
                    _pontoAA.Cor = Color.Black;
                    _pontoCC.Cor = Color.Black;
                    _pontoDD.Cor = Color.Black;
                    break;
                case PontoAtual.C:
                    _pontoCC.Cor = Color.Red;
                    _pontoAA.Cor = Color.Black;
                    _pontoBB.Cor = Color.Black;
                    _pontoDD.Cor = Color.Black;
                    break;
                case PontoAtual.D:
                    _pontoDD.Cor = Color.Red;
                    _pontoAA.Cor = Color.Black;
                    _pontoBB.Cor = Color.Black;
                    _pontoCC.Cor = Color.Black;
                    break;
            }
        }
        private void moverPonto(double x, double y)
        {
            switch (_pontoAtual)
            {
                case PontoAtual.A:
                    _pontoA.X += x;
                    _pontoA.Y += y;
                    break;
                case PontoAtual.B:
                    _pontoB.X += x;
                    _pontoB.Y += y;
                    break;
                case PontoAtual.C:
                    _pontoC.X += x;
                    _pontoC.Y += y;
                    break;
                case PontoAtual.D:
                    _pontoD.X += x;
                    _pontoD.Y += y;
                    break;
            }
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
