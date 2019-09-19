using System;
using OpenTK.Graphics.OpenGL;
using CG_Biblioteca;
using System.Drawing;

namespace gcgcg
{
    internal class GenericPoints : ObjetoAramado
    {
        private PrimitiveType _type;
        public PrimitiveType Type 
        {
            get => _type;
            set => _type = value;
        }
        private Ponto4D PointA;
        private Ponto4D PointB;
        private Ponto4D PointC;
        private Ponto4D PointD;
        public GenericPoints(string rotulo, PrimitiveType type) : base(rotulo)
        {  
            Type = type;

            PointA = new Ponto4D(){ X = -200, Y = 200};
            PointB = new Ponto4D(){ X = 200, Y = 200};
            PointC = new Ponto4D(){ X = 200, Y = -200};
            PointD = new Ponto4D(){ X = -200, Y = -200};
        }

        protected override void DesenharAramado()
        {
            GL.PointSize(5);
            GL.Begin(Type);
                GL.Color3(Color.Cyan);
                GL.Vertex2(PointA.X, PointA.Y);
                GL.Color3(Color.Magenta);
                GL.Vertex2(PointB.X, PointB.Y);
                GL.Color3(Color.Black);
                GL.Vertex2(PointC.X, PointC.Y);           
                GL.Color3(Color.Yellow);
                GL.Vertex2(PointD.X, PointD.Y);
            GL.End();
        }
    }
}