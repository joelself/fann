/*SwigFann.i*/
%module SwigFann
%{
#include "fann.h"
#include "fann_cpp.h"
#include "fann_error.h"
#include "fann_data.h"
#include "fann_data_cpp.h"
#include "fann_training_data_cpp.h"
#include "stdio.h"
%}
%include typemaps.i
%include "carrays.i"
%include "cpointer.i"
%include "std_string.i"
%include "fann.h"
%include "fann_error.h"
%include "fann_data.h"
%include "fann_cpp.h"
%include "fann_data_cpp.h"
%include "fann_training_data_cpp.h"

FILE *fopen(const char *filename, const char *mode);

%pointer_class(fann_type, fann_type_p);
%pointer_class(float, float_p);
%pointer_class(training_algorithm_enum, training_algorithm_enum_p);
%pointer_class(activation_function_enum, activation_function_enum_p);
%pointer_class(error_function_enum, error_function_enum_p);
%pointer_class(network_type_enum, network_type_enum_p);
%pointer_class(stop_function_enum, stop_function_enum_p);

%array_functions(float*, float_p_array)
%array_functions(float, float_array)
%array_functions(unsigned int, uint_array)
%array_functions(connection, connection_array);

%inline %{
    typedef float fann_type;
%}
