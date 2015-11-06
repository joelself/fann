/*SwigFannFloat.i*/
%module SwigFannFloat
%{
#include "fann.h"
#include "fann_cpp.h"
#include "fann_error.h"
#include "fann_data.h"
#include "fann_train.h"
#include "fann_data_cpp.h"
#include "fann_training_data_cpp.h"
#include "parallel_fann.hpp"
#include "stdio.h"
%}
%rename("to_fann") operator struct fann*();
%rename("to_fann_train_data") operator struct fann_train_data*();
#define FANN_EXTERNAL /**/
#define FANN_API /**/
%include typemaps.i
%include "carrays.i"
%include "arrays_csharp.i"
%include "cpointer.i"
%include "std_string.i"
%include "std_vector.i"
%include "fann_data.h"
%include "fann_train.h"
%array_class(FANN::connection, connectionArray);
%include "fann_training_data_cpp.h"
%include "fann_cpp.h"
%include "fann_data_cpp.h"
%include "parallel_fann.hpp"
FILE *fopen(const char *filename, const char *mode);
namespace std {
    %template(FloatVectorVector) vector<vector<float>>;
	%template(FloatVector) vector<float>;
}
struct fann_connection
{
    /* Unique number used to identify source neuron */
    unsigned int from_neuron;
    /* Unique number used to identify destination neuron */
    unsigned int to_neuron;
    /* The numerical value of the weight */
    fann_type weight;
};
%inline %{;
	typedef float* float_ptr;
    typedef float fann_type;
	typedef fann_connection connection;
	typedef FANN::training_algorithm_enum training_algorithm_enum;
	typedef FANN::activation_function_enum activation_function_enum;
	typedef FANN::error_function_enum error_function_enum;
	typedef FANN::network_type_enum network_type_enum;
	typedef FANN::stop_function_enum stop_function_enum;
%}
%array_class(float_ptr, floatArrayArray);
%array_class(float, floatArray);
%array_class(unsigned int, uintArray);
%array_class(activation_function_enum, activationFunctionEnumArray);


