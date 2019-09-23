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
        protected List<Objeto> objetosLista = new List<Objeto>();
        private Ponto4D _pointA;
        private Ponto4D _pointB;
        private SegReta _segLine;
        public Mundo(int width, int height) : base(width, height) { }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            _segLine = new SegReta("Line", 5, Color.Black);

            objetosLista.Add(_segLine);

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
                case Key.Q:
                    _segLine.MoveLine(-1);
                    break;
                case Key.W:
                    _segLine.MoveLine(1);
                    break;
                case Key.A:
                    _segLine.ChangeSizeLine(1);
                    break;
                case Key.S:
                    _segLine.ChangeSizeLine(-1);
                    break;
                case Key.Z:
                    _segLine.LoopLine(1);
                    break;
                case Key.X:
                    _segLine.LoopLine(-1);
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
