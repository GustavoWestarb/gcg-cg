using System;
using System.Collections.Generic;
using System.Drawing;
using CG_Biblioteca;
using OpenTK.Graphics.OpenGL;

namespace gcgcg
{
    internal class Desenho : ObjetoAramado
    {
        private List<Ponto4D> _pontos = new List<Ponto4D>();

        public List<Ponto4D> Pontos
        {
            get => pontosLista;
        }

        public Desenho(string rotulo) : base(rotulo)
        {
            base.Desenhar();
        }

        public void AdicionarPonto(Ponto4D ponto)
        {
            base.PontosAdicionar(ponto);
            base.DesenharAramado();
        }

        private void SalvarPontos()
        {
            this._pontos.Clear();
            this._pontos.AddRange(base.pontosLista);
        }

        private void AtribuirPontosSalvos()
        {
            base.pontosLista.AddRange(this._pontos);
        }

        public void MoverUltimoPonto(Ponto4D ponto)
        {
            base.pontosLista[base.pontosLista.Count - 1] = ponto;
        }

        public void RemoverPonto(Ponto4D ponto)
        {
            base.pontosLista.Remove(ponto);
            SalvarPontos();
            base.PontosRemoverTodos();
            AtribuirPontosSalvos();
            base.DesenharAramado();
        }

        public void Redesenhar()
        {
            SalvarPontos();
            base.PontosRemoverTodos();
            AtribuirPontosSalvos();
            base.DesenharAramado();
        }

        public int RetornarQuantidadePontos()
        {
            return base.pontosLista.Count;
        }

        public List<Ponto4D> RetornaListaPontos()
        {
            return base.pontosLista;
        }

        public void AtualizarCor(Color cor)
        {
            Cor = cor;
        }

        public void AlterarPrimitiva()
        {
            switch (PrimitivaTipo)
            {
                case PrimitiveType.LineLoop:
                    PrimitivaTipo = PrimitiveType.LineStrip;
                    break;
                case PrimitiveType.LineStrip:
                    PrimitivaTipo = PrimitiveType.LineLoop;
                    break;
            }
        }
    }
}