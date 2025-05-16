using System.Collections.Generic;
using System.Linq;
using CapaEntidades;
using CapaDatos;

namespace CapaNegocio
{
    public class CustomerBL
    {
        private CustomerDAO dao = new CustomerDAO();

        public List<Customer> ListarPorNombre(string nombre)
        {
            var customers = dao.ListarCustomers();
            return customers
                   .Where(c => c.Name.ToLower().Contains(nombre.ToLower()))
                   .ToList();
        }

        public void InsertarCustomer(Customer customer)
        {
            dao.InsertarCustomer(customer);
        }

        public void ModificarCustomer(Customer customer)
        {
            dao.ModificarCustomer(customer);
        }

        public void EliminarCustomer(int customerId)
        {
            dao.EliminarCustomer(customerId);
        }

        public bool ExisteNombre(string nombre)
        {
            var customers = dao.ListarCustomers();
            return customers.Any(c => c.Name.ToLower() == nombre.ToLower());
        }
    }
}
