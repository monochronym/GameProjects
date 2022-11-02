using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
public class NoSolutionException : Exception
{
    public NoSolutionException(string message) : base(message)
    { }

    public NoSolutionException(double[][] initialMatrix, double[] freeMembers, double[][] matrixAfterSolve)
        : base(GetMessage(initialMatrix, freeMembers, matrixAfterSolve))
    { }

    private static string GetMessage(double[][] sourceMatrix, double[] freeMembers, double[][] solvedMatrix)
    {
        var builder = new StringBuilder();
        builder.Append("Initial matrix:" + Environment.NewLine + sourceMatrix.FormatMatrix() + Environment.NewLine);
        builder.Append("Free members: [" + string.Join(", ", freeMembers) + "]" + Environment.NewLine);
        builder.Append("Matrix after Solve:" + Environment.NewLine + solvedMatrix.FormatMatrix());
        return builder.ToString();
    }
}
public static class Extensions
{
    public static string FormatMatrix(this double[][] matrix)
    {
        return string.Join(Environment.NewLine, matrix.Select(row => string.Join("\t", row)));
    }
}
public class Solver
{
    public static double[] MatrixIs1x1(double[][] matrix, double[] freeMembers)
    {
        for (int i = 0; i < matrix.Length; i++)
        {
            if (freeMembers[i] != 0 && matrix[i][0] != 0)
            {
                freeMembers[i] /= matrix[i][0];
                matrix[i][0] = matrix[i][0] / matrix[i][0];
            }
        }
        if (matrix.All(x => x.Length == 1) && !freeMembers.All(x => x == freeMembers[0])) throw new NoSolutionException("");
        return freeMembers;
    }
    public double[] Solve(double[][] matrix, double[] freeMembers)
    {
        double eps = 1E-10;
        int rowCount = matrix.Length;
        int columsCount = matrix[0].Length;

        if (matrix.Length != columsCount)
            if (matrix.All(x => x.Length == 1))
            {
                return MatrixIs1x1(matrix, freeMembers);
            }
            else if (matrix.Length < columsCount)
            {
                var mtxLength = matrix.Length;
                for (int i = 0; i < matrix[0].Length - mtxLength; i++)
                {
                    matrix = matrix.Append(new double[matrix[0].Length]).ToArray();
                    freeMembers = freeMembers.Append(0).ToArray();
                }
            }

        List<double[]> listOfMtx = matrix.ToList();
        List<int> indexesOfRemovedRows = new List<int>();
        for (int i = 0; i < columsCount; i++)
        {
            var tempRow = listOfMtx.FirstOrDefault(x => Math.Abs(x[i]) > eps);
            if (tempRow == null) continue;
            var tempNum = tempRow[i];
            var matrixIndex = matrix.ToList().IndexOf(tempRow);
            if (tempNum != 0)
            {
                matrix[matrixIndex] = matrix[matrixIndex].Select(x => (double)(x / tempNum)).ToArray();
                tempRow = tempRow.Select(x => (double)(x / tempNum)).ToArray();
                freeMembers[matrixIndex] = freeMembers[matrixIndex] / tempNum;
            }
            for (var j = 0; j < matrix.Length; j++)
            {
                var tempFirstElem = -matrix[j][i];
                if (matrixIndex == j) continue;
                for (var k = 0; k < matrix[j].Length; k++) matrix[j][k] += tempRow[k] * tempFirstElem;
                freeMembers[j] += freeMembers[matrixIndex] * tempFirstElem;
            }
            indexesOfRemovedRows.Add(matrixIndex);
            listOfMtx = matrix.ToList();
            listOfMtx = listOfMtx.Where(x => !indexesOfRemovedRows.Contains(listOfMtx.IndexOf(x))).ToList();
        }

        for (int i = 0; i < matrix[0].Length; i++)
        {
            if (matrix[i].All(x => Math.Abs(x) < eps))
                if (Math.Abs(freeMembers[i]) > eps)
                    throw new NoSolutionException("");
                else
                {
                    for (var j = i + 1; j < matrix.Length; j++)
                        if (matrix[j].Any(x => Math.Abs(x) > eps))
                        {
                            (matrix[i], matrix[j]) = (matrix[j], matrix[i]);
                            (freeMembers[i], freeMembers[j]) = (freeMembers[j], freeMembers[i]);
                        }
                }
            if (matrix[i][i] != 1)
            {
                for (var j = 0; j < matrix.Length; j++)
                    if (matrix[j][i] == 1)
                    {
                        (matrix[i], matrix[j]) = (matrix[j], matrix[i]);
                        (freeMembers[i], freeMembers[j]) = (freeMembers[j], freeMembers[i]);
                    }
            }
        }
        return freeMembers;
    }
}
public class GameMechanics: MonoBehaviour
{
    public TextMeshProUGUI salt;
    public TextMeshProUGUI salt1;
    public TextMeshProUGUI salt2;
    public TextMeshProUGUI salt3;
    public TMP_InputField Input; 
    public TMP_InputField Input1;
    public TMP_InputField Input2;
    public TMP_InputField Input3;
    public TextMeshProUGUI Textpoints;
    double[] coefs;
    public int points;
    public static double[][] CreateMatrix(params Salt[] salts)
    {
        double[][] matrix = new double[4][] {new double[] {salts[0].metal.coef,0,-salts[2].metal.coef,0},
            new double[] {0,salts[1].metal.coef,0,-salts[3].metal.coef}
            ,new double[] {salts[0].oxide.coef,0,0,-salts[3].oxide.coef}
            ,new double[] {0,salts[1].oxide.coef,-salts[2].oxide.coef,0}};
        return matrix;
    }

