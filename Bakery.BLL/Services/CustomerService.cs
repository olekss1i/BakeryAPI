using AutoMapper;
using BakeryAPI.Data;
using BakeryAPI.DTOs;
using BakeryAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BakeryAPI.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CustomerService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CustomerResponseDTO>> GetAllCustomers()
        {
            var customers = await _context.Customers
                .Include(c => c.Country)
                .ToListAsync();

            return _mapper.Map<IEnumerable<CustomerResponseDTO>>(customers);
        }

        public async Task<CustomerResponseDTO> GetCustomerById(int id)
        {
            var customer = await _context.Customers
                .Include(c => c.Country)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (customer == null)
                throw new KeyNotFoundException("Customer not found");

            return _mapper.Map<CustomerResponseDTO>(customer);
        }

        public async Task<CustomerResponseDTO> CreateCustomer(CustomerCreateDTO customerDto)
        {
            var customer = _mapper.Map<Customer>(customerDto);

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return _mapper.Map<CustomerResponseDTO>(customer);
        }

        public async Task UpdateCustomer(int id, CustomerUpdateDTO customerDto)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                throw new KeyNotFoundException("Customer not found");

            _mapper.Map(customerDto, customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
                throw new KeyNotFoundException("Customer not found");

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }
    }
}
