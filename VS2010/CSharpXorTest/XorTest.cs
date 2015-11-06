using System;
using FANNCSharp;

#if FANN_FIXED
using NeuralNet = FANNCSharp.NeuralNetFixed;
using TrainingData = FANNCSharp.TrainingDataFixed;
using DataType = System.Int32;
#elif FANN_DOUBLE
using NeuralNet = FANNCSharp.NeuralNetDouble;
using TrainingData = FANNCSharp.TrainingDataDouble;
using DataType = System.Double;
#else
using NeuralNet = FANNCSharp.NeuralNetFloat;
using TrainingData = FANNCSharp.TrainingDataFloat;
using DataType = System.Single;
#endif

namespace CSharpXorTest
{
    class XorTest
    {
        static int Main(string[] args)
        {
            using (NeuralNet net = new NeuralNet())
            {
                if (!net.CreateFromFile("..\\..\\examples\\xor_float.net"))
                {
                    Console.WriteLine("Error creating ann --- ABORTING.\n");
                    return -1;
                }

                net.PrintConnections();
                net.PrintParameters();

                Console.WriteLine("Testing network.");

                using (TrainingData data = new TrainingData())
                {
                    if (!data.ReadTrainFromFile("..\\..\\examples\\xor.data"))
                    {
                        Console.WriteLine("Error reading training data --- ABORTING.\n");
                        return -1;
                    }
                    DataType[][] inputs = data.GetInput();
                    DataType[][] outputs = data.GetOutput();
                    for (int i = 0; i < data.LengthTrainData(); i++)
                    {
                        net.ResetMSE();
                        DataType[] calc_out = net.Test(inputs[i], inputs[i]);

                        Console.WriteLine("XOR test ({0}, {1}) -> {2}, should be {3}, difference={4}",
                            inputs[i][0],
                            inputs[i][1],
                            calc_out[0],
                            outputs[i][0],
                            calc_out[0] - outputs[i][0]);
                    }
                }
            }
            Console.WriteLine("Cleaning up.");
            Console.ReadKey();
            return 0;
        }
        static float fann_abs(float value)
        {
            return (((value) > 0) ? (value) : -(value));
        }
    }

}
