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
    class Robot
    {
        static void Main()
        {   
	        const float desired_error = 0.0F;
	        uint max_neurons = 30;
	        uint neurons_between_reports = 1;
	        uint bit_fail_train, bit_fail_test;
	        float mse_train, mse_test;
	        DataType[] output;
	        DataType[] steepness = new DataType[1];
	        int multi = 0;
            activation_function_enum[] activation = new activation_function_enum[1];
	        training_algorithm_enum training_algorithm = training_algorithm_enum.TRAIN_RPROP;

            Console.WriteLine("Reading data.");

            using(TrainingData trainData = new TrainingData())
            using (TrainingData testData = new TrainingData())
            {
                trainData.ReadTrainFromFile("..\\..\\datasets\\parity8.train");
                testData.ReadTrainFromFile("..\\..\\datasets\\parity8.test");

                trainData.ScaleTrainData(-1, 1);
                testData.ScaleTrainData(-1, 1);

                Console.WriteLine("Creating network.");

                using (NeuralNet net = new NeuralNet())
                {
                    net.CreateShortcut(2, trainData.NumInput(), trainData.NumOutput());

                    net.SetTrainingAlgorithm(training_algorithm);
                    net.SetActivationFunctionHidden(activation_function_enum.SIGMOID_SYMMETRIC);
                    net.SetActivationFunctionOutput(activation_function_enum.LINEAR);
                    net.SetTrainErrorFunction(error_function_enum.ERRORFUNC_LINEAR);

                    if (multi != 0)
                    {
                        steepness[0] = 1;
                        net.SetCascadeActivationSteepnesses(steepness);

                        activation[1] = activation_function_enum.SIGMOID_SYMMETRIC;

                        net.SetCascadeActivationFunctions(activation);
                        net.SetCascadeNumCandidateGroups(8);
                    }

                    if (training_algorithm == training_algorithm_enum.TRAIN_QUICKPROP)
                    {
                        net.SetLearningRate(0.35F);
                        net.RandomizeWeights(-2.0F, 2.0F);
                    }

                    net.SetBitFailLimit((DataType)0.9);
                    net.SetTrainStopFunction(stop_function_enum.STOPFUNC_BIT);
                    net.PrintParameters();

                    net.Save("..\\..\\examples\\cascade_train2.net");

                    Console.WriteLine("Training network.");

                    net.CascadetrainOnData(trainData, max_neurons, neurons_between_reports, desired_error);

                    net.PrintConnections();

                    mse_train = net.TestData(trainData);
                    bit_fail_train = net.GetBitFail();
                    mse_test = net.TestData(testData);
                    bit_fail_test = net.GetBitFail();

                    Console.WriteLine("\nTrain error: {0}, Train bit-fail: {1}, Test error: {2}, Test bit-fail: {3}\n",
                                      mse_train, bit_fail_train, mse_test, bit_fail_test);
                    DataType[][] input = trainData.GetInput();
                    DataType[][] outputs = trainData.GetOutput();
                    for (int i = 0; i < trainData.LengthTrainData(); i++)
                    {
                        output = net.Run(input[i]);
                        if((outputs[i][0] >= 0 && output[0] <= 0) ||
                            (outputs[i][0] <= 0 && output[0] >= 0))
                        {
                            Console.WriteLine("ERROR: {0} does not match {1}", outputs[i][0], output[0]);
                        }
                    }

                    Console.WriteLine("Saving network.");
                    net.Save("..\\..\\examples\\cascade_train.net");

                    Console.ReadKey();
                }
            }
        }
    }
}
