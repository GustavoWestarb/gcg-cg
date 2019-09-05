﻿using System;
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

        private Circulo circuloA;

        public Mundo(int width, int height) : base(width, height) { }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            circuloA = new Circulo("A");
            objetosLista.Add(circuloA);

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
                case Key.E:
                    camera.panEsq();
                    break;
                case Key.D:
                    camera.panDir();
                    break;
                case Key.C:
                    camera.panCim();
                    break;
                case Key.B:
                    camera.panBai();
                    break;
                case Key.I:
                    camera.zoomIn();
                    break;
                case Key.O:
                    camera.zoomOut();
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
