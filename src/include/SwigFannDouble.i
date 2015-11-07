/*SwigFannDouble.i*/
%module fanndouble
%include "doublefann.h"
%include "SwigFann.i"
%{
#include "parallel_fann.hpp"
%}
%include "std_vector.i"
%include "parallel_fann.hpp"
enum fann_errno_enum
{
	FANN_E_NO_ERROR = 0,
	FANN_E_CANT_OPEN_CONFIG_R,
	FANN_E_CANT_OPEN_CONFIG_W,
	FANN_E_WRONG_CONFIG_VERSION,
	FANN_E_CANT_READ_CONFIG,
	FANN_E_CANT_READ_NEURON,
	FANN_E_CANT_READ_CONNECTIONS,
	FANN_E_WRONG_NUM_CONNECTIONS,
	FANN_E_CANT_OPEN_TD_W,
	FANN_E_CANT_OPEN_TD_R,
	FANN_E_CANT_READ_TD,
	FANN_E_CANT_ALLOCATE_MEM,
	FANN_E_CANT_TRAIN_ACTIVATION,
	FANN_E_CANT_USE_ACTIVATION,
	FANN_E_TRAIN_DATA_MISMATCH,
	FANN_E_CANT_USE_TRAIN_ALG,
	FANN_E_TRAIN_DATA_SUBSET,
	FANN_E_INDEX_OUT_OF_BOUND,
	FANN_E_SCALE_NOT_PRESENT,
	FANN_E_INPUT_NO_MATCH,
	FANN_E_OUTPUT_NO_MATCH,
	FANN_E_WRONG_PARAMETERS_FOR_CREATE
};
struct fann_train_data
{
	enum fann_errno_enum errno_f;
	FILE *error_log;
	char *errstr;

	unsigned int num_data;
	unsigned int num_input;
	unsigned int num_output;
	fann_type **input;
	fann_type **output;
};
namespace std {
    %template(DoubleVectorVector) vector<vector<double>>;
	%template(DoubleVector) vector<double>;
};
%inline %{
	typedef double* double_ptr;
    typedef double fann_type;
%}
%array_class(double_ptr, doubleArrayArray);
%array_class(double, doubleArray);


