/*************************************************************************************************************/
// Based off the Mushrooms project, but adds in a speed test comparing the speed of repeated
// accesses to the Input and Output properties and repeated access to the Accessor properties

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
    class MushroomSpeedTest
    {
        static void Main()
        {
            const uint num_layers = 3;
	        const uint num_neurons_hidden = 32;
	        const float desired_error = 0.0001F;
	        const uint max_epochs = 300;
	        const uint epochs_between_reports = 10;
            long before;


            Console.WriteLine("Creating network.");
            using (TrainingData data = new TrainingData("..\\..\\..\\datasets\\mushroom.train"))
            using (NeuralNet net = new NeuralNet(NetworkType.LAYER, num_layers, data.InputCount, num_neurons_hidden, data.OutputCount))
            {
                Console.WriteLine("Training network.");

                net.ActivationFunctionHidden = ActivationFunction.SIGMOID_SYMMETRIC;
                net.ActivationFunctionOutput = ActivationFunction.SIGMOID;

                net.TrainOnData(data, max_epochs, epochs_between_reports, desired_error);

                Console.WriteLine("Testing network.");

                using (TrainingData testData = new TrainingData())
                {
                    testData.ReadTrainFromFile("..\\..\\..\\datasets\\mushroom.test");
                    before = Environment.TickCount;
                    for (int i = 0; i < 5; i++)
                    {
                        DataType[] input = new DataType[testData.InputCount];
                        DataType[] output = new DataType[testData.OutputCount];
                        for(int j = 0; j < testData.InputCount; j++)
                        {
                            input[j] = testData.InputAccessor[i][j];
                        }
                        for (int j = 0; j < testData.OutputCount; j++)
                        {
                            output[j] = testData.OutputAccessor[i][j];
                        }
                        net.Test(input, output);
                    }
                    Console.WriteLine("Data Accessor ticks {0}", Environment.TickCount - before);

                    before = Environment.TickCount;
                    for (int i = 0; i < 5; i++)
                    {
                        DataType[] input = new DataType[testData.InputCount];
                        DataType[] output = new DataType[testData.OutputCount];
                        for (int j = 0; j < testData.InputCount; j++)
                        {
                            input[j] = testData.Input[i][j];
                        }
                        for (int j = 0; j < testData.OutputCount; j++)
                        {
                            output[j] = testData.Output[i][j];
                        }
                        net.Test(input, output);
                    }
                    Console.WriteLine("Array ticks {0}", Environment.TickCount - before);
                }
                Console.WriteLine("MSE error on test data {0}", net.MSE);

                Console.WriteLine("Saving network.");

                net.Save("..\\..\\..\\examples\\mushroom_float.net");

                Console.ReadKey();
            }
        }
    }
}
