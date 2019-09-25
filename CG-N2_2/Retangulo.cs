using System;
using CG_Biblioteca;

namespace gcgcg
{
    internal class Retangulo : ObjetoAramado
    {
        private Ponto4D ptoInferiorEsquerdo, ptoSuperiorDireito;
        public Retangulo(string rotulo, Ponto4D ptoInferiorEsquerdo, Ponto4D ptoSuperiorDireito) : base(rotulo)
        {
            this.ptoInferiorEsquerdo = ptoInferiorEsquerdo;
            this.ptoSuperiorDireito = ptoSuperiorDireito;
            geraPtosRetangulo();
        }

        private void geraPtosRetangulo()
        {
            base.PontosRemoverTodos();
            base.PontosAdicionar(ptoInferiorEsquerdo);
            base.PontosAdicionar(new Ponto4D(ptoSuperiorDireito.X, ptoInferiorEsquerdo.Y));
            base.PontosAdicionar(ptoSuperiorDireito);
            base.PontosAdicionar(new Ponto4D(ptoInferiorEsquerdo.X, ptoSuperiorDireito.Y));
        }

        public void MoverPtoSuperiorDireito(Ponto4D ptoMover)
        {
            ptoSuperiorDireito = ptoMover;
            geraPtosRetangulo();
        }
    }
}