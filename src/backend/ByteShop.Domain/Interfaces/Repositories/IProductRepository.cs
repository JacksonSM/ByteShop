﻿using ByteShop.Domain.Entities;
using System.Linq.Expressions;

namespace ByteShop.Domain.Interfaces.Repositories;
public interface IProductRepository : IRepository<Product>
{
    Task<(IEnumerable<Product> products, int quantityProduct)> GetAllAsync(
    string sku, string name, string brand, string category, int? actualPage, int? itemsPerPage);
}