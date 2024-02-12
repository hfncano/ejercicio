// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;

int[] encryptMessage = { 46, 11, 15, 8, 11, 7, 7, 0, 12, 11, 16, 65, 32, 11, 13, 19, 17, 78, 45, 4, 4, 29, 12, 15, 72, 45, 2, 15, 7, 66, 67, 9, 9, 29, 67, 13, 7, 9, 17, 0, 12, 1, 67, 5, 13, 29, 0, 8, 14, 28, 2, 19, 72, 11, 15, 65, 5, 11, 13, 18, 9, 4, 6, 79, 72, 78, 34, 9, 7, 28, 2, 77, 72, 30, 2, 19, 9, 78, 18, 20, 13, 78, 42, 15, 27, 7, 21, 4, 72, 28, 6, 2, 7, 0, 12, 27, 11, 15, 67, 21, 29, 78, 15, 14, 15, 28, 12, 77, 72, 29, 22, 3, 13, 78, 6, 13, 72, 13, 12, 5, 13, 78, 0, 14, 6, 78, 6, 13, 72, 31, 22, 4, 72, 28, 6, 18, 7, 2, 21, 8, 27, 26, 6, 65, 13, 29, 23, 4, 72, 11, 9, 4, 26, 13, 10, 2, 1, 1, 67, 4, 6, 78, 36, 8, 28, 38, 22, 3, 71, 41, 10, 21, 36, 15, 1, 65, 17, 78, 0, 14, 5, 30, 2, 19, 28, 11, 67, 4, 4, 78, 6, 15, 4, 15, 0, 4, 72, 15, 67, 18, 7, 30, 12, 19, 28, 11, 35, 8, 6, 29, 10, 23, 13, 64, 0, 13, 70 };
//según el ejercicio la contraseña cumple con la expresión regular [a-z] lo que me indica que con las letras de la a a la z en minuscula
string pattern = "abcdefghijklmnopqrstuvwxyz";
//expresión regular para validar cada uno de los caracteres del mensaje
string regex = @"[a-zA-Z0-9\s.,@\-_\/]";
//variable para validar posición
int cont = 0;

#region posibles combinaciones de 4 dígitos
/*
 * en este segmento de código recorremos todas las posibles combinaciones de 4 dígitos que se pueden generar
 * con la expresión regular [a-z] que consiste en una permutación de 4*4*4*4, por que los caracteres se pueden
 * repetir en cualquier posición
 */
for (int i = 0; i < pattern.Length; i++)
{
    for (int j = 0; j < pattern.Length; j++)
    {
        for (int k = 0; k < pattern.Length; k++)
        {
            for (int l = 0; l < pattern.Length; l++)
            {
                /*
                 * la variable key representa un array de 4 posiciones númericas, transformando cada caracter a
                 * a su correspondiente valor númerico ASCII
                 */
                int[] key = { Convert.ToInt32(pattern[i]), Convert.ToInt32(pattern[j]), Convert.ToInt32(pattern[k]), Convert.ToInt32(pattern[l]) };
                string text = $"{pattern[i]}{pattern[j]}{pattern[k]}{pattern[l]}";
                await decryptMessage(key, text);
            }
        }
    }
}
#endregion

#region
/*
 * la función decryptMessage es la que se encarga de decodificar el mensaje con la key generada
 */
async Task decryptMessage(int[] key, string text)
{
    /*
     * variable para almacenar el mensaje decodificado
     */
    string message = "";

    /*
     * recorremos la lista de númerica en donde se encuentra almacenado el mensaje encriptado
     */
    for(int i = 0; i < encryptMessage.Length; i++)
    {
        /*
         * aumentamos en una unidad el valor de la variable cont
         */
        cont++;

        /*
         * segun las condiciones de encriptacion del mensaje cada caracter se decodifica con una posición del 
         * array (0 ^ 0, 1 ^ 1, 2 ^ 2, 3 ^ 3, 4 ^ 0, 5 ^ 1...) númerico que corresponde a la key y este se repite cada
         * 4 posiciones por lo tanto cuando la variable cont tiene un valor de 4, le asigamos nuevamete el valor 0
         * para cumplir con esta condición
         */
        if (cont == 4) cont = 0;

        /*
         * desencriptamos cada posición del mensaje con su respectivo valor proporcionado por la key evaluada
         */
        char character = (char)(encryptMessage[i] ^ key[cont]);
        /*
         * con la variable boleana isValid, se valida que el caracter cumpla con la expresión regular [a-zA-Z0-9\s.,@\-_\/],
         * si un caracter no cumple con esa condición se termina esta ejecución y se continua con la validación de la siguiente
         * key
         */
        bool isValid = Regex.IsMatch(character.ToString(), regex);

        if (!isValid) return;

        /*
         * almacenamos el texto decodificado en la variable message
         */
        message += character;
    }

    /*
     * se muestra el mensaje decodificado y su correspondiente clave en la consola del programa
     */
    Console.WriteLine("-----------------------------");
    Console.WriteLine(text);
    Console.WriteLine("-----------------------------");
    Console.WriteLine(message);
    await Task.Delay(0);
    return;
}
#endregion