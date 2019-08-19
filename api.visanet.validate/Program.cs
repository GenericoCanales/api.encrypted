using System;

using api.visanet.validate.cases;

namespace api.visanet.validate
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Inicio de Prueba: Texto Claro\n\n");
            Text_Clear obj_1 = new Text_Clear();
            string strText_Clear = obj_1.runText_Clear();
            Console.WriteLine(strText_Clear + "\n\n");
            Console.WriteLine("Fin de Prueba: Texto Claro\n\n");
            Console.ReadLine();
            
            Console.WriteLine("Inicio de Prueba: Alcance Cifrado\n\n");
            Encripted_Scope obj_2 = new Encripted_Scope();
            string strEncripted_Scope = obj_2.runEncripted_Scope();
            Console.WriteLine(strEncripted_Scope + "\n\n");
            Console.WriteLine("Fin de Prueba: Alcance Cifrado\n\n");
            Console.ReadLine();

            Console.WriteLine("Inicio de Prueba: Petición Cifrada\n\n");
            Encripted_Payload obj_3 = new Encripted_Payload();
            string strEncripted_Payload = obj_3.runEncripted_Payload();
            Console.WriteLine(strEncripted_Payload + "\n\n");
            Console.WriteLine("Fin de Prueba: Petición Cifrada\n\n");
            Console.ReadLine();

            Console.WriteLine("Inicio de Prueba: Alcance y Petición Cifrada\n\n");
            Encripted_Scope_And_Payload obj_4 = new Encripted_Scope_And_Payload();
            string strEncripted_Scope_And_Payload = obj_4.runEncripted_Scope_And_Payload();
            Console.WriteLine(strEncripted_Scope_And_Payload + "\n\n");
            Console.WriteLine("Fin de Prueba: Alcance y Petición Cifrada\n\n");
            Console.ReadLine();
        }
    }
}
