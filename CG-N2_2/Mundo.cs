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
    private static Mundo instance = null;

    private Mundo(int width, int height) : base(width, height) { }

    public static Mundo getInstance(int width, int height)
    {
      if (instance == null)
        instance = new Mundo(width,height);
      return instance;
    }

    private Camera camera = new Camera();
    protected List<Objeto> objetosLista = new List<Objeto>();
    private bool moverPto = false;
    //FIXME: estes objetos não devem ser atributos do Mundo
    private Retangulo retanguloA, retanguloB;

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);

      retanguloA = new Retangulo("A", new Ponto4D(50, 50, 0), new Ponto4D(150, 150, 0));
      retanguloB = new Retangulo("B", new Ponto4D(50, 150, 0), new Ponto4D(150, 250, 0));
      objetosLista.Add(retanguloA);
      objetosLista.Add(retanguloB);

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
      GL.LoadIdentity();

      Sru3D();

      for (var i = 0; i < objetosLista.Count; i++)
      {
        objetosLista[i].Desenhar();
      }

      this.SwapBuffers();
    }

    protected override void OnKeyDown(OpenTK.Input.KeyboardKeyEventArgs e)
    {
      if (e.Key == Key.Escape)
        Exit();
      else
      if (e.Key == Key.E)
      {
        for (var i = 0; i < objetosLista.Count; i++)
        {
          objetosLista[i].PontosExibirObjeto();
        }
      }
      else
      if (e.Key == Key.M)
      {
        moverPto = !moverPto;
      }
    }

    protected override void OnMouseMove(MouseMoveEventArgs e)
    {
      if (moverPto)
      {
        retanguloB.MoverPtoSupDir(new Ponto4D(e.Position.X, 600 - e.Position.Y, 0));
      }
    }

    private void Sru3D()
    {
      GL.LineWidth(1);
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
      Mundo window = Mundo.getInstance(600,600);
      window.Title = "CG-N2_2";
      window.Run(1.0 / 60.0);
    }
  }

}
