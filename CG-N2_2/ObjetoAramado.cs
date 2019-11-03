using System;
using System.Collections.Generic;
using System.Drawing;
using CG_Biblioteca;
using OpenTK.Graphics.OpenGL;

namespace gcgcg {
  internal class ObjetoAramado : Objeto {
    protected List<Ponto4D> pontosLista = new List<Ponto4D> ();
    protected int ValorCorPosicaoVermelha;
    protected int ValorCorPosicaoVerde;
    protected int ValorCorPosicaoAzul;

    public ObjetoAramado (string rotulo) : base (rotulo) 
    {
      this.ValorCorPosicaoVermelha = 255;
      this.ValorCorPosicaoVerde = 255;
      this.ValorCorPosicaoAzul = 255;
    }

    protected override void DesenharAramado () {
      GL.LineWidth(base.PrimitivaTamanho);
      GL.Color3(Color.FromArgb(0, ValorCorPosicaoVermelha, ValorCorPosicaoVerde, ValorCorPosicaoAzul));
      GL.Begin(PrimitivaTipo);
      foreach (Ponto4D pto in pontosLista) {
        GL.Vertex2(pto.X, pto.Y);
      }
      GL.End ();
    }

    protected void PontosAdicionar (Ponto4D pto) {
      pontosLista.Add(pto);

      if (pontosLista.Count.Equals(1))
      {
        base.BBox.Atribuir(pto);
      }
      else
      {
        base.BBox.Atualizar(pto);
      }
      base.BBox.ProcessarCentro();
    }

    protected void PontosRemoverTodos () {
      pontosLista.Clear ();
    }

    protected override void PontosExibir () {
      Console.WriteLine ("__ Objeto: " + base.rotulo);
      for (var i = 0; i < pontosLista.Count; i++) {
        Console.WriteLine ("P" + i + "[" + pontosLista[i].X + "," + pontosLista[i].Y + "," + pontosLista[i].Z + "," + pontosLista[i].W + "]");
      }
    }
  }
}