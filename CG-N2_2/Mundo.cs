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

        public static Mundo getInstance(int width, int height)
        {
            if (instance == null)
                instance = new Mundo(width, height);
            return instance;
        }

        private Camera camera = new Camera();
        protected List<Desenho> objetosLista = new List<Desenho>();
        private Desenho _novoDesenho;
        private Ponto4D _pontoSelecionado;
        private int _indiceSelecionado = -1;
        private bool _desenharBB;

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
                if(i == _indiceSelecionado && _desenharBB) 
                {
                    objetosLista[i].DesenharBB();
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
                case Key.Space:
                    _novoDesenho = null;
                    _pontoSelecionado = null;
                    _indiceSelecionado = -1;
                    _desenharBB = false;
                    break;
                case Key.R:
                    _novoDesenho.AlterarCor(Color.Red);
                    break;
                case Key.G:
                    _novoDesenho.AlterarCor(Color.Green);
                    break;
                case Key.B:
                    _novoDesenho.AlterarCor(Color.Blue);
                    break;
                case Key.A:
                    _desenharBB = true;
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
                if (_novoDesenho == null)
                {
                    _novoDesenho = new Desenho("A");
                    objetosLista.Add(_novoDesenho);
                }

                _pontoSelecionado = new Ponto4D(e.Position.X, 600 - e.Position.Y, 0);
                _novoDesenho.AdicionarPonto(_pontoSelecionado);
            }
            else if (e.Button == MouseButton.Right)
            {
                if (_novoDesenho == null)
                {
                    var listaPontos = objetosLista
                                         .ConvertAll(x => x.Pontos)
                                         .SelectMany(x => x)
                                         .ToList();
                    
                     _pontoSelecionado = listaPontos.OrderBy(v => Math.Abs(v.Y - (600 - e.Position.Y)) + Math.Abs(v.X - e.Position.X)).First();


                    for (int i = 0; i < objetosLista.Count(); i ++) {
                        if(objetosLista[i].Pontos.Any(x => x == _pontoSelecionado)) {
                            _indiceSelecionado = i;
                        }
                    }
                }
            }
        }

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
            window.Title = "CG-N2_2";
            window.Run(1.0 / 60.0);
        }
    }

}