using System;
using System.Collections.Generic;
using System.Drawing;
using CG_Biblioteca;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Linq;


namespace gcgcg
{
    class Mundo : GameWindow
    {
        private static Mundo instance = null;
        private Mundo(int width, int height) : base(width, height) { }
        private Camera camera = new Camera();
        protected List<Desenho> objetosLista = new List<Desenho>();
        private Desenho _novoDesenho;
        private Ponto4D _pontoSelecionado;
        private int _indiceSelecionado = -1;
        private bool _desenharBB = false;
        private bool _atualizandoDesenho = false;
        private bool _addFilho = false;

        public static Mundo getInstance(int width, int height)
        {
            if (instance == null)
            {
                instance = new Mundo(width, height);
            }
            return instance;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(Color.Gray);
        }
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(camera.xmin, camera.xmax, camera.ymin, camera.ymax, camera.zmin, camera.zmax);
        }
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();

            Sru3D();

            for (var i = 0; i < objetosLista.Count; i++)
            {
                objetosLista[i].Desenhar();

                if (_desenharBB && _novoDesenho != null)
                {
                    _novoDesenho.DesenharBB();
                }
            }

            this.SwapBuffers();
        }

        protected override void OnKeyDown(OpenTK.Input.KeyboardKeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    Exit();
                    break;
                case Key.E:
                    for (var i = 0; i < objetosLista.Count; i++)
                    {
                        objetosLista[i].PontosExibirObjeto();
                    }
                    break;
                case Key.D:
                    if (_pontoSelecionado != null)
                    {
                        objetosLista[_indiceSelecionado].RemoverPonto(_pontoSelecionado);
                        _pontoSelecionado = null;
                    }
                    break;
                case Key.P:
                    FuncoesDesenho.AlterarPrimitivaDesenhos(objetosLista);
                    break;
                case Key.Enter:
                    if (_addFilho)
                    {
                        objetosLista[0].FilhoAdicionar(_novoDesenho);
                    }
                    
                    FuncaoEnter();

                    break;
                case Key.R:
                    _novoDesenho.AtualizarCor(Color.Red);
                    break;
                case Key.G:
                    _novoDesenho.AtualizarCor(Color.Green);
                    break;
                case Key.B:
                    _novoDesenho.AtualizarCor(Color.Blue);
                    break;
                case Key.A:
                    _desenharBB = !_desenharBB;
                    _novoDesenho?.AtribuirMatrizIdentidade();
                    //_novoDesenho = objetosLista[objetosLista.Count - 1];
                    break;
                case Key.Left:
                    _novoDesenho?.Translacao(-10, 0);

                    // if (_indiceSelecionado != 1)
                    // {
                    //     objetosLista[_indiceSelecionado].Translacao(-10, 0);
                    // }

                    // foreach (var item in _novoDesenho.objetosLista)
                    // {
                    //     item.Translacao(-10, 0);
                    // }
                    break;
                case Key.Right:
                    _novoDesenho?.Translacao(10, 0);

                    // if (_indiceSelecionado != 1)
                    // {
                    //     objetosLista[_indiceSelecionado].Translacao(10, 0);
                    // }

                    // foreach (var item in _novoDesenho.objetosLista)
                    // {
                    //     item.Translacao(10, 0);
                    // }
                    break;
                case Key.Up:
                    _novoDesenho?.Translacao(0, 10);

                    // if (_indiceSelecionado != 1)
                    // {
                    //     objetosLista[_indiceSelecionado].Translacao(0, 10);
                    // }


                    // foreach (var item in _novoDesenho.objetosLista)
                    // {
                    //     item.Translacao(0, 10);
                    // }
                    break;
                case Key.Down:
                    _novoDesenho?.Translacao(0, -10);

                    // if (_indiceSelecionado != 1)
                    // {
                    //     objetosLista[_indiceSelecionado].Translacao(0, -10);
                    // }

                    foreach (var item in _novoDesenho.objetosLista)
                    {
                        item.Translacao(0, -10);
                    }
                    break;
                case Key.PageUp:
                    _novoDesenho?.Escala(2);
                    break;
                case Key.PageDown:
                    _novoDesenho?.Escala(0.5);
                    break;
                case Key.Number1:
                    _novoDesenho?.Rotacao(10);
                    break;
                case Key.Number2:
                    _novoDesenho?.Rotacao(-10);
                    break;
                case Key.F:
                    _addFilho = !_addFilho;
                    break;
            }
        }

        protected override void OnMouseMove(MouseMoveEventArgs e)
        {
            if (_pontoSelecionado != null)
            {
                _pontoSelecionado.X = e.Position.X;
                _pontoSelecionado.Y = 600 - e.Position.Y;
            }
            else if (_novoDesenho != null && _novoDesenho.RetornarQuantidadePontos() > 0)
            {
                _novoDesenho.MoverUltimoPonto(new Ponto4D(e.Position.X, 600 - e.Position.Y, 0));
            }
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            if (e.Button == MouseButton.Left)
            {
                if (!_atualizandoDesenho)
                {
                    if (_novoDesenho == null)
                    {
                        _novoDesenho = new Desenho("A");
                        objetosLista.Add(_novoDesenho);
                    }

                    _pontoSelecionado = new Ponto4D(e.Position.X, 600 - e.Position.Y, 0);
                    _novoDesenho.AdicionarPonto(_pontoSelecionado);
                    
                }
                else
                {
                    FuncaoEnter();
                }
            }
            else if (e.Button == MouseButton.Right)
            {
                if (_novoDesenho == null)
                {
                    BuscarPontoMaisProximo(e);
                }

                // VerificarPoligno(new Ponto4D(e.Position.X, 600 - e.Position.Y, 0));
            }
        }

        private void VerificarPoligno(Ponto4D ponto)
        {
            foreach (var objeto in objetosLista)
            {
                int resultado = objeto.PontoEmPoligno(ponto);

                if ((resultado % 2) == 0)
                {
                    Console.WriteLine("Fora");
                }
                else
                {
                    Console.WriteLine("Dentro");
                    _novoDesenho = objeto;
                    _novoDesenho.DesenharBB();
                }
            }
        }

        #region Função da tecla espaço
        private void FuncaoEnter()
        {
            _novoDesenho = null;
            _pontoSelecionado = null;
            _indiceSelecionado = -1;
            _atualizandoDesenho = false;
            FuncoesDesenho.AtualizarValoresBBox(objetosLista);
        }
        #endregion

        #region Método para retornar o vértice mais próximo
        private void BuscarPontoMaisProximo(MouseButtonEventArgs e)
        {
            var listaPontos = objetosLista
                                .ConvertAll(x => x.Pontos)
                                .SelectMany(x => x)
                                .ToList();

            _pontoSelecionado = listaPontos?.OrderBy(v => Math.Abs(v.Y - (600 - e.Position.Y)) + Math.Abs(v.X - e.Position.X)).First();

            for (int i = 0; i < objetosLista.Count(); i++)
            {
                if (objetosLista[i].Pontos.Any(x => x == _pontoSelecionado))
                {
                    _atualizandoDesenho = true;
                    _indiceSelecionado = i;
                    _novoDesenho = objetosLista[_indiceSelecionado];
                }
            }
        }
        #endregion

        private void Sru3D()
        {
            GL.LineWidth(1);
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Red);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(200, 0, 0);
            GL.Color3(Color.Green);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 200, 0);
            GL.Color3(Color.Blue);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 0, 200);
            GL.End();
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Mundo window = Mundo.getInstance(600, 600);
            window.Title = "TRABALHO 03";
            window.Run(1.0 / 60.0);
        }
    }
}