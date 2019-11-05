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
    private List<Objeto> objetosLista = new List<Objeto>();

    private Transformacao4D matrix = new Transformacao4D();

    private static Transformacao4D matrizTmpTranslacao = new Transformacao4D();
    private static Transformacao4D matrizTmpTranslacaoInversa = new Transformacao4D();
    private static Transformacao4D matrizTmpEscala = new Transformacao4D();
    private static Transformacao4D matrizTmpRotacao = new Transformacao4D();
    private static Transformacao4D matrizGlobal = new Transformacao4D();

    public Objeto(string rotulo)
    {
      this.rotulo = rotulo;
    }

    protected PrimitiveType PrimitivaTipo { get => primitivaTipo; set => primitivaTipo = value; }
    protected float PrimitivaTamanho { get => primitivaTamanho; set => primitivaTamanho = value; }

    public void Desenhar()
    {
      GL.PushMatrix();
      GL.MultMatrix(matrix.ObterDados());
      DesenharAramado();

      for (var i = 0; i < objetosLista.Count; i++)
      {
        objetosLista[i].Desenhar();
      }

      GL.PopMatrix();
    }

    protected abstract void DesenharAramado();
    public void FilhoAdicionar(Objeto filho)
    {
      this.objetosLista.Add(filho);
    }
    public void FilhoRemover(Objeto filho)
    {
      this.objetosLista.Remove(filho);
    }
    protected abstract void PontosExibir();
    public void PontosExibirObjeto()
    {
      PontosExibir();
    }
    public void DesenharBB()
    {
      bBox.Desenhar();
    }

    public void AtribuirMatrizIdentidade()
    {
      matrix.AtribuirIdentidade();
    }
    public void Translacao(double x, double y)
    {
      var matrixAux = new Transformacao4D();
      matrixAux.AtribuirTranslacao(x, y, 0);
      matrix = matrixAux.MultiplicarMatriz(matrix);
    }

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

      matrix = matrix.MultiplicarMatriz(matrixAuxRotacao); 
    }

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

      matrix = matrix.MultiplicarMatriz(matrixAuxRotacao);
    }
  }
}