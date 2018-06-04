using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Enables fields of the TrainingSet class in the inspector.
[System.Serializable]
public class TrainingSet
{
    #region Fields

    [SerializeField]
    private double[] inputs;
    [SerializeField]
    private double expectedOutput;
    #endregion

    #region Properties

    public double[] Inputs
    {
        get { return inputs; }
        private set { inputs = value; }
    }

    public double ExpectedOutput
    {
        get
        {
            return expectedOutput;
        }

        private set
        {
            expectedOutput = value;
        }
    }
    #endregion
}

public class Perceptron : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private TrainingSet[] trainingSets;
    // There have to be as many weights as inputs.
    private double[] weights = { 0, 0 };
    private double bias = 0;
    // Keeps track of each epoch's errors.
    // The difference between the desired output and the actual output from the perceptron.
    private double totalError = 0;
    #endregion

    // Use this for initialization
    private void Start () 
	{
        Train(8);

        // Testing cases of or-statement:
        Debug.Log("Test 0 0: " + CalculateOutput(0, 0));
        Debug.Log("Test 0 1: " + CalculateOutput(0, 1));
        Debug.Log("Test 1 0: " + CalculateOutput(1, 0));
        Debug.Log("Test 1 1: " + CalculateOutput(1, 1));
    }

    private void Train (int epochs)
    {
        InitialiseWeightsAndBias();

        for(var e = 0; e < epochs; e++)
        {
            totalError = 0;

            for (var t = 0; t < trainingSets.Length; t++)
            {
                UpdateWeights(t);
                Debug.Log("weight 1: " + weights[0] + " weight 2: " + weights[1] + " bias: " + bias);
            }
            Debug.Log("TOTAL ERROR: " + totalError);
        }
    }

    private void InitialiseWeightsAndBias ()
    {
        for(var i = 0; i < weights.Length; i++)
        {
            weights[i] = Random.Range(-1f, 1f);
        }
        bias = Random.Range(-1f, 1f);
    }

    private void UpdateWeights (int line)
    {
        double error = trainingSets[line].ExpectedOutput - CalculateOutput(line);
        totalError += Mathf.Abs((float)error);

        for (var i = 0; i < weights.Length; i++)
        {
            weights[i] = weights[i] + error * trainingSets[line].Inputs[i];
        }
        bias += error;
    }

    // Activation function.
    // For training.
    private double CalculateOutput (int line)
    {
        double dotProductPlusBias = DotProductPlusBias(weights, trainingSets[line].Inputs);

        if (dotProductPlusBias > 0)
            return 1d;

        return 0d;
    }

    // Activation function with given inputs.
    // For testing.
    private double CalculateOutput (double input1, double input2)
    {
        double[] inputs = new double[] { input1, input2 };
        double dotProductPlusBias = DotProductPlusBias(weights, inputs);

        if (dotProductPlusBias > 0)
            return 1;
        return 0;
    }

    private double DotProductPlusBias (double[] weights, double[] inputs)
    {
        if (weights == null || inputs == null)
            return -1;

        if (weights.Length != inputs.Length)
            return -1;

        double dotProductPlusBias = 0d;

        for (var x = 0; x < weights.Length; x++)
        {
            dotProductPlusBias += weights[x] * inputs[x];
        }
        dotProductPlusBias += bias;
        return dotProductPlusBias;
    }
}
