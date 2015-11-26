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
            int ret = 0;
#if FANN_FIXED
            using (NeuralNet net = new NeuralNet("..\\..\\..\\examples\\xor_fixed.net"))
#else
            using (NeuralNet net = new NeuralNet("..\\..\\..\\examples\\xor_float.net"))
#endif
            {
                net.PrintConnections();
                net.PrintParameters();

                Console.WriteLine("Testing network.");

                using (TrainingData data = new TrainingData())
                {
#if FANN_FIXED      
                    if (!data.ReadTrainFromFile("..\\..\\..\\examples\\xor_fixed.data"))
#else
                    if (!data.ReadTrainFromFile("..\\..\\..\\examples\\xor.data"))
#endif
                    {
                        Console.WriteLine("Error reading training data --- ABORTING.\n");
                        return -1;
                    }
                    for (int i = 0; i < data.TrainDataLength; i++)
                    {
                        net.ResetMSE();
                        DataType[] calc_out = net.Test(data.GetTrainInput((uint)i).Array, data.GetTrainOutput((uint)i).Array);
#if FANN_FIXED
                        Console.WriteLine("XOR test ({0}, {1}) - {2}, should be {3}, difference={4}",
                                            data.InputAccessor[i][0], data.InputAccessor[i][0],
                                            calc_out[0], data.OutputAccessor[i][0],
                                            (float) fann_abs(calc_out[0] - data.OutputAccessor[i][0]) / net.Multiplier);


                        if ((float)fann_abs(calc_out[0] - data.OutputAccessor[i][0]) / net.Multiplier > 0.2)
                        {
                            Console.WriteLine("Test failed");
                            ret = -1;
                        }
#else

                        Console.WriteLine("XOR test ({0}, {1}) -> {2}, should be {3}, difference={4}",
                            data.GetTrainInput((uint)i)[0],
                            data.GetTrainInput((uint)i)[1],
                            calc_out[0],
                            data.GetTrainOutput((uint)i)[0],
                            calc_out[0] - data.GetTrainOutput((uint)i)[0]);
#endif

                    }
                    Console.WriteLine("Cleaning up.");
                }
            }
            Console.ReadKey();
            return ret;
        }
        static float fann_abs(float value)
        {
            return (((value) > 0) ? (value) : -(value));
        }
    }

}
