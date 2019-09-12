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
        private bool moverPto = false;

        public Mundo(int width, int height) : base(width, height) { }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);


            var circuloA = new Circulo("A", 0, 100, Color.Black, 100);
            var circuloB = new Circulo("B", 100, -100, Color.Black, 100);
            var circuloC = new Circulo("C", -100, -100, Color.Black, 100);

            objetosLista.Add(circuloA);
            objetosLista.Add(circuloB);
            objetosLista.Add(circuloC);            

            var segRetaA = new SegReta("D", circuloA.RetornarPontosCentro(), circuloB.RetornarPontosCentro(),
            5, Color.LightBlue); 
            var segRetaB = new SegReta("E", circuloA.RetornarPontosCentro(), circuloC.RetornarPontosCentro(),
            5, Color.LightBlue);
            var segRetaC = new SegReta("F", circuloB.RetornarPontosCentro(), circuloC.RetornarPontosCentro(),
            5, Color.LightBlue);

            objetosLista.Add(segRetaA);
            objetosLista.Add(segRetaB);
            objetosLista.Add(segRetaC);

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
                case Key.M:
                    moverPto = !moverPto;
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
