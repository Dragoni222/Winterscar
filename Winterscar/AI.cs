using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace Winterscar;

public class AI
{
    private List<Matrix> Weights;
    private List<Vector> Biases;
    private List<int> LayerData { get; }
    
    public AI(List<int> layerData, Random random, double weightRange, double biasRange)
    {
        Weights = new List<Matrix>();
        Biases = new List<Vector>();
        LayerData = layerData;


        for (int layer = 0; layer < layerData.Count - 1 ; layer++)
        {
           
            int layerNodes = layerData[layer];
            
            

            int nextLayerNodes = layerData[layer + 1];
            Biases.Add(new DenseVector(nextLayerNodes));
            Weights.Add(new DenseMatrix(nextLayerNodes,layerNodes));
            //x/width/columns is lower layer
            //y/height/rows is higher layer
            
            for (int nextNode = 0; nextNode < nextLayerNodes; nextNode++)
            {
                Biases[layer][nextNode] = (random.NextDouble() - 0.5) * biasRange;
                    
                for (int node = 0; node < layerNodes; node++)
                {
                    Weights[layer][nextNode,node] = (random.NextDouble() - 0.5) * weightRange;
                }




            }
        }
        
    }

    public override string ToString()
    {
        string final = "";

        for (int layer = 0; layer < LayerData.Count - 1; layer++)
        {
            final += $"L{layer}: \n";
            for (int node = 0; node < LayerData[layer + 1]; node++)
            {
                final += $"W{node}: ";
                for (int weight = 0; weight < LayerData[layer]; weight++)
                {
                    final += Weights[layer][node,weight] + " ";
                }
                
                final += "\n";
                final += $"B{node}: " + Biases[layer][node];
                final += "\n";
            }
        }

        return final;
    }
    
    public List<double> Ask(List<double> input)
    {
        Vector<double> currentState = new DenseVector(input.ToArray());
        Console.WriteLine("Vector in ASK: " + input);
        for (int layer = 0; layer < Weights.Count; layer++)
        {
            currentState = (Weights[layer] * currentState) + Biases[layer];
        }

        return currentState.ToList();

    }

    public NodeData AskNodeData(List<double> input)
    {
        NodeData final = new NodeData(LayerData);
        
        Vector<double> currentState = new DenseVector(input.ToArray());
        final.Nodes[0] = currentState.ToList();
        for (int layer = 0; layer < Weights.Count; layer++)
        {
            currentState = (Weights[layer] * currentState) + Biases[layer];
            final.Nodes[layer + 1] = currentState.ToList();
        }

        return final;
    }



    public void Train(NodeData nodeData, List<double> expectedOutput, double step)
    {
        
        
        List<List<double>> nodes = nodeData.Nodes;
        List<double> upperDCDA = new List<double>();
        List<double> DCDA = new List<double>();
        for (int upper = 0; upper < nodes[^1].Count; upper++)
        {
            DCDA.Add(2 * (nodes[^1][upper] - expectedOutput[upper]));
            for (int lower = 0; lower < nodes[^2].Count; lower++)
            {
                Weights[^1][upper, lower] -=
                   DCDA[upper] * nodes[^2][lower] * step;
            }
        }


        for (int layer = LayerData.Count - 2; layer > 0; layer--)
        {
            upperDCDA = DCDA.ToList();
            DCDA = new List<double>();
            
            double currentDCDA = 0;
            for (int lower = 0; lower < nodes[layer - 1].Count; lower++)
            {
                for (int upper = 0; upper < nodes[layer].Count; upper++)
                {
                    currentDCDA += Weights[layer - 1][upper, lower] * upperDCDA[upper];
                }
                DCDA.Add(currentDCDA);
                
                
            }
            //seperate loops because we need all the dcda before continuing
            for (int lower = 0; lower < nodes[layer - 1].Count; lower++)
            {
                for (int upper = 0; upper < nodes[layer].Count; upper++)
                {
                  
                    Weights[layer - 1][upper, lower] -=
                        DCDA[lower] * nodes[layer - 1][lower] * step;
                }
            }
            
            
        }
        
        
        
        
    }
    
    
    
    

    public List<double> Ask(List<int> input)
    {
        return Ask(input.Select(i => (double)i).ToList());
    }
    
    public NodeData AskNodeData(List<int> input)
    {
        return AskNodeData(input.Select(i => (double)i).ToList());
    }
        
    
        
        
        
}

public class NodeData
{
    public List<List<double>> Nodes;
    
    public NodeData(List<int> layerData)
    {
        Nodes = new List<List<double>>();
        for (int i = 0; i < layerData.Count; i++)
        {
            Nodes.Add(new List<double>());
        }
    }
    
    
    
    
}






