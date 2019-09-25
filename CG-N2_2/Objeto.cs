using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using CG_Biblioteca;

namespace gcgcg
{
  internal abstract class Objeto
  {
    private string rotulo;
    private PrimitiveType primitivaTipo = PrimitiveType.LineLoop;
    private float primitivaTamanho = 2;
    public BBox bBox = new BBox();

    public Objeto(string rotulo)
    {
      this.rotulo = rotulo;
    }

    protected PrimitiveType PrimitivaTipo { get => primitivaTipo; set => primitivaTipo = value; }
    protected float PrimitivaTamanho { get => primitivaTamanho; set => primitivaTamanho = value; }

    public void Desenhar()
    {
      DesenharAramado();
    }
    protected abstract void DesenharAramado();

  }
}