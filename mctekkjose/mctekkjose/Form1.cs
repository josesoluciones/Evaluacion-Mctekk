using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;
using System.Net;
using System.Web;
using System.Net.Http.Headers;
using System.IO;
using Newtonsoft.Json;
using josedll;

namespace mctekkjose
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            



        }

       

 
        private void button1_Click(object sender, EventArgs e)
        {


            Servicio.Datos datos = new Servicio.Datos();
            josedll.Servicio.llamar(ref datos);
            lbl_id.Text = datos.Id.ToString();
            lbl_name.Text = datos.Name.ToString();

            lbl_cons.Text = datos.Consumption.ToString();


        }

        private void btnwmic_Click(object sender, EventArgs e)
        {

            ManagementScope scope = new ManagementScope("\\root\\cimv2");
            //Crear un objeto para consultar una tabla del namespace
            ObjectQuery queryhd = new ObjectQuery("SELECT * FROM Win32_LogicalDisk where drivetype=3");
            ObjectQuery queryos = new ObjectQuery("SELECT * FROM Win32_OperatingSystem");
            ObjectQuery querybios = new ObjectQuery("SELECT * FROM Win32_UserAccount");
            ObjectQuery queryproccesor = new ObjectQuery("SELECT * FROM Win32_ComputerSystem");
            ObjectQuery queryram = new ObjectQuery("SELECT * FROM Win32_PhysicalMemory");
            ObjectQuery querymanufacturer = new ObjectQuery("SELECT * FROM Win32_BaseBoard");
            //Ejecutar el query
            ManagementObjectSearcher mos = new ManagementObjectSearcher(scope, queryhd);
            ManagementObjectSearcher searcheros = new ManagementObjectSearcher(scope, queryos);
            ManagementObjectSearcher searchcpu = new ManagementObjectSearcher(scope, queryproccesor);
            ManagementObjectSearcher searchram = new ManagementObjectSearcher(scope, queryram);
            ManagementObjectSearcher searchpc = new ManagementObjectSearcher(scope, querymanufacturer);
            ManagementObjectSearcher searchbios = new ManagementObjectSearcher(scope, querybios);
            //Iterar en los resultados del query
            foreach (ManagementObject item in mos.Get())
            {
                long hddSizeBytes = Int64.Parse(item["Size"].ToString());
                double hddSizeGBytes = hddSizeBytes / 1024 / 1024 / 1024;
                lblhd.Text = "Tamaño = " + hddSizeGBytes + "Gb";
            }
            UInt64 capacity = 0;

            foreach (ManagementObject item in searcheros.Get())
            {
                lblos.Text = item["Caption"].ToString();
            }

            foreach (ManagementObject item in searchram.Get())
            {
                capacity += (UInt64)item["Capacity"];
                lblram.Text = String.Format("{0} GB", capacity / (1024 * 1024 * 1024));

            }

            foreach (ManagementObject item in searchcpu.Get())
            {
                lblprocesador.Text = item["Manufacturer"].ToString();
            }

            foreach (ManagementObject item in searchbios.Get())
            {

                lblbios.Text = item["Name"].ToString();
            }
        }
    }
}
