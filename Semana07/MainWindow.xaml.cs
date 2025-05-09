using CapaEntidades;
using CapaNegocio;
using System.Collections.Generic;
using System.Windows;

namespace Semana07
{
    public partial class MainWindow : Window
    {
        private CustomerBL customerBL = new CustomerBL();

        public MainWindow()
        {
            InitializeComponent();
            MostrarClientes(""); // Carga todos los clientes al inicio
        }

        private void MostrarClientes(string nombre)
        {
            List<Customer> lista = customerBL.ListarPorNombre(nombre);
            dgCustomers.ItemsSource = lista;
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            string nombre = txtBuscar.Text.Trim();
            MostrarClientes(nombre);
        }

        private void BtnAgregar_Click(object sender, RoutedEventArgs e)
        {
            Customer nuevo = new Customer
            {
                Name = txtNombre.Text,
                Address = txtDireccion.Text,
                Phone = txtTelefono.Text,
                Active = true
            };

            customerBL.InsertarCustomer(nuevo);
            MessageBox.Show("Cliente agregado con éxito");
            MostrarClientes(""); // Refrescar la tabla
        }
    }
}
