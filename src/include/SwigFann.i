%module SwigFann
%{
#include "fann.h"
#include "fann_cpp.h"
#include "fann_error.h"
#include "fann_data.h"
#include "fann_train.h"
#include "fann_data_cpp.h"
#include "fann_training_data_cpp.h"
#include "stdio.h"
%}
%rename("to_fann") operator struct fann*();
%rename("to_fann_train_data") operator struct fann_train_data*();
%csconstvalue(0) LINEAR;
%csconstvalue(0) ERRORFUNC_LINEAR;
%csconstvalue(0) LAYER;
%csconstvalue(0) STOPFUNC_MSE;
%csconstvalue(0) TRAIN_INCREMENTAL;
#define FANN_EXTERNAL /**/
#define FANN_API /**/
%include "typemaps.i"
%include "carrays.i"
%include "arrays_csharp.i"
%include "cpointer.i"
%include "std_string.i"
%include "fann_training_data_cpp.h"
%include "fann_data_cpp.h"
%include "fann_cpp.h"
FILE *fopen(const char *filename, const char *mode);
typedef struct fann_connection
{
    /* Unique number used to identify source neuron */
    unsigned int from_neuron;
    /* Unique number used to identify destination neuron */
    unsigned int to_neuron;
    /* The numerical value of the weight */
    fann_type weight;
}connection;
%{
typedef struct fann_connection connection;
typedef FANN::activation_function_enum activation_function_enum;
typedef FANN::error_function_enum error_function_enum;
typedef FANN::network_type_enum network_type_enum;
typedef FANN::stop_function_enum stop_function_enum;
typedef FANN::training_algorithm_enum training_algorithm_enum;
%}
%array_class(connection, connectionArray);
%array_class(unsigned int, uintArray);
%array_class(FANN::activation_function_enum, activationFunctionArray);