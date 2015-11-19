using System;
using FANNCSharp;
#if FANN_FIXED
using FANNCSharp.Fixed;
using DataType = System.Int32;
#elif FANN_DOUBLE
using FANNCSharp.Double;
using DataType = System.Double;
#else
using FANNCSharp.Float;
using DataType = System.Single;
#endif

namespace Example
{
    class XorTest
    {
        static int Main(string[] args)
        {
            using (NeuralNet net = new NeuralNet("..\\..\\examples\\xor_float.net"))
            {
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
                    for (int i = 0; i < data.TrainDataLength; i++)
                    {
                        net.ResetMSE();
                        DataType[] calc_out = net.Test(data.GetTrainInput((uint)i), data.GetTrainOutput((uint)i));

                        Console.WriteLine("XOR test ({0}, {1}) -> {2}, should be {3}, difference={4}",
                            data.GetTrainInput((uint)i)[0],
                            data.GetTrainInput((uint)i)[1],
                            calc_out[0],
                            data.GetTrainOutput((uint)i)[0],
                            calc_out[0] - data.GetTrainOutput((uint)i)[0]);
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
