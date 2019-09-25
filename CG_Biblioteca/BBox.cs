/*
  Autor: Dalton Solano dos Reis
*/

using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace CG_Biblioteca
{
    public class BBox
    {
        private double menorX, menorY, menorZ, maiorX, maiorY, maiorZ;
        private Color _color = Color.Purple;
        private Ponto4D centro = new Ponto4D();
        public BBox(double menorX = 0, double menorY = 0, double menorZ = 0, double maiorX = 0, double maiorY = 0, double maiorZ = 0)
        {
            this.menorX = menorX; this.menorY = menorY; this.menorZ = menorZ;
            this.maiorX = maiorX; this.maiorY = maiorY; this.maiorZ = maiorZ;
        }

        public void atribuirBBox(Ponto4D pto)
        {
            this.menorX = pto.X; this.menorY = pto.Y; this.menorZ = pto.Z;
            this.maiorX = pto.X; this.maiorY = pto.Y; this.maiorZ = pto.Z;
        }

        public void atualizarBBox(Ponto4D pto)
        {
            atualizarBBox(pto.X, pto.Y, pto.Z);
        }

        public void atualizarBBox(double x, double y, double z)
        {
            if (x < menorX)
                menorX = x;
            else
            {
                if (x > maiorX) maiorX = x;
            }
            if (y < menorY)
                menorY = y;
            else
            {
                if (y > maiorY) maiorY = y;
            }
            if (z < menorZ)
                menorZ = z;
            else
            {
                if (z > maiorZ) maiorZ = z;
            }
        }

        public void processarCentroBBox()
        {
            centro.X = (maiorX + menorX) / 2;
            centro.Y = (maiorY + menorY) / 2;
            centro.Z = (maiorZ + menorZ) / 2;
        }

        public void atualizarCorBBox(Color color)
        {
			_color = color;
			desenhaBBox();
        }

        public void desenhaBBox()
        {
            GL.Color3(_color);

            // GL.PointSize(5);
            // GL.Begin(PrimitiveType.Points);
            //   GL.Vertex2(centro.X,centro.Y);
            // GL.End();

            GL.LineWidth(3);
            GL.Begin(PrimitiveType.LineLoop);
            GL.Vertex3(menorX, maiorY, menorZ);
            GL.Vertex3(maiorX, maiorY, menorZ);
            GL.Vertex3(maiorX, menorY, menorZ);
            GL.Vertex3(menorX, menorY, menorZ);
            GL.End();
            GL.Begin(PrimitiveType.LineLoop);
            GL.Vertex3(menorX, menorY, menorZ);
            GL.Vertex3(menorX, menorY, maiorZ);
            GL.Vertex3(menorX, maiorY, maiorZ);
            GL.Vertex3(menorX, maiorY, menorZ);
            GL.End();
            GL.Begin(PrimitiveType.LineLoop);
            GL.Vertex3(maiorX, maiorY, maiorZ);
            GL.Vertex3(menorX, maiorY, maiorZ);
            GL.Vertex3(menorX, menorY, maiorZ);
            GL.Vertex3(maiorX, menorY, maiorZ);
            GL.End();
            GL.Begin(PrimitiveType.LineLoop);
            GL.Vertex3(maiorX, menorY, menorZ);
            GL.Vertex3(maiorX, maiorY, menorZ);
            GL.Vertex3(maiorX, maiorY, maiorZ);
            GL.Vertex3(maiorX, menorY, maiorZ);
            GL.End();
        }

        /// Obter menor valor X da BBox.
        public double obterMenorX => menorX;

        /// Obter menor valor Y da BBox.
        public double obterMenorY => menorY;

        /// Obter menor valor Z da BBox.
        public double obterMenorZ => menorZ;

        /// Obter maior valor X da BBox.
        public double obterMaiorX => maiorX;

        /// Obter maior valor Y da BBox.
        public double obterMaiorY => maiorY;

        /// Obter maior valor Z da BBox.
        public double obterMaiorZ => maiorZ;

        /// Obter ponto do centro da BBox.
        public Ponto4D obterCentro => centro;

    }
}