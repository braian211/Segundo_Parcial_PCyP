using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SegundoParcialPCyP_BraianIbarra
{
    class Program
    {
        static void Main(string[] args)
        {
            string archivo = @"c:\rutas_nacionales.csv";
            

            Dictionary<string, int> ProvinciasLong = new Dictionary<string, int>();

            Parallel.ForEach(File.ReadLines(archivo).Skip(1),
                
                () => { return new Dictionary<string, int>(); },

                (linea, loopControl, provincia) =>
                {
                    
                    char[] separadores = { ',' };

                    string[] parte = linea.Split(separadores);

                    string provincia1 = Convert.ToString(parte[0]);
                    string responsable = Convert.ToString(parte[1]);
                    string ruta = Convert.ToString(parte[2]);
                    string tipo = Convert.ToString(parte[3]);
                    int longitud = Convert.ToInt32(parte[4]);
                    

                    if (!provincia.ContainsKey(provincia1))  
                        provincia.Add(provincia1, longitud);
                    else  
                        provincia[provincia1]+=longitud;

                    return provincia;  
                },

                (provincia) =>
                {
                    lock (ProvinciasLong)
                    {
                        
                        foreach (string localProv in provincia.Keys)
                        {
                            int longitud = provincia[localProv];

                            if (!ProvinciasLong.ContainsKey(localProv))  
                                ProvinciasLong.Add(localProv, longitud);
                            else  
                                ProvinciasLong[localProv] += longitud;
                        }
                    }
                }

            );

            var ordenar = ProvinciasLong.OrderByDescending(x => x.Value);

            foreach (var user in ordenar)
                Console.WriteLine("{0}: {1}", user.Key, user.Value);

            Console.Write("Press a key to exit...");
            Console.ReadKey();
        }
        
    }
}
