using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace ReconciliationTool
{
    class Crypto
    {
        public Crypto()
        {

        }

        public byte[] btEncryptConfig(String szPlainASCIIString)
        {
            Utility utility = new Utility();

            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.Mode = CipherMode.CBC;
            aes.BlockSize = 128;
            aes.KeySize = 256;
            aes.Key = composeKey();
            aes.IV = composeIV();
            aes.Padding = PaddingMode.PKCS7;
            ICryptoTransform enc = aes.CreateEncryptor();

            byte[] btPlainText = Encoding.ASCII.GetBytes(szPlainASCIIString);
            byte[] btCipherText = enc.TransformFinalBlock(btPlainText, 0, btPlainText.Length);
            return btCipherText;
        }

        public byte[] btDecryptConfig(String szCipherHexString)
        {
            Utility utility = new Utility();

            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.Mode = CipherMode.CBC;
            aes.BlockSize = 128;
            aes.KeySize = 256;
            aes.Key = composeKey();
            aes.IV = composeIV();
            aes.Padding = PaddingMode.PKCS7;
            ICryptoTransform dec = aes.CreateDecryptor();

            byte[] btCipherText = utility.btHexToByte(szCipherHexString);
            byte[] btDecipheredText = dec.TransformFinalBlock(btCipherText, 0, btCipherText.Length);
            return btDecipheredText;
        }

        public byte[] btEncryptServerDBConfig(String szPlainASCIIString)
        {
            Utility utility = new Utility();

            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.Mode = CipherMode.CBC;
            aes.BlockSize = 128;
            aes.KeySize = 256;
            aes.Key = composeKey();
            aes.IV = composeIV();
            aes.Padding = PaddingMode.PKCS7;
            ICryptoTransform enc = aes.CreateEncryptor();

            byte[] btPlainText = Encoding.ASCII.GetBytes(szPlainASCIIString);
            byte[] btCipherText = enc.TransformFinalBlock(btPlainText, 0, btPlainText.Length);
            return btCipherText;
        }

        public byte[] btDecryptServerDBConfig(String szCipherHexString)
        {
            Utility utility = new Utility();

            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.Mode = CipherMode.CBC;
            aes.BlockSize = 128;
            aes.KeySize = 256;
            aes.Key = composeKey();
            aes.IV = composeIV();
            aes.Padding = PaddingMode.PKCS7;
            ICryptoTransform dec = aes.CreateDecryptor();

            byte[] btCipherText = utility.btHexToByte(szCipherHexString);
            byte[] btDecipheredText = dec.TransformFinalBlock(btCipherText, 0, btCipherText.Length);
            return btDecipheredText;
        }

        public byte[] btEncryptMessage(byte[] btPlainMessage, byte[] btKey)
        {
            Utility utility = new Utility();

            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.Mode = CipherMode.CBC;
            aes.BlockSize = 128;
            aes.KeySize = 256;
            aes.Key = btKey;
            aes.IV = utility.btHexToByte("00000000000000000000000000000000");
            aes.Padding = PaddingMode.PKCS7;
            ICryptoTransform enc = aes.CreateEncryptor();

            byte[] btCipherText = enc.TransformFinalBlock(btPlainMessage, 0, btPlainMessage.Length);
            return btCipherText;
        }

        public byte[] btDecryptMessage(byte[] btCipherMessage, byte[] btKey)
        {
            Utility utility = new Utility();

            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.Mode = CipherMode.CBC;
            aes.BlockSize = 128;
            aes.KeySize = 256;
            aes.Key = btKey;
            aes.IV = utility.btHexToByte("00000000000000000000000000000000");
            aes.Padding = PaddingMode.PKCS7;
            ICryptoTransform dec = aes.CreateDecryptor();

            byte[] btDecipheredText = dec.TransformFinalBlock(btCipherMessage, 0, btCipherMessage.Length);
            return btDecipheredText;
        }

        public byte[] btEncryptPINBlock(byte[] btPlainPINBlock, byte[] btKey)
        {
            Utility utility = new Utility();

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            des.Mode = CipherMode.ECB;
            des.BlockSize = 64;
            des.KeySize = 64;
            des.Key = btKey;
            des.Padding = PaddingMode.None;
            ICryptoTransform enc = des.CreateEncryptor();

            byte[] btCipherText = enc.TransformFinalBlock(btPlainPINBlock, 0, btPlainPINBlock.Length);
            return btCipherText;
        }

        public byte[] btComposeFEKey(byte[] btMK, byte[] btWK)
        {
            Utility utility = new Utility();

            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            aes.Mode = CipherMode.CBC;
            aes.BlockSize = 128;
            aes.KeySize = 256;
            aes.Key = btMK;
            aes.Padding = PaddingMode.None;
            aes.IV = utility.btHexToByte("00000000000000000000000000000000");
            ICryptoTransform dec = aes.CreateDecryptor();

            byte[] btFEKey = dec.TransformFinalBlock(btWK, 0, btWK.Length);
            return btFEKey;
        }

        public byte[] btComposePINKey(byte[] btMK, byte[] btWK)
        {
            Utility utility = new Utility();

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            des.Mode = CipherMode.ECB;
            des.BlockSize = 64;
            des.KeySize = 64;
            des.Key = btMK;
            des.Padding = PaddingMode.None;
            ICryptoTransform dec = des.CreateDecryptor();

            byte[] btPINKey = dec.TransformFinalBlock(btWK, 0, btWK.Length);
            return btPINKey;
        }

        private static byte[] composeKey()
        {
            Utility utility = new Utility();
            int iKey1 = 7483;
            int iKey2 = 2121212121;
            int iKey3 = 6789;
            int iKey4 = 1526737475;
            int iKey5 = 5555;
            int iKey6 = 1987654321;
            int iKey7 = 8888;
            int iKey8 = 1282745612;
            String hexValue1 = iKey1.ToString("X"); //MessageBox.Show(hexValue1);
            String hexValue2 = iKey2.ToString("X"); //MessageBox.Show(hexValue2);
            String hexValue3 = iKey3.ToString("X"); //MessageBox.Show(hexValue3);
            String hexValue4 = iKey4.ToString("X"); //MessageBox.Show(hexValue4);
            String hexValue5 = iKey5.ToString("X"); //MessageBox.Show(hexValue5);
            String hexValue6 = iKey6.ToString("X"); //MessageBox.Show(hexValue6);
            String hexValue7 = iKey7.ToString("X"); //MessageBox.Show(hexValue7);
            String hexValue8 = iKey8.ToString("X"); //MessageBox.Show(hexValue8);

            byte[] byteValue1 = new byte[hexValue1.Length / 2];
            byte[] byteValue2 = new byte[hexValue2.Length / 2];
            byte[] byteValue3 = new byte[hexValue3.Length / 2];
            byte[] byteValue4 = new byte[hexValue4.Length / 2];
            byte[] byteValue5 = new byte[hexValue5.Length / 2];
            byte[] byteValue6 = new byte[hexValue6.Length / 2];
            byte[] byteValue7 = new byte[hexValue7.Length / 2];
            byte[] byteValue8 = new byte[hexValue8.Length / 2];

            byteValue1 = utility.btHexToByte(hexValue1);
            byteValue2 = utility.btHexToByte(hexValue2);
            byteValue3 = utility.btHexToByte(hexValue3);
            byteValue4 = utility.btHexToByte(hexValue4);
            byteValue5 = utility.btHexToByte(hexValue5);
            byteValue6 = utility.btHexToByte(hexValue6);
            byteValue7 = utility.btHexToByte(hexValue7);
            byteValue8 = utility.btHexToByte(hexValue8);

            Array.Reverse(byteValue1);
            Array.Reverse(byteValue2);
            Array.Reverse(byteValue3);
            Array.Reverse(byteValue4);
            Array.Reverse(byteValue5);
            Array.Reverse(byteValue6);
            Array.Reverse(byteValue7);
            Array.Reverse(byteValue8);

            byte[] sharedKey = new byte[32];

            Array.Copy(byteValue1, 0, sharedKey, 0, byteValue1.Length);
            Array.Copy(byteValue2, 0, sharedKey, 4, byteValue2.Length);
            Array.Copy(byteValue3, 0, sharedKey, 8, byteValue3.Length);
            Array.Copy(byteValue4, 0, sharedKey, 12, byteValue4.Length);
            Array.Copy(byteValue5, 0, sharedKey, 16, byteValue5.Length);
            Array.Copy(byteValue6, 0, sharedKey, 20, byteValue6.Length);
            Array.Copy(byteValue7, 0, sharedKey, 24, byteValue7.Length);
            Array.Copy(byteValue8, 0, sharedKey, 28, byteValue8.Length);

            return sharedKey;
        }

        private static byte[] composeIV()
        {
            Utility utility = new Utility();
            int iKey1 = 6452;
            int iKey2 = 1245125678;
            int iKey3 = 4726;
            int iKey4 = 1451457898;
            int iKey5 = 8823;
            int iKey6 = 1987654321;
            int iKey7 = 9989;
            int iKey8 = 1116666222;
            String hexValue1 = iKey1.ToString("X"); //MessageBox.Show(hexValue1);
            String hexValue2 = iKey2.ToString("X"); //MessageBox.Show(hexValue2);
            String hexValue3 = iKey3.ToString("X"); //MessageBox.Show(hexValue3);
            String hexValue4 = iKey4.ToString("X"); //MessageBox.Show(hexValue4);
            String hexValue5 = iKey5.ToString("X"); //MessageBox.Show(hexValue5);
            String hexValue6 = iKey6.ToString("X"); //MessageBox.Show(hexValue6);
            String hexValue7 = iKey7.ToString("X"); //MessageBox.Show(hexValue7);
            String hexValue8 = iKey8.ToString("X"); //MessageBox.Show(hexValue8);

            byte[] byteValue1 = new byte[hexValue1.Length / 2];
            byte[] byteValue2 = new byte[hexValue2.Length / 2];
            byte[] byteValue3 = new byte[hexValue3.Length / 2];
            byte[] byteValue4 = new byte[hexValue4.Length / 2];
            byte[] byteValue5 = new byte[hexValue5.Length / 2];
            byte[] byteValue6 = new byte[hexValue6.Length / 2];
            byte[] byteValue7 = new byte[hexValue7.Length / 2];
            byte[] byteValue8 = new byte[hexValue8.Length / 2];

            byteValue1 = utility.btHexToByte(hexValue1);
            byteValue2 = utility.btHexToByte(hexValue2);
            byteValue3 = utility.btHexToByte(hexValue3);
            byteValue4 = utility.btHexToByte(hexValue4);
            byteValue5 = utility.btHexToByte(hexValue5);
            byteValue6 = utility.btHexToByte(hexValue6);
            byteValue7 = utility.btHexToByte(hexValue7);
            byteValue8 = utility.btHexToByte(hexValue8);

            Array.Reverse(byteValue1);
            Array.Reverse(byteValue2);
            Array.Reverse(byteValue3);
            Array.Reverse(byteValue4);
            Array.Reverse(byteValue5);
            Array.Reverse(byteValue6);
            Array.Reverse(byteValue7);
            Array.Reverse(byteValue8);

            byte[] sharedKey = new byte[16];

            Array.Copy(byteValue1, 0, sharedKey, 0, byteValue1.Length);
            Array.Copy(byteValue2, 0, sharedKey, 4, byteValue2.Length);
            Array.Copy(byteValue3, 0, sharedKey, 8, byteValue3.Length);
            Array.Copy(byteValue4, 0, sharedKey, 12, byteValue4.Length);
            //Array.Copy(byteValue5, 0, sharedKey, 16, byteValue5.Length);
            //Array.Copy(byteValue6, 0, sharedKey, 20, byteValue6.Length);
            //Array.Copy(byteValue7, 0, sharedKey, 24, byteValue7.Length);
            //Array.Copy(byteValue8, 0, sharedKey, 28, byteValue8.Length);

            return sharedKey;
        }

        public int inEncryptTMK(String szPlainTMK, out String szEncryptedTMK)
        {
            szEncryptedTMK = "";
            Utility utility = new Utility();
            int inRetval = 0;

            byte[] bPlainText;
            byte[] bCipherText;
            byte[] bDecipheredText;
            String szKeyString;
            byte[] bKeyBytes;
            AesLib.Aes aes;

            try
            {
                if (szPlainTMK.Length < 32)
                    szPlainTMK = szPlainTMK.PadRight(32, '0');

                bPlainText = utility.StringToByteArray(szPlainTMK);
                bCipherText = new byte[16];
                bDecipheredText = new byte[16];

                szKeyString = "E446EF0DADAFC3A0B86679518D1872F7";
                bKeyBytes = utility.StringToByteArray(szKeyString);

                aes = new AesLib.Aes(AesLib.Aes.KeySize.Bits128, bKeyBytes);
                aes.Cipher(bPlainText, bCipherText);

                szEncryptedTMK = utility.DisplayAsBytes(bCipherText);
            }
            catch
            {
                inRetval = -1;
            }

            return inRetval;
        }

        public int inDecryptTMK(String szEncryptedTMK, out String szPlainTMK)
        {
            Utility utility = new Utility();
            int inRetval = 0;

            byte[] bCipherText;
            byte[] bDecipheredText;
            String szKeyString;
            byte[] bKeyBytes;
            AesLib.Aes aes;
            szPlainTMK = String.Empty;
            try
            {
                bCipherText = new byte[16];
                bDecipheredText = new byte[16];

                bCipherText = utility.StringToByteArray(szEncryptedTMK);

                szKeyString = "E446EF0DADAFC3A0B86679518D1872F7";
                bKeyBytes = utility.StringToByteArray(szKeyString);

                aes = new AesLib.Aes(AesLib.Aes.KeySize.Bits128, bKeyBytes);
                aes.InvCipher(bCipherText, bDecipheredText);

                szPlainTMK = utility.DisplayAsBytes(bDecipheredText);
            }
            catch
            {
                inRetval = -1;
            }

            return inRetval;
        }

        ~Crypto()
        {

        }
    }

    public static class ConfigEncryption
    {
        public static int inDecryptXML(String szFileInput, String szFileOutput)
        {
            using (System.Security.Cryptography.Aes decryptor = System.Security.Cryptography.Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes("WDTerminalUploader", new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                decryptor.Key = pdb.GetBytes(32);
                decryptor.IV = pdb.GetBytes(16);

                using (FileStream fsInput = new FileStream(szFileInput, FileMode.Open))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(fsInput, decryptor.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        using (FileStream fsOutput = new FileStream(szFileOutput, FileMode.Create))
                        {
                            //TODO: Need to look into this more. Assuming encrypted text is longer than plain but there is probably a better way
                            int inData;
                            while ((inData = cryptoStream.ReadByte()) != -1)
                            {
                                fsOutput.WriteByte((byte)inData);
                            }
                        }
                    }
                }
            }
            return 0;
        }

        public static int inEncryptXML(String szFileInput, String szFileOutput)
        {
            try
            {
                using (System.Security.Cryptography.Aes encryptor = System.Security.Cryptography.Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes("WDTerminalUploader", new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (FileStream fsOutput = new FileStream(szFileOutput, FileMode.Create))
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(fsOutput, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            using (FileStream fsInput = new FileStream(szFileInput, FileMode.Open))
                            {
                                int inData;
                                while ((inData = fsInput.ReadByte()) != -1)
                                {
                                    cryptoStream.WriteByte((byte)inData);
                                }
                            }
                        }
                    }
                }
            }
            catch { }

            return 0;
        }
    }
}
