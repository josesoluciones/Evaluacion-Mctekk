using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace josedll
{
    public class Servicio
    {
        public static void llamar( ref Datos datos)
        {

            var url = "http://rtb.000webhostapp.com/api.php/HardwareInfo/1";
            var webrequest = (HttpWebRequest)System.Net.WebRequest.Create(url);
          
            using (var response = webrequest.GetResponse())
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                var json = reader.ReadToEnd();
                //var result = reader.ReadToEnd();
                datos = JsonConvert.DeserializeObject<Datos>(json);

                //string id = datos.Id.ToString();
                //string Name = datos.Name
                //txtapi.Text = Convert.ToString(result);
               // System. datos.Name.ToString()



            }
        }

        public class Datos
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Descripcion { get; set; }
            public string Consumption { get; set; }
        }
    }
}
