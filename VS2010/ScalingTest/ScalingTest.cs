using System;
using FANNCSharp;
using FannWrapper;
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
namespace Example
{
    class ScalingTest
    {
        static void Main()
        {
            DataType[] calc_out;
            Console.WriteLine("Creating network.");

            using(NeuralNet net = new NeuralNet())
            {
                if(!net.CreateFromFile("..\\..\\datasets\\scaling.net"))
                {
                    Console.WriteLine("Error creating NeuralNet --- ABORTING.");
                }
                net.PrintConnections();
                net.PrintParameters();
                Console.WriteLine("Testing network.");
                using(TrainingData data = new TrainingData())
                {
                    data.ReadTrainFromFile("..\\..\\datasets\\scaling.data");
                    DataType[][] input = data.GetInput();
                    DataType[][] output = data.GetOutput();
                    for(int i = 0; i < data.LengthTrainData(); i++)
                    {
                        net.ResetMSE();
                        net.ScaleInput(input[i]);
                        calc_out = net.Run(input[i]);
                        net.DescaleOutput(calc_out);
                        Console.WriteLine("Result {0} original {1} error {2}", calc_out[0], output[i][0],
                                          FannAbs(calc_out[0] - output[i][0]));
                    }
                    Console.ReadKey();
                }
            }
        }
        static float FannAbs(float value)
        {
            return (((value) > 0) ? (value) : -(value));
        }
    }
}
