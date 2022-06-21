using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace PLC
{
    public class PLCDriver
    {
        public AxActUtlTypeLib.AxActUtlType plc = new AxActUtlTypeLib.AxActUtlType();
        public bool connected { get; set; }

        public PLCDriver()
        {
            plc.CreateControl();
        }


        #region Connection
        public bool Connect(int station)
        {
                plc.ActLogicalStationNumber = station;
                int reuslt = plc.Open();

                if (reuslt == 0)
                {
                    connected = true;
                    
                    return true;
                }
                else
                {
                    return false;
                }
            }
        public bool Disconnect()
        {
            try
            {
                plc.Close();
                connected = false;
                return true;
            }
            catch (Exception ex)
            {
                return  false;
                throw ex;
            }
        }
        #endregion

        #region Monitor PLC
        public int write(string address, int value)
        {
            return plc.SetDevice(address.ToUpper(), value);
        }

        public async Task<int> read(string address)
        {
            int output = 0;
            int result;
            result = await Task.Run(() =>

                plc.GetDevice(address, out output)
            );
            return output;
        }

        #endregion

    }
}
