using System;
using FANNCSharp;
#if FANN_DOUBLE
using FANNCSharp.Double;
using DataType = System.Double;
#else
using FANNCSharp.Float;
using DataType = System.Single;
#endif
namespace Example
{
    class CascadeTrain
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
            ActivationFunction[] activation = new ActivationFunction[1];
            TrainingAlgorithm training_algorithm = TrainingAlgorithm.TRAIN_RPROP;

            Console.WriteLine("Reading data.");

            using (TrainingData trainData = new TrainingData("..\\..\\..\\datasets\\parity8.train"))
            using (TrainingData testData = new TrainingData("..\\..\\..\\datasets\\parity8.test"))
            {
                trainData.ScaleTrainData(-1, 1);
                testData.ScaleTrainData(-1, 1);

                Console.WriteLine("Creating network.");

                using (NeuralNet net = new NeuralNet(NetworkType.SHORTCUT, 2, trainData.InputCount, trainData.OutputCount))
                {
                    net.TrainingAlgorithm = training_algorithm;
                    net.ActivationFunctionHidden = ActivationFunction.SIGMOID_SYMMETRIC;
                    net.ActivationFunctionOutput = ActivationFunction.LINEAR;
                    net.TrainErrorFunction = ErrorFunction.ERRORFUNC_LINEAR;

                    if (multi == 0)
                    {
                        steepness[0] = 1;
                        net.CascadeActivationSteepnesses = steepness;

                        activation[0] = ActivationFunction.SIGMOID_SYMMETRIC;

                        net.CascadeActivationFunctions = activation;
                        net.CascadeCandidateGroupsCount = 8;
                    }

                    if (training_algorithm == TrainingAlgorithm.TRAIN_QUICKPROP)
                    {
                        net.LearningRate = 0.35F;
                        net.RandomizeWeights(-2.0F, 2.0F);
                    }

                    net.BitFailLimit = (DataType)0.9;
                    net.TrainStopFunction = StopFunction.STOPFUNC_BIT;
                    net.PrintParameters();

                    net.Save("..\\..\\..\\examples\\cascade_train2.net");

                    Console.WriteLine("Training network.");

                    net.CascadetrainOnData(trainData, max_neurons, neurons_between_reports, desired_error);

                    net.PrintConnections();

                    mse_train = net.TestData(trainData);
                    bit_fail_train = net.BitFail;
                    mse_test = net.TestData(testData);
                    bit_fail_test = net.BitFail;

                    Console.WriteLine("\nTrain error: {0}, Train bit-fail: {1}, Test error: {2}, Test bit-fail: {3}\n",
                                      mse_train, bit_fail_train, mse_test, bit_fail_test);

                    for (int i = 0; i < trainData.TrainDataLength; i++)
                    {
                        output = net.Run(trainData.GetTrainInput((uint)i));
                        if ((trainData.GetTrainOutput((uint)i)[0] >= 0 && output[0] <= 0) ||
                            (trainData.GetTrainOutput((uint)i)[0] <= 0 && output[0] >= 0))
                        {
                            Console.WriteLine("ERROR: {0} does not match {1}", trainData.GetTrainOutput((uint)i)[0], output[0]);
                        }
                    }

                    Console.WriteLine("Saving network.");
                    net.Save("..\\..\\..\\examples\\cascade_train.net");

                    Console.ReadKey();
                }
            }
        }
    }
}