    public double[] CreateEquals(ref bool flag)
    {
        var rnd = new System.Random();
        var solver = new Solver();
        var panel = new TableOfDissolve();
        (int indexmet, int indexoxi) = (rnd.Next(0, panel.MetalIons.Length - 1), rnd.Next(0, panel.OxideIons.Length - 1));
        (int indexmet1, int indexoxi1) = (rnd.Next(0, panel.MetalIons.Length - 1), rnd.Next(0, panel.OxideIons.Length - 1));
        Salt[] saltMass = new Salt[4] {
        new Salt(new Ion(panel.MetalIons[indexmet]), new Ion(panel.OxideIons[indexoxi])),
        new Salt(new Ion(panel.MetalIons[indexmet1]), new Ion(panel.OxideIons[indexoxi1])),
        new Salt(new Ion(panel.MetalIons[indexmet]), new Ion(panel.OxideIons[indexoxi1])),
        new Salt(new Ion(panel.MetalIons[indexmet1]), new Ion(panel.OxideIons[indexoxi])),
            };
        var matrix = CreateMatrix(saltMass);
        solver.Solve(matrix, new double[] { 0, 0, 0, 0, 0 });
        var saltCoefs = new double[4];
        for (int i = 0; i < saltCoefs.Length; i++) saltCoefs[i] = -matrix[i][3];
        saltCoefs[3] = 1;
        saltCoefs = saltCoefs.Select(x => Math.Round(x / saltCoefs.Min())).ToArray();
        var saltStrings = saltMass.Select(x => x.ToString(panel.DissolvePanel)).ToArray();
        List<TextMeshProUGUI> saltText = new List<TextMeshProUGUI> { salt, salt1, salt2, salt3 };
        for (var i = 0; i < saltStrings.Length; i++)
            saltText[i].text = saltStrings[i];
        if (saltMass[0].SaltCondition(panel.DissolvePanel, "Р") && saltMass[1].SaltCondition(panel.DissolvePanel, "Р") && saltMass.Any(x => x.SaltCondition(panel.DissolvePanel, "Н"))
            && !saltCoefs.All(x => x == 1))
            flag = true;
        return saltCoefs;
    }
    public void Start()
    {
        bool flag = false;
        while(!flag)
            coefs = CreateEquals(ref flag);
    }
    public void OnMouseDown()
    {
        List<TMP_InputField> inputFields = new List<TMP_InputField>() { Input, Input1, Input2, Input3};
        string [] tempCoefs = coefs.Select(x => x.ToString()).ToArray();
        if (inputFields.All(x => tempCoefs.Contains(x.text)))
        {
            points++;
            Textpoints.text = $"Очки:{points}";
            foreach(var field in inputFields)
            {
                field.Select();
                field.text = "";
            }
            bool flag = false;
            while (!flag)
                coefs = CreateEquals(ref flag);
        }

    }
}
