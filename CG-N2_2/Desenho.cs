using System;
using System.Collections.Generic;
using System.Drawing;
using CG_Biblioteca;
using OpenTK.Graphics.OpenGL;

namespace gcgcg
{
    internal class Desenho : ObjetoAramado
    {
        List<Ponto4D> pontos = new List<Ponto4D>();

        public Desenho(string rotulo) : base(rotulo)
        {
            base.Desenhar();
        }

        public void AdicionarPonto(Ponto4D ponto)
        {
            base.PontosAdicionar(ponto);
            base.PontosAdicionar(ponto);
            base.DesenharAramado();
        }

        private void SalvarPontos()
        {
            this.pontos.Clear();
            this.pontos.AddRange(base.pontosLista);
        }

        private void AtribuirPontosSalvos()
        {
            base.pontosLista.AddRange(this.pontos);
        }

        public void MoverUltimoPonto(Ponto4D ponto)
        {
            base.pontosLista[base.pontosLista.Count - 1] = ponto;
        }

        public void RemoverPonto(Ponto4D ponto)
        {
            base.pontosLista.RemoveAt(base.pontosLista.Count - 1);
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

        public void AlterarValorCorPosicaoVermelhar()
        {
            if (ValorCorPosicaoVermelha >= 255)
            {
                ValorCorPosicaoVermelha = 0;
            }
            else
            {
                ValorCorPosicaoVermelha++;
            }

            Redesenhar();
        }

        public void AlterarValorCorPosicaoVerde()
        {
            if (ValorCorPosicaoVerde >= 255)
            {
                ValorCorPosicaoVerde = 0;
            }
            else
            {
                ValorCorPosicaoVerde++;
            }

            Redesenhar();
        }

        public void AlterarValorCorPosicaoAzul()
        {
            if (ValorCorPosicaoAzul >= 255)
            {
                ValorCorPosicaoAzul = 0;
            }
            else
            {
                ValorCorPosicaoAzul++;
            }

            Redesenhar();
        }

        public void AlterarPrimitiva(bool primeiraVerificacao)
        {
            if (primeiraVerificacao)
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
}