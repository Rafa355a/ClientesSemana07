using CapaEntidades;
using CapaNegocio;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Semana07
{
    public partial class MainWindow : Window
    {
        private CustomerBL customerBL = new CustomerBL();

        public MainWindow()
        {
            InitializeComponent();
            MostrarClientes(""); // Carga inicial
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
            string nombre = txtNombre.Text.Trim();
            string direccion = txtDireccion.Text.Trim();
            string telefono = txtTelefono.Text.Trim();

            // Validación de campos vacíos
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(direccion) || string.IsNullOrWhiteSpace(telefono))
            {
                MessageBox.Show("Todos los campos son obligatorios.");
                return;
            }

            // Validación de nombre duplicado
            if (customerBL.ExisteNombre(nombre))
            {
                MessageBox.Show("Ya existe un cliente con ese nombre.");
                return;
            }

            Customer nuevo = new Customer
            {
                Name = nombre,
                Address = direccion,
                Phone = telefono,
                Active = true
            };

            customerBL.InsertarCustomer(nuevo);
            MessageBox.Show("Cliente agregado con éxito");
            MostrarClientes(""); // Refrescar
            LimpiarCampos();
        }

        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (dgCustomers.SelectedItem is Customer seleccionado)
            {
                seleccionado.Name = txtNombre.Text;
                seleccionado.Address = txtDireccion.Text;
                seleccionado.Phone = txtTelefono.Text;
                seleccionado.Active = true;

                customerBL.ModificarCustomer(seleccionado);
                MessageBox.Show("Cliente modificado con éxito");
                MostrarClientes("");
                LimpiarCampos();
            }
            else
            {
                MessageBox.Show("Selecciona un cliente para editar.");
            }
        }

        private void BtnEliminar_Click(object sender, RoutedEventArgs e)
        {
            if (dgCustomers.SelectedItem is Customer seleccionado)
            {
                if (MessageBox.Show("¿Deseas eliminar este cliente?", "Confirmar", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    customerBL.EliminarCustomer(seleccionado.CustomerId);
                    MessageBox.Show("Cliente eliminado con éxito");
                    MostrarClientes("");
                    LimpiarCampos();
                }
            }
            else
            {
                MessageBox.Show("Selecciona un cliente para eliminar.");
            }
        }

        private void dgCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgCustomers.SelectedItem is Customer seleccionado)
            {
                txtNombre.Text = seleccionado.Name;
                txtDireccion.Text = seleccionado.Address;
                txtTelefono.Text = seleccionado.Phone;
            }
        }

        private void LimpiarCampos()
        {
            txtNombre.Clear();
            txtDireccion.Clear();
            txtTelefono.Clear();
        }
    }
}
