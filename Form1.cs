using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabRepaso1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<Empleado> empleados = new List<Empleado>();
        List<Mes> meses = new List<Mes>();
        List<Sueldo> sueldos = new List<Sueldo>();

        private void buttonMostrar_Click(object sender, EventArgs e)
        {
            Empleado empleado = new Empleado();
            Mes mes = new Mes();
            string nombre = comboBox2.Text + ".txt";
            leer(nombre);
            mostar(nombre);
            cargarComboBox();
            sueldoMensual();            
        }

        private void mostar(string nombre)
        {
            dataGridView1.DataSource = null;
            dataGridView1.Refresh();

            if (nombre == "empleados.txt")
            {
                dataGridView1.DataSource = empleados;
                dataGridView1.Refresh();
            }
            else
            {
                dataGridView1.DataSource = meses;
                dataGridView1.Refresh();
            }
            
        }

        private void mostrarSueldo()
        {
            dataGridView2.DataSource = null;
            dataGridView2.Refresh();

            dataGridView2.DataSource = sueldos;
            dataGridView2.Refresh();
        }

        private void sueldoMensual()
        {           
            int pocicion = 0;
            while (pocicion < empleados.Count)
            {
                Sueldo sueldo = new Sueldo();
                sueldo.numeroEmpleado = empleados[pocicion].numeroEmpleado;
                sueldo.nombre = empleados[pocicion].nombreEmpleado;
                sueldo.sueldoMes = meses[pocicion].horaMes * empleados[pocicion].sueldo;                
                sueldos.Add(sueldo);
                pocicion++;
            }            
        }

        private void guardar(string fileNombre, string texto)
        {
            FileStream stream = new FileStream(fileNombre, FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine(texto);
            stream.Close();
        }

        private void leer(string fileNombre)
        {
            FileStream stream = new FileStream(fileNombre, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);
            while (reader.Peek() > -1)
            {
                Empleado empleado = new Empleado();
                Mes mes = new Mes();
                if (fileNombre == "empleados.txt")
                {
                    empleado.numeroEmpleado = Convert.ToInt16(reader.ReadLine());
                    empleado.nombreEmpleado = reader.ReadLine();
                    empleado.sueldo = Convert.ToInt16(reader.ReadLine());
                    empleados.Add(empleado);
                }
                else
                {
                    mes.numeroEmpleado = Convert.ToInt16(reader.ReadLine());
                    mes.horaMes = Convert.ToInt16((reader.ReadLine()));
                    mes.mes = reader.ReadLine();
                    meses.Add(mes);
                }

            }
            reader.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            String empleadoSueldo = comboBox1.SelectedItem.ToString();
            for (int i = 0; i < sueldos.Count; i++)
            {
                Sueldo sueldotemp = new Sueldo();
                if (empleadoSueldo == "empleado: " + sueldos[i].nombre)
                {
                    sueldotemp.numeroEmpleado = sueldos[i].numeroEmpleado;
                    sueldotemp.nombre = sueldos[i].nombre;
                    sueldotemp.sueldoMes = sueldos[i].sueldoMes;
                    MessageBox.Show("No.Empleado: " + sueldotemp.numeroEmpleado + "\nNombre: " + sueldotemp.nombre + "\nSueldo: " + sueldotemp.sueldoMes);
                }
            }
        }

        private void cargarComboBox()
        {
            foreach (var i in empleados)
            {
                comboBox1.Items.Add("empleado: " + i.nombreEmpleado);
            }
        }

        private void buttonSueldos_Click(object sender, EventArgs e)
        {
            mostrarSueldo();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox2.SelectedIndex = 1;
            leer("Enero.txt");
            mostar("Enero.txt");
        }
    }
}
