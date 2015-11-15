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
namespace Example
{
    class ScalingTest
    {
        static void Main()
        {
            DataType[] calc_out;
            Console.WriteLine("Creating network.");

            using(NeuralNet net = new NeuralNet("..\\..\\datasets\\scaling.net"))
            {
                net.PrintConnections();
                net.PrintParameters();
                Console.WriteLine("Testing network.");
                using(TrainingData data = new TrainingData())
                {
                    data.ReadTrainFromFile("..\\..\\datasets\\scaling.data");
                    for(int i = 0; i < data.TrainDataLength; i++)
                    {
                        net.ResetMSE();
                        net.ScaleInput(data.Input[i]);
                        calc_out = net.Run(data.Input[i]);
                        net.DescaleOutput(calc_out);
                        Console.WriteLine("Result {0} original {1} error {2}", calc_out[0], data.Output[i][0],
                                          FannAbs(calc_out[0] - data.Output[i][0]));
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
