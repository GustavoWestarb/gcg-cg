using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using CG_Biblioteca;

namespace gcgcg
{
	internal abstract class Objeto
	{
		private static Transformacao4D matrizTmpTranslacao = new Transformacao4D();
		private Transformacao4D _matrizTransformacao;
		private BBox _bBox;
		private PrimitiveType _primitivaTipo;
		private float _primitivaTamanho = 2;
		protected PrimitiveType PrimitivaTipo { get => _primitivaTipo; set => _primitivaTipo = value; }
		protected float PrimitivaTamanho { get => _primitivaTamanho; set => _primitivaTamanho = value; }
		protected string Rotulo;
	    public BBox BBox { get => _bBox; set => _bBox = value; }
		public List<ObjetoAramado> ObjetosLista = new List<ObjetoAramado>();

		public Objeto(string rotulo)
		{
			Rotulo = rotulo;
			_bBox = new BBox();
			_matrizTransformacao = new Transformacao4D();
			_primitivaTipo = PrimitiveType.LineLoop;
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

		/// <summary>
		/// Desenha a cena.
		/// </summary>
		public void Desenhar()
		{
			GL.PushMatrix();
			GL.MultMatrix(_matrizTransformacao.ObterDados());
			DesenharAramado();

			for (var i = 0; i < ObjetosLista.Count; i++)
			{
				ObjetosLista[i].Desenhar();
			}

			GL.PopMatrix();
		}

		/// <summary>
		/// adciona um filho ao objeto atual
		/// </summary>
		/// <param name="filho"> Objeto Filho </param>
		public void FilhoAdicionar(ObjetoAramado filho)
		{
			this.ObjetosLista.Add(filho);
		}

		/// <summary>
		/// remove um filho do objeto atual
		/// </summary>
		/// <param name="filho"> Objeto Filho </param>
		public void FilhoRemover(ObjetoAramado filho)
		{
			this.ObjetosLista.Remove(filho);
		}

		/// <summary>
		/// Exibe os pontos do objeto atual em cena
		/// </summary>
		public void PontosExibirObjeto()
		{
			PontosExibir();
		}

		/// <summary>
		/// desenha a BBox para o objeto atual
		/// </summary>
		public void DesenharBB()
		{
			_bBox.Desenhar();
		}

		public void AtribuirMatrizIdentidade()
		{
			_matrizTransformacao.AtribuirIdentidade();
		}

		/// <summary>
		/// Aplica o efeito de translação no objeto atual
		/// </summary>
		/// <param name="x"> Intensidade de translação no eixo das abscissas </param>
		/// <param name="y"> Intensidade de translação no eixo das ordenadas </param>
		public void Translacao(double x, double y)
		{
			matrizTmpTranslacao.AtribuirTranslacao(x, y, 0);
			_matrizTransformacao = matrizTmpTranslacao.MultiplicarMatriz(_matrizTransformacao);
		}

		/// <summary>
		/// Adiciona a escala informada ao objeto atual
		/// </summary>
		/// <param name="escala"> Escala: deve ser positiva para aumentar o objeto, ou negativa para diminui-lo </param>
		public void Escala(double escala)
		{
			var matrixAuxRotacao = new Transformacao4D();
			var pontoCentroBB = _bBox.obterCentro;

			var matrixTrans = new Transformacao4D();
			matrixTrans.AtribuirTranslacao(-pontoCentroBB.X, -pontoCentroBB.Y, -pontoCentroBB.Z);
			matrixAuxRotacao = matrixTrans.MultiplicarMatriz(matrixAuxRotacao);

			var matrixAuxRot = new Transformacao4D();
			matrixAuxRot.AtribuirEscala(escala, escala, 1.0);
			matrixAuxRotacao = matrixAuxRot.MultiplicarMatriz(matrixAuxRotacao);

			var matrixAuxTransInv = new Transformacao4D();
			matrixAuxTransInv.AtribuirTranslacao(pontoCentroBB.X, pontoCentroBB.Y, pontoCentroBB.Z);
			matrixAuxRotacao = matrixAuxTransInv.MultiplicarMatriz(matrixAuxRotacao);

			_matrizTransformacao = _matrizTransformacao.MultiplicarMatriz(matrixAuxRotacao);
		}

		/// <summary>
		/// Rotaciona o objeto no angulo informado  
		/// </summary>
		/// <param name="escala"> angulo em que deve ser rotacionado, postivo para sentido horário e negativo anti-horário </param>
		public void Rotacao(double angulo)
		{
			var matrixAuxRotacao = new Transformacao4D();
			var pontoCentroBB = _bBox.obterCentro;

			var matrixTrans = new Transformacao4D();
			matrixTrans.AtribuirTranslacao(-pontoCentroBB.X, -pontoCentroBB.Y, -pontoCentroBB.Z);
			matrixAuxRotacao = matrixTrans.MultiplicarMatriz(matrixAuxRotacao);

			var matrixAuxRot = new Transformacao4D();
			matrixAuxRot.AtribuirRotacaoZ(Transformacao4D.DEG_TO_RAD * angulo);
			matrixAuxRotacao = matrixAuxRot.MultiplicarMatriz(matrixAuxRotacao);

			var matrixAuxTransInv = new Transformacao4D();
			matrixAuxTransInv.AtribuirTranslacao(pontoCentroBB.X, pontoCentroBB.Y, pontoCentroBB.Z);
			matrixAuxRotacao = matrixAuxTransInv.MultiplicarMatriz(matrixAuxRotacao);

			_matrizTransformacao = _matrizTransformacao.MultiplicarMatriz(matrixAuxRotacao);
		}

		protected abstract void DesenharAramado();
		protected abstract void PontosExibir();
	}
}