using OpenTK.Graphics.OpenGL;
using System.Collections.Generic;
using CG_Biblioteca;

namespace gcgcg
{
  internal abstract class Objeto
  {
    protected string rotulo;
    private PrimitiveType primitivaTipo = PrimitiveType.LineLoop;
    private float primitivaTamanho = 2;
    private BBox bBox = new BBox();
    public BBox BBox
    { 
      get => bBox;
      set => bBox = value;
    }
    public List<Objeto> objetosLista = new List<Objeto>();

    private Transformacao4D matriz = new Transformacao4D();

    private static Transformacao4D matrizTmpTranslacao = new Transformacao4D();

    public Objeto(string rotulo)
    {
      this.rotulo = rotulo;
    }

    protected PrimitiveType PrimitivaTipo { get => primitivaTipo; set => primitivaTipo = value; }
    protected float PrimitivaTamanho { get => primitivaTamanho; set => primitivaTamanho = value; }

    /// <summary>
    /// Desenha a cena.
    /// </summary>
    public void Desenhar()
    {
      GL.PushMatrix();
      GL.MultMatrix(matriz.ObterDados());
      DesenharAramado();

      for (var i = 0; i < objetosLista.Count; i++)
      {
        objetosLista[i].Desenhar();
      }

      GL.PopMatrix();
    }

    protected abstract void DesenharAramado();

    /// <summary>
    /// adciona um filho ao objeto atual
    /// </summary>
    /// <param name="filho"> Objeto Filho </param>
    public void FilhoAdicionar(Objeto filho)
    {
      this.objetosLista.Add(filho);
    }

    /// <summary>
    /// remove um filho do objeto atual
    /// </summary>
    /// <param name="filho"> Objeto Filho </param>
    public void FilhoRemover(Objeto filho)
    {
      this.objetosLista.Remove(filho);
    }

    protected abstract void PontosExibir();

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
      bBox.Desenhar();
    }

    public void AtribuirMatrizIdentidade()
    {
      matriz.AtribuirIdentidade();
    }


    /// <summary>
    /// Aplica o efeito de translação no objeto atual
    /// </summary>
    /// <param name="x"> Intensidade de translação no eixo das abscissas </param>
    /// <param name="y"> Intensidade de translação no eixo das ordenadas </param>
    public void Translacao(double x, double y)
    {
      matrizTmpTranslacao.AtribuirTranslacao(x, y, 0);
      matriz = matrizTmpTranslacao.MultiplicarMatriz(matriz);
    }

    /// <summary>
    /// Adiciona a escala informada ao objeto atual
    /// </summary>
    /// <param name="escala"> Escala: deve ser positiva para aumentar o objeto, ou negativa para diminui-lo </param>
    public void Escala(double escala)
    {
      var matrixAuxRotacao = new Transformacao4D();
      var pontoCentroBB = bBox.obterCentro;

      var matrixTrans = new Transformacao4D();
      matrixTrans.AtribuirTranslacao(-pontoCentroBB.X, -pontoCentroBB.Y, -pontoCentroBB.Z);
      matrixAuxRotacao = matrixTrans.MultiplicarMatriz(matrixAuxRotacao);

      var matrixAuxRot = new Transformacao4D();
      matrixAuxRot.AtribuirEscala(escala, escala, 1.0);
      matrixAuxRotacao = matrixAuxRot.MultiplicarMatriz(matrixAuxRotacao);

      var matrixAuxTransInv = new Transformacao4D();
      matrixAuxTransInv.AtribuirTranslacao(pontoCentroBB.X, pontoCentroBB.Y, pontoCentroBB.Z);
      matrixAuxRotacao = matrixAuxTransInv.MultiplicarMatriz(matrixAuxRotacao); 

      matriz = matriz.MultiplicarMatriz(matrixAuxRotacao); 
    }

    /// <summary>
    /// Rotaciona o objeto no angulo informado  
    /// </summary>
    /// <param name="escala"> angulo em que deve ser rotacionado, postivo para sentido horário e negativo anti-horário </param>
    public void Rotacao(double angulo)
    {
      var matrixAuxRotacao = new Transformacao4D();
      var pontoCentroBB = bBox.obterCentro;

      var matrixTrans = new Transformacao4D();
      matrixTrans.AtribuirTranslacao(-pontoCentroBB.X, -pontoCentroBB.Y, -pontoCentroBB.Z);
      matrixAuxRotacao = matrixTrans.MultiplicarMatriz(matrixAuxRotacao);

      var matrixAuxRot = new Transformacao4D();
      matrixAuxRot.AtribuirRotacaoZ(Transformacao4D.DEG_TO_RAD * angulo);
      matrixAuxRotacao = matrixAuxRot.MultiplicarMatriz(matrixAuxRotacao);

      var matrixAuxTransInv = new Transformacao4D();
      matrixAuxTransInv.AtribuirTranslacao(pontoCentroBB.X, pontoCentroBB.Y, pontoCentroBB.Z);
      matrixAuxRotacao = matrixAuxTransInv.MultiplicarMatriz(matrixAuxRotacao); 

      matriz = matriz.MultiplicarMatriz(matrixAuxRotacao);
    }
  }
}