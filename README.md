# Fann C#
## FANN

**Fast Artificial Neural Network (FANN) Library** is a free open source neural network library, which implements multilayer artificial neural networks in C with support for both fully connected and sparsely connected networks.

Cross-platform execution in both fixed and floating point are supported. It includes a framework for easy handling of training data sets. It is easy to use, versatile, well documented, and fast. 

## Fann C\#
** Fann C\# ** is a wapper around FANN that lets you use the FANN libraries from C# on Windows. This is currently a work in progress as only a limited subset of the FANN functionality has been tested or is even available. As time goes on I plan to add more of the FANN functionality as well as make it much easier to use.

## To Install

#### From Source

First you'll want to clone the repository:

`git clone https://github.com/joelself/FannCSharp.git`

Once that's finished, navigate to the VS2010 directory. In this case it would be ./VS2010:

`cd .\VS2010`

Then run CMake

Open the solution fann.sln

Build the solution. If you chose 'x64' as your platform the dlls will be built in .\fann\bin\x64\. If you chose x86 as your platform the dlls will be in two places: .\VS2010\bin\x86\ and .\VS2010\bin\Win32\

## To Learn More About FANN

To get started with FANN, go to the [FANN help site](http://leenissen.dk/fann/wp/help/), which will include links to all the available resources. 

For more information about FANN, please refer to the [FANN website](http://leenissen.dk/fann/wp/)
