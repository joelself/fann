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
    class ScalingTest
    {
        static void Main()
        {
            DataType[] calc_out;
            Console.WriteLine("Creating network.");

            using(NeuralNet net = new NeuralNet("..\\..\\..\\fannpure\\bin\\Win32\\scaling.net"))
            {
                net.PrintConnections();
                net.PrintParameters();
                Console.WriteLine("Testing network.");
                using (TrainingData data = new TrainingData("..\\..\\datasets\\scaling.data"))
                {
                    //Console.WriteLine("-- {0} {1} {2}", data.TrainDataLength, data.InputCount, data.OutputCount);
                    //for (int i = 0; i < data.TrainDataLength; i++)
                    //{
                    //    for (int j = 0; j < data.InputCount; j++)
                    //    {
                    //        Console.Write("{0} ", data.InputAccessor[i][j]);
                    //    }
                    //    Console.WriteLine("");
                    //    for (int j = 0; j < data.OutputCount; j++)
                    //    {
                    //        Console.Write("{0} ", data.OutputAccessor[i][j]);
                    //    }
                    //    Console.WriteLine("");
                    //}
                    for (int i = 0; i < data.TrainDataLength; i++)
                    {
                        net.ResetMSE();
                        net.ScaleInput(data.GetTrainInput((uint)i));
                        calc_out = net.Run(data.GetTrainInput((uint)i));
                        net.DescaleOutput(calc_out);
                        Console.WriteLine("Result {0} original {1} error {2}", calc_out[0], data.OutputAccessor[i][0],
                                          FannAbs(calc_out[0] - data.OutputAccessor[i][0]));
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
