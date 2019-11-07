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
		private List<ObjetoAramado> _objetoLista = new List<ObjetoAramado>();
		private static Mundo instance = null;
		private Camera camera = new Camera();
		private ObjetoAramado _objetoEmFoco;
		private ObjetoAramado _objetoSelecionado;
		private Ponto4D _pontoSelecionado;
		private bool _desenharBB = false;
		private bool _atualizandoDesenho = false;
		private bool _buscarVerticeMaisProximo = false;

		#region Construtores
		private Mundo(int width, int height) : base(width, height) { }

		public static Mundo getInstance(int width, int height)
		{
			if (instance == null)
			{
				instance = new Mundo(width, height);
			}

			return instance;
		}
		#endregion

		#region Métodos originais da classe
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

			for (var i = 0; i < _objetoLista.Count; i++)
			{
				_objetoLista[i].Desenhar();

				if (_desenharBB && _objetoEmFoco != null)
				{
					_objetoEmFoco.DesenharBB();
				}

				if (_objetoSelecionado != null)
				{
					_objetoSelecionado.DesenharBB();
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
					Funcoes.FuncaoKeyE(_objetoLista);
					break;
				case Key.D:
					if (_pontoSelecionado != null)
					{
						_objetoSelecionado.RemoverPonto(_pontoSelecionado);
						_pontoSelecionado = null;
					}
					break;
				case Key.P:
					Funcoes.AlterarPrimitivaObjetoAtual(_objetoEmFoco);
					break;
				case Key.Enter:
					FuncaoEnter();
					break;
				case Key.R:
					_objetoSelecionado.AlterarCor(Color.Red);
					break;
				case Key.G:
					_objetoSelecionado.AlterarCor(Color.Green);
					break;
				case Key.B:
					_objetoSelecionado.AlterarCor(Color.Blue);
					break;
				case Key.A:
					_desenharBB = !_desenharBB;
					_objetoSelecionado?.AtribuirMatrizIdentidade();
					break;
				case Key.Left:
					_objetoSelecionado?.Translacao(-10, 0);
					break;
				case Key.Right:
					_objetoSelecionado?.Translacao(10, 0);
					break;
				case Key.Up:
					_objetoSelecionado?.Translacao(0, 10);
					break;
				case Key.Down:
					_objetoSelecionado?.Translacao(0, -10);

					foreach (var item in _objetoSelecionado.ObjetosLista)
					{
						item.Translacao(0, -10);
					}
					break;
				case Key.PageUp:
					_objetoSelecionado?.Escala(2);
					break;
				case Key.PageDown:
					_objetoSelecionado?.Escala(0.5);
					break;
				case Key.Number1:
					_objetoSelecionado?.Rotacao(10);
					break;
				case Key.Number2:
					_objetoSelecionado?.Rotacao(-10);
					break;
				case Key.F:
					_buscarVerticeMaisProximo = !_buscarVerticeMaisProximo;
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
			else
			if (_objetoEmFoco != null && _objetoEmFoco.RetornarQuantidadePontos() > 0)
			{
				_objetoEmFoco.MoverUltimoPonto(new Ponto4D(e.Position.X, 600 - e.Position.Y, 0));
			}
		}

		protected override void OnMouseDown(MouseButtonEventArgs e)
		{
			if (e.Button == MouseButton.Left)
			{
				if (!_atualizandoDesenho)
				{
					if (_objetoEmFoco == null)
					{
						_objetoEmFoco = new ObjetoAramado("A");

						if (_objetoSelecionado != null)
						{
							_objetoSelecionado.FilhoAdicionar(_objetoEmFoco);
						}
						else
						{
							_objetoLista.Add(_objetoEmFoco);
						}
					}

					_pontoSelecionado = new Ponto4D(e.Position.X, 600 - e.Position.Y, 0);
					_objetoEmFoco.AdicionarPonto(_pontoSelecionado);
				}
				else
				{
					FuncaoEnter();
				}
			}
			else
				if (e.Button == MouseButton.Right)
			{
				Ponto4D pontoClique = new Ponto4D(e.Position.X, 600 - e.Position.Y, 0);

				if (_buscarVerticeMaisProximo)
				{
					BuscarPontoMaisProximo(e);
				}
				else
				{
					Funcoes.SelecionarPoligno(_objetoLista, pontoClique, ref _objetoSelecionado);
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
		#endregion

		#region Métodos adicionais
		private void FuncaoEnter()
		{
			Funcoes.AtualizarBBoxObjetoAtual(_objetoEmFoco);
			_objetoEmFoco = null;
			_pontoSelecionado = null;
			_atualizandoDesenho = false;
		}

		/// <summary>
		/// Busca o ponto mais próximo de um objeto
		/// </summary>
		private void BuscarPontoMaisProximo(MouseButtonEventArgs e)
		{
			var listaPontos = _objetoLista
								.ConvertAll(x => x.RetornarListaDePontos())
								.SelectMany(x => x)
								.ToList();

			_pontoSelecionado = listaPontos?.OrderBy(v => Math.Abs(v.Y - (600 - e.Position.Y)) + Math.Abs(v.X - e.Position.X)).First();

			for (int i = 0; i < _objetoLista.Count(); i++)
			{
				if (_objetoLista[i].RetornarListaDePontos().Any(x => x == _pontoSelecionado))
				{
					_atualizandoDesenho = true;
					_objetoSelecionado = _objetoLista[i];
					_objetoEmFoco = _objetoLista[i];
				}
			}
		}
		#endregion
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