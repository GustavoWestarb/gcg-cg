using System;
using OpenTK.Graphics.OpenGL;
using CG_Biblioteca;
using System.Drawing;

namespace gcgcg
{
  internal class SegReta : ObjetoAramado
  {
    private Ponto4D _center, _dotB;
    private int _size;
    private Color _color;
    private double _radius = 100;
    private double _angle = 45;
    private Matematica mat = new Matematica();

    public SegReta(string rotulo, int size, Color color) : base(rotulo)
    {
      _center = new Ponto4D(){ X = 0, Y = 0};
      _dotB = new Ponto4D(){ X = 100, Y = 100};
      _size = size;
      _color = color;

      updateDotB();
    }

    private void updateDotB()
    {
      _dotB.X = _center.X + (_radius * Math.Cos(Math.PI * _angle / 180.0));
      _dotB.Y = _center.Y + (_radius * Math.Sin(Math.PI * _angle / 180.0));
    }

    protected override void DesenharAramado()
    {
      GL.LineWidth(_size);
      GL.Begin(PrimitiveType.Lines);
        GL.Color3(_color);
        GL.Vertex3(_center.X, _center.Y, 0); 
        GL.Vertex3(_dotB.X, _dotB.Y, 0);
      GL.End();
    }

    public void MoveLine(int strength)
    {
      _center.X += strength;
      updateDotB();
    }

    public void ChangeSizeLine(int strength)
    {
      _radius += strength;
      updateDotB();
    }

    public void LoopLine(int strength)
    {
      _angle = _angle == 360 ? strength : _angle + strength;
      updateDotB();
    }
  }
}