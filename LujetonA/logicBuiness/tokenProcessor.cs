using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace LujetonA.logicBuiness
{
    public class tokenProcessor
    {

        static string clave = "cadenadecifrado";
        //tiene que actualizarse
        public static string GetToken(int id_usuario, DateTime expirationTime) {
            string token = id_usuario + ";" + expirationTime.Ticks.ToString();
           
            return cifrar(token);
        }

        public static Models.usuario ValidateToken(string token, long seconds) {
            token = descifrar(token);            
            var id = int.Parse(token.Split(';')[0]);
            var time = new DateTime(long.Parse(token.Split(';')[1]));
            if ((time - DateTime.UtcNow).TotalSeconds > seconds) {
                return null;
            }
            else
            {
                var store_user = new Models.bd_proyectoEntities1().usuario.Where(x => x.idusuario == id).FirstOrDefault();
                return store_user;
            }
        }


        // Función para cifrar una cadena.
        static string cifrar(string cadena)
        {

            byte[] llave; //Arreglo donde guardaremos la llave para el cifrado 3DES.

            byte[] arreglo = UTF8Encoding.UTF8.GetBytes(cadena); //Arreglo donde guardaremos la cadena descifrada.

            // Ciframos utilizando el Algoritmo MD5.
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            llave = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(clave));
            md5.Clear();

            //Ciframos utilizando el Algoritmo 3DES.
            TripleDESCryptoServiceProvider tripledes = new TripleDESCryptoServiceProvider();
            tripledes.Key = llave;
            tripledes.Mode = CipherMode.ECB;
            tripledes.Padding = PaddingMode.PKCS7;
            ICryptoTransform convertir = tripledes.CreateEncryptor(); // Iniciamos la conversión de la cadena
            byte[] resultado = convertir.TransformFinalBlock(arreglo, 0, arreglo.Length); //Arreglo de bytes donde guardaremos la cadena cifrada.
            tripledes.Clear();

            return Convert.ToBase64String(resultado, 0, resultado.Length); // Convertimos la cadena y la regresamos.
        }        

// Función para descifrar una cadena.
        static string descifrar(string cadena)
        {
            try
            {
                byte[] llave;

                byte[] arreglo = Convert.FromBase64String(cadena); // Arreglo donde guardaremos la cadena descovertida.

                // Ciframos utilizando el Algoritmo MD5.
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                llave = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(clave));
                md5.Clear();

                //Ciframos utilizando el Algoritmo 3DES.
                TripleDESCryptoServiceProvider tripledes = new TripleDESCryptoServiceProvider();
                tripledes.Key = llave;
                tripledes.Mode = CipherMode.ECB;
                tripledes.Padding = PaddingMode.PKCS7;
                ICryptoTransform convertir = tripledes.CreateDecryptor();
                byte[] resultado = convertir.TransformFinalBlock(arreglo, 0, arreglo.Length);
                tripledes.Clear();

                string cadena_descifrada = UTF8Encoding.UTF8.GetString(resultado); // Obtenemos la cadena
                return cadena_descifrada; // Devolvemos la cadena
            }
            catch
            {
                return "";
            }
        }
    }
}