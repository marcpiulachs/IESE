using Microsoft.EntityFrameworkCore;
using ProjectLex.InventoryManagement.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectLex.InventoryManagement.Desktop.DAL
{
    public interface IUnitOfWork: IDisposable
    {
        IGenericRepository<TEntity> GenericRepository<TEntity>() where TEntity : class;
        public GenericRepository<Role> RoleRepository { get; }
        public GenericRepository<Category> CategoryRepository { get; }
        public GenericRepository<Warehouse> WarehouseRepository { get; }
        public GenericRepository<Supplier> SupplierRepository { get; }
        public GenericRepository<Staff> StaffRepository { get; }
        public GenericRepository<Product> ProductRepository { get; }
        public GenericRepository<Order> OrderRepository { get; }
        public GenericRepository<OrderDetail> OrderDetailRepository { get; }
        public GenericRepository<Location> LocationRepository { get; }
        public GenericRepository<Customer> CustomerRepository { get; }
        public GenericRepository<Defective> DefectiveRepository { get; }
        public GenericRepository<ProductLocation> ProductLocationRepository { get; }
        public GenericRepository<Log> LogRepository { get; }
        public GenericRepository<Carrier> CarrierRepository { get; }

        public int Save();
        public void Begin();
        public void Rollback();
        public void Commit();
    }
}
