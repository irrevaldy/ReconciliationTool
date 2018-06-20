using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReconciliationTool
{
    class Utility
    {
        public Utility()
        {

        }

        public byte[] btHexToByte(string msg)
        {
            //remove any spaces from the string
            msg = msg.Replace(" ", "");
            //create a byte array the length of the
            //string divided by 2
            byte[] comBuffer = new byte[msg.Length / 2];
            //loop through the length of the provided string
            for (int i = 0; i < msg.Length; i += 2)
                //convert each set of 2 characters to a byte
                //and add to the array
                comBuffer[i / 2] = (byte)Convert.ToByte(msg.Substring(i, 2), 16);
            //return the array
            return comBuffer;
        }

        public String szByteToHex(byte[] comByte)
        {
            int byteLength = 0;
            //create a new StringBuilder object
            //byteLength = comByte[0] / 256 + comByte[1] % 256 + 2;
            byteLength = comByte.Length;

            //StringBuilder builder = new StringBuilder(comByte.Length * 3);
            StringBuilder builder = new StringBuilder(byteLength * 3);
            //loop through each byte in the array
            //foreach (byte data in comByte)
            for (int i = 0; i < byteLength;  i++)
                //convert the byte to a string and add to the stringbuilder
                builder.Append(Convert.ToString(comByte[i], 16).PadLeft(2, '0'));
            //return the converted value
            return builder.ToString().ToUpper();
        }

        public String szByteToHexWithLength(byte[] comByte, int length)
        {
            int byteLength = 0;
            //create a new StringBuilder object
            //byteLength = comByte[0] / 256 + comByte[1] % 256 + 2;
            byteLength = length;

            //StringBuilder builder = new StringBuilder(comByte.Length * 3);
            StringBuilder builder = new StringBuilder(byteLength * 3);
            //loop through each byte in the array
            //foreach (byte data in comByte)
            for (int i = 0; i < byteLength; i++)
                //convert the byte to a string and add to the stringbuilder
                builder.Append(Convert.ToString(comByte[i], 16).PadLeft(2, '0'));
            //return the converted value
            return builder.ToString().ToUpper();
        }

        public String szBytesHeaderToHex(byte[] comByte)
        {
            int byteLength = 0;
            byteLength = 4;
            StringBuilder builder = new StringBuilder(byteLength * 3);

            for (int i = 0; i < byteLength; i++) builder.Append(Convert.ToString(comByte[i], 16).PadLeft(2, '0'));

            return builder.ToString().ToUpper();
        }

        public String szHexAsciiConvert(String hex)
        {

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i <= hex.Length - 2; i += 2)
            {
                if (hex.Substring(i, 2) != "00")
                {
                    sb.Append(Convert.ToString(Convert.ToChar(Int32.Parse(hex.Substring(i, 2),

                    System.Globalization.NumberStyles.HexNumber))));
                }
            }

            return sb.ToString();
        }

        public String szHexToBinary(String szHex)
        {
            StringBuilder szBinary = new StringBuilder();
            String szBuff = String.Empty;
            String szBuff2 = String.Empty;
            int i = 0;

            for (i = 0; i < szHex.Length;)
            {
                szBuff = szHex.Substring(i, 1);
                szBuff2 = Convert.ToString(Convert.ToInt32(szBuff, 16), 2);
                szBinary.Append(szBuff2.PadLeft(4, '0'));
                i += 1;
            }

            return szBinary.ToString();
        }

        public String szDecimalToHexadecimal(int inDecimal)
        {
            return (inDecimal.ToString("X4"));
        }

        public byte[] StringToByteArray(string hex)
        {
            if (hex.Length % 2 == 1)
                throw new Exception("The binary key cannot have an odd number of digits");

            byte[] arr = new byte[hex.Length >> 1];

            for (int i = 0; i < hex.Length >> 1; ++i)
            {
                arr[i] = (byte)((GetHexVal(hex[i << 1]) << 4) + (GetHexVal(hex[(i << 1) + 1])));
            }

            return arr;
        }

        public int GetHexVal(char hex)
        {
            int val = (int)hex;

            return val - (val < 58 ? 48 : (val < 97 ? 55 : 87));
        }

        public String DisplayAsBytes(byte[] bytes)
        {
            String retVal = "";
            for (int i = 0; i < bytes.Length; ++i)
            {
                retVal += bytes[i].ToString("x2");// + " " ;
                if (i > 0 && i % 16 == 0) retVal += "\n";
            }
            return retVal;
        }  // DisplayAsBytes()

        ~Utility()
        {

        }
    }
}
