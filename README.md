# Fann C#
## FANN

**Fast Artificial Neural Network (FANN) Library** is a free open source neural network library, which implements multilayer artificial neural networks in C with support for both fully connected and sparsely connected networks.

Cross-platform execution in both fixed and floating point are supported. It includes a framework for easy handling of training data sets. It is easy to use, versatile, well documented, and fast. 

## Fann `C#`
**`Fann C#`** is a wapper around FANN that lets you use the FANN libraries from C# on Windows. This is currently a work in progress as only a subset of the FANN functionality has been tested. As time goes on I plan to add more of the FANN functionality as well as make it much easier to use.

## Current Progress
The entire FANN neural_net and training_data C++ wrapper functionality is available along with the FANN parallel functions (for fannfloat and fanndouble).

## To Install

#### From Source

First you'll want to clone the repository:

`git clone https://github.com/joelself/FannCSharp.git`

Once that's finished, navigate to the VS2010 directory. In this case it would be .\VS2010:

`cd VS2010`

Open the solution fann.sln

Build the solution. If you chose 'x64' as your platform the dlls will be built in .\fann\bin\x64\. If you chose x86 as your platform the dlls will be in .\fann\bin\x86\.

You will need the FANNCSharp.dll and at least one of fannfloat.dll, fanndouble.dll, or fannfixed.dll depending on which version of FANN you want to run. In your project add FANNCSharp.dll as a reference and make sure the fann[float|double|fixed] dll is in the same directory as FANNCSharp. You can now code against the C# version of the FANN C++ interface. The main classes you'll be using are NeuralNetwork[Float|Double|Fixed] and TrainingData[Float|Double|Fixed].

To easily switch between different versions of FANN, do what the examples do and put using statements at the top of your file and specify a compilation symbol in your project properties to select a specific NeuralNetwork and TrainingData type.

#### Note

I'm currently making changes to both the NeuralNetworrk* and TrainingData* classes to be more C-Sharpey, so code written against them might break in the future. The fixes should be easy, e.g. a getter method changes to a Property, so don't worry too much.

## To Learn More About FANN

To get started with FANN, go to the [FANN help site](http://leenissen.dk/fann/wp/help/), which will include links to all the available resources. FannCSharp closely mirrors the C++ interfaces: [neural_net](http://libfann.github.io/fann/docs/files/fann_cpp-h.html) and [training_data](http://libfann.github.io/fann/docs/files/fann_training_data_cpp-h.html).

For more information about FANN, please refer to the [FANN website](http://leenissen.dk/fann/wp/)
