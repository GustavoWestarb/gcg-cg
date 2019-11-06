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

        public Desenho(string rotulo) : base(rotulo){}

        /// <summary>
        /// Função utilizada para adcionar um ponto ao poligono atual, chama a função <c>AdcionarPonto</c> e <c>DesenharAramado</c> da super classe
        /// que adiciona os pontos na lista <c>base.pontosLista</c> da super classe e desenha os pontos em tela utilizando o openTK
        /// </summary>
        /// <param name="ponto">Ponto a ser adcionado</param>

        public void AdicionarPonto(Ponto4D ponto)
        {
            base.PontosAdicionar(ponto);
            base.DesenharAramado();
        }

        /// <summary>
        /// Recebe vários pontos e os agrega na lista de pontos deste poligono
        /// </summary>
        /// <param name="ponto">Ponto a ser salvo</param>
        private void SalvarPontos()
        {
            this._pontos.Clear();
            this._pontos.AddRange(base.pontosLista);
        }

        /// <summary>
        /// Delega os pontos da lista de pontos desse poligono para pontosLista da super classe
        /// </summary>
        private void AtribuirPontosSalvos()
        {
            base.pontosLista.AddRange(this._pontos);
        }

        /// <summary>
        /// adiciona um ponto na ultima posição da lista
        /// </summary>
        /// <param name="ponto">Ponto a ser adcionado</param>
        public void MoverUltimoPonto(Ponto4D ponto)
        {
            base.pontosLista[base.pontosLista.Count - 1] = ponto;
        }

        /// <summary>
        /// Remove o ponto informado da <c>base.pontosLista</c>, recria o poligono e redesenha ele.
        /// </summary>
        /// Veja <see cref="Desenho.Redesenhar()"/> Para redesenhar o poligono
        /// <param name="ponto">Ponto a ser removido</param>
        public void RemoverPonto(Ponto4D ponto)
        {
            base.pontosLista.Remove(ponto);
            Redesenhar();
        }


        /// <summary>
        /// Chama Funções da classe base que servem para Atualizar a cena do OpenTK
        /// </summary>
        /// Apaga e redesenha todos os pontos da tela para esse poligono
        public void Redesenhar()
        {
            SalvarPontos();
            base.PontosRemoverTodos();
            AtribuirPontosSalvos();
            base.DesenharAramado();
        }

        /// <summary>
        /// Conta quantos pontos estão na <c>base.pontosLista</c>
        /// </summary>
        public int RetornarQuantidadePontos()
        {
            return base.pontosLista.Count;
        }

        /// <summary>
        /// Getter de <c>base.pontosLista</c>
        /// </summary>
        /// <returns>Lista com todos os pontos desse poligono</returns>
        public List<Ponto4D> RetornaListaPontos()
        {
            return base.pontosLista;
        }

        /// <summary>
        /// Muda a cor do poligono
        /// </summary>
        /// <param name="cor">Nova cor</param>
        public void AtualizarCor(Color cor)
        {
            Cor = cor;
        }

        /// <summary>
        /// Alterna o tipo da primitiva entre <c>PrimitiveType.LineStrip</c> e <c>PrimitiveType.LineLoop</c>
        /// </summary>
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