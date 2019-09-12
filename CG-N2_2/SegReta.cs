using System;
using OpenTK.Graphics.OpenGL;
using CG_Biblioteca;
using System.Drawing;

namespace gcgcg
{
  internal class SegReta : ObjetoAramado
  {
    private Ponto4D _pontoA, _pontoB;
    private int _tamanho;
    private Color _cor;

    public SegReta(string rotulo, Ponto4D pontoA, Ponto4D pontoB, int tamanho, Color cor) : base(rotulo)
    {
      this._pontoA = pontoA;
      this._pontoB = pontoB;
      this._tamanho = tamanho;
      this._cor = cor;
    }

    protected override void DesenharAramado()
    {
      GL.LineWidth(_tamanho);
      GL.Begin(PrimitiveType.Lines);
      GL.Color3(_cor);
      GL.Vertex3(_pontoA.X, _pontoA.Y, 0); 
      GL.Vertex3(_pontoB.X, _pontoB.Y, 0);
      GL.End();
    }
  }
}