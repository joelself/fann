/*SwigFann.i*/
%module SwigFann
%include typemaps.i
%include "carrays.i"
%include "cpointer.i"
%include "std_string.i"
%include "fann.h"
%include "fann_error.h"
%include "fann_data.h"
%include "fann_training_data_cpp.h"
%pointer_class(fann_type, fann_type_p);
%pointer_class(float, float_p);
%include "fann_cpp.h"
%include "fann_data_cpp.h"
FILE *fopen(const char *filename, const char *mode);


%pointer_class(FANN::training_data, training_data_p);
%pointer_functions(FANN::training_data, training_data_pointer);
%array_functions(float*, float_p_array)
%array_functions(float, float_array)
%array_functions(unsigned int, uint_array)
%array_functions(connection, connection_array);
%array_class(float, floatArray);
%array_class(unsigned int, uintArray);

%inline %{
    typedef float fann_type;
	typedef FANN::training_data training_data;
	typedef FANN::training_algorithm_enum training_algorithm_enum;
	typedef FANN::activation_function_enum activation_function_enum;
	typedef FANN::error_function_enum error_function_enum;
	typedef FANN::network_type_enum network_type_enum;
	typedef FANN::stop_function_enum stop_function_enum;
%}

%{
#include "fann.h"
#include "fann_cpp.h"
#include "fann_error.h"
#include "fann_data.h"
#include "fann_data_cpp.h"
#include "fann_training_data_cpp.h"
#include "stdio.h"
%}

