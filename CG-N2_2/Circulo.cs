using System;
using CG_Biblioteca;

namespace gcgcg
{
    internal class Circulo : ObjetoAramado
    {
        public Circulo(string rotulo) : base(rotulo)
        {
            GerarPtosCirculos();
        }

        private void GerarPtosCirculos() 
        {
            base.PontosRemoverTodos();
            for (double i = .0; i <= 72.0; i++) 
            {
                double theta = 2.0 * Math.PI * i / 72.0;

                double x = 100.0 * Math.Sin(theta);
                double y = 100.0 * Math.Cos(theta);
                base.PontosAdicionar(new Ponto4D(x, y));
            }
        }
    }
}