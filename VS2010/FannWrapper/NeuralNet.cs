using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FannWrap;
namespace FannWrapper
{
    public class NeuralNet : IDisposable
    {
        neural_net net = null;
        public NeuralNet()
        {
            net = new neural_net();
        }

        public NeuralNet(NeuralNet other)
        {
            net = new neural_net(other.InternalNet);
        }

        public NeuralNet(fann other)
        {
            net = new neural_net(other);
        }

        public void CopyFromFann(fann other)
        {
            net = new neural_net(other);
        }

        public void Dispose()
        {
            net.destroy();
        }

        public bool Create(uint numLayers)
        {
            return net.create_standard(numLayers);
        }

        public bool Create(uint[] layers)
        {
            SWIGTYPE_p_unsigned_int newLayers = SwigFann.new_uint_array(layers.Length);
            for(int i = 0; i < layers.Length; i++) {
                SwigFann.uint_array_setitem(newLayers, i, layers[i]);
            }
            return net.create_standard_array((uint)layers.Length, newLayers);
        }

        public bool Create(float connectionRate, uint numLayers)
        {
            return net.create_sparse(connectionRate, numLayers);
        }

        public bool Create(float connectionRate, uint[] layers)
        {
            SWIGTYPE_p_unsigned_int newLayers = SwigFann.new_uint_array(layers.Length);
            for (int i = 0; i < layers.Length; i++)
            {
                SwigFann.uint_array_setitem(newLayers, i, layers[i]);
            }
            return net.create_sparse_array(connectionRate, (uint)layers.Length, newLayers);
        }

        public bool CreateShortcut(uint numLayers)
        {
            return net.create_shortcut(numLayers);
        }

        public bool CreateShortcut(uint[] layers)
        {
            SWIGTYPE_p_unsigned_int newLayers = SwigFann.new_uint_array(layers.Length);
            for (int i = 0; i < layers.Length; i++)
            {
                SwigFann.uint_array_setitem(newLayers, i, layers[i]);
            }
            return net.create_shortcut_array((uint)layers.Length, newLayers);
        }
#region Properties
        public neural_net InternalNet
        {
            get
            {
                return net;
            }
        }
#endregion Properties
    }
}
